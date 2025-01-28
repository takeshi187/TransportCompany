using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using TransportCompany.Classes;
using TransportCompany.database;

namespace TransportCompany.Windows
{
    public partial class AdminWin : Window
    {
        // Создаем объект базы данных
        Database database = new Database();
        // Коллекция смен
        private ObservableCollection<Employees> employees = new ObservableCollection<Employees>();
        public AdminWin()
        {
            InitializeComponent();
            // Заполнение datagrid коллекцией
            DGEmployees.ItemsSource = employees;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Вызываем функцию загрузки смен
            LoadEmployees();
        }

        private void LoadEmployees()
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

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditStatusWin editStatusWin = new EditStatusWin();
            editStatusWin.Show();
            this.Close();
        }

        private bool DeleteEmployeeProfile(long Id)
        {
            try
            {
                // Получаем id удаляемого профиля
                var selectedEmployee = employees.FirstOrDefault(item => item.EmployeeId == Id);
                if (selectedEmployee != null)
                {
                    // Удаляем из коллекции выбранного сотрудника
                    employees.Remove(selectedEmployee);

                    //Открываем подключение к бд
                    database.OpenConnection();

                    // Запрос на удаление по id
                    string query = "DELETE FROM Employees WHERE EmployeeId = @id";

                    // Создаем команду
                    SqlCommand command = new SqlCommand(query, database.GetConnection());

                    // Передаем параметры
                    command.Parameters.AddWithValue("@id", Id);

                    // Выполняем команду
                    command.ExecuteNonQuery();

                    // Результат проверки
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении профиля сотрудника: " + ex.Message);
                return false;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, является ли выбранная строка Employees
                Employees selectedEmployee = DGEmployees.SelectedItem as Employees;
                if (selectedEmployee == null)
                {
                    MessageBox.Show("Выберите профиль для удаления.", "Внимание");
                    return;
                }
                // Передаем выбранный id в функцию удаления профиля
                long id = selectedEmployee.EmployeeId;
                bool success = DeleteEmployeeProfile(id);
                // Получаем результат удаления
                if (success)
                {
                    LoadEmployees();
                    MessageBox.Show("Профиль сотрудника успешно удален!");
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении профиля");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении профиля: " + ex.Message);
            }
        }
    }
}
