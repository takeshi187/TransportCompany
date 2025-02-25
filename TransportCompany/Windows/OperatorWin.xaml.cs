using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using TransportCompany.Classes;
using TransportCompany.database;

namespace TransportCompany.Windows
{
    public partial class OperatorWin : Window
    {
        // Создаем объект базы данных
        Database database = new Database();
        // Коллекция смен
        private ObservableCollection<Shifts> shifts = new ObservableCollection<Shifts>();
        public OperatorWin()
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
                string query = "SELECT * FROM Shifts";

                // Создаем команду
                SqlCommand command = new SqlCommand(query, database.GetConnection());

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
                        Hours = Convert.ToInt32(reader["Hours"]),
                        Distance = Convert.ToInt32(reader["Distance"])
                    };
                    // Заносим данные в коллекцию
                    shifts.Add(shift);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке смен: {ex.Message}");
            }
            finally
            {
                database.CloseConnection();
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Calculate_Salary_Per_Km_Click(object sender, RoutedEventArgs e)
        {
            CalculateSalaryPerKmWin calculateSalaryPerKmWin = new CalculateSalaryPerKmWin();
            calculateSalaryPerKmWin.Show();
            this.Close();
        }

        private void Btn_Calculate_Salary_Per_Hour_Click(object sender, RoutedEventArgs e)
        {
            CalculateSalaryPerHourWin calculateSalaryPerHourWin = new CalculateSalaryPerHourWin();
            calculateSalaryPerHourWin.Show();
            this.Close();
        }
    }
}
