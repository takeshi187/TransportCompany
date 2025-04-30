using System.Collections.ObjectModel;
using System.Windows;
using TransportCompany.Classes;
using TransportCompany.database;
using System.Data.SqlClient;

namespace TransportCompany.Windows
{
    public partial class CalculateSalaryPerHourWin : Window
    {
        // Создаем объект базы данных
        Database database = new Database();
        // Создаем коллекцию водителей
        private ObservableCollection<Employees> employees = new ObservableCollection<Employees>();
        public CalculateSalaryPerHourWin()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Загружаем водителей
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
                string query = "SELECT * FROM Employees WHERE PostId = 3";

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
                ComboBox_EmployeeId.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сотрудников: {ex.Message}");
            }
            finally
            {
                database.CloseConnection();
            }
        }

        private void Btn_Calculate_Salary_Click(object sender, RoutedEventArgs e)
        {
            // Рассчет зарплаты
            try
            {
                // Задаем значения
                long empId = long.Parse(ComboBox_EmployeeId.Text);
                decimal hourlyRate = decimal.Parse(TextBox_Hourly_Rate.Text);

                decimal totalSalary = 0;

                // Открываем подключение к БД
                database.OpenConnection();

                // Создаем запрос
                string query = "SELECT Hours FROM Shifts WHERE EmployeeId = @EmployeeId AND StartDate >= DATEADD(MONTH, -1, GETDATE())";

                // Создаем команду
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                command.Parameters.AddWithValue("@EmployeeId", empId);

                // Создаем чтение запроса
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int hours = reader.GetInt32(0);

                    // Рассчитываем зарплату для каждой смены
                    totalSalary += (hours * hourlyRate);
                }
                reader.Close();

                // Вставляем расчет в таблицу выплат
                string insertquery = "INSERT INTO Salary (EmployeeId, Count, Date) VALUES (@EmployeeId, @Count, GETDATE())";

                // Создаем команду
                SqlCommand insertCommand = new SqlCommand(insertquery, database.GetConnection());
                insertCommand.Parameters.AddWithValue("@EmployeeId", empId);
                insertCommand.Parameters.AddWithValue("@Count", totalSalary);

                // Выполняем команду
                insertCommand.ExecuteNonQuery();

                MessageBox.Show($"Итого: {totalSalary:C}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расчете зарплаты: {ex.Message}");
            }
            finally
            {
                database.CloseConnection();
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            OperatorWin operatorWin = new OperatorWin();
            operatorWin.Show();
            this.Close();
        }
    }
}
