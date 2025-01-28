using System.Windows;
using TransportCompany.database;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using TransportCompany.Classes;

namespace TransportCompany.Windows
{
    public partial class EditStatusWin : Window
    {
        // Создаем объект базы данных
        Database database = new Database();
        private ObservableCollection<Employees> employees = new ObservableCollection<Employees>();
        private ObservableCollection<Statuses> statuses = new ObservableCollection<Statuses>();
        private ObservableCollection<Posts> posts = new ObservableCollection<Posts>();
        public EditStatusWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox_EmployeeId();
        }

        private void LoadComboBox_EmployeeId()
        {
            try
            {
                // Очищаем содержимое коллекции
                employees.Clear();

                // Открываем подключение
                database.OpenConnection();

                // Запрос к базе
                string query = "SELECT * FROM Employees";

                // Создаем команду
                SqlCommand command = new SqlCommand(query, database.GetConnection());

                // Выполняем команду
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Читаем ответ
                    Employees employee = new Employees()
                    {
                        EmployeeId = Convert.ToInt64(reader["EmployeeId"]),
                        StatusId = Convert.ToInt64(reader["StatusId"]),
                        PostId = Convert.ToInt64(reader["PostId"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        MiddleName = Convert.ToString(reader["MiddleName"]),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Phone = Convert.ToString(reader["Phone"]),
                        Email = Convert.ToString(reader["Email"]),
                        Login = Convert.ToString(reader["Login"]),
                        Password = Convert.ToString(reader["Password"])
                    };
                    // Заносим данные в коллекцию
                    employees.Add(employee);
                }
                
                // Загрузка данных в combobox
                ComboBox_EmployeeId.DataContext = employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке сотрудников: {ex.Message}");
            }
            finally
            {
                database.CloseConnection();
            }       
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
