using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using TransportCompany.Classes;
using System.Data.SqlClient;
using TransportCompany.database;

namespace TransportCompany.Windows
{
    public partial class DriverWin : Window
    {
        // Создаем объект базы данных
        Database database = new Database();
        // Коллекция смен
        private ObservableCollection<Shifts> shifts = new ObservableCollection<Shifts>();
        public DriverWin()
        {
            InitializeComponent();
            // Заполнение datagrid коллекцией
            DGShifts.ItemsSource = shifts;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Вызываем функцию загрузки смен
            LoadShifts();
        }

        private void LoadShifts()
        {
            try
            {
                // Очищаем содержимое коллекции
                shifts.Clear();

                // Открываем подключение
                database.OpenConnection();

                // Запрос к базе
                string query = "SELECT * FROM Shifts WHERE EmployeeId = @id";

                // Создаем команду
                SqlCommand command = new SqlCommand(query, database.GetConnection());

                command.Parameters.AddWithValue("@id", TextBox_DriverId.Text.ToString());

                // Выполняем команду
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Читаем ответ
                    Shifts shift = new Shifts()
                    {
                        ShiftId = Convert.ToInt64(reader["ShiftId"]),
                        EmployeeId = Convert.ToInt64(reader["EmployeeId"]),
                        RoadTrainId = Convert.ToInt64(reader["RoadTrainId"]),
                        SupplyId = Convert.ToInt64(reader["SupplyId"]),
                        RouteId = Convert.ToInt64(reader["RouteId"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        Hours = Convert.ToInt32(reader["Hours"])
                    };
                    // Заносим данные в коллекцию
                    shifts.Add(shift);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке смен: {ex.Message}");
            }
            finally
            { 
                database.CloseConnection(); 
            }
        }

        public void SetUserInfo(long id, string lastname)
        {
            TextBox_DriverId.Text = id.ToString();
            TextBox_LastName.Text = lastname;
        }
    }
}
