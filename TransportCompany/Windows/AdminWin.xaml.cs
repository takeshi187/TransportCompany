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
        private ObservableCollection<EmployeeDisplay> employeesDisplay = new ObservableCollection<EmployeeDisplay>();
        public AdminWin()
        {
            InitializeComponent();
            // Заносим данные в коллекцию
            DGEmployees.ItemsSource = employeesDisplay;
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
                employeesDisplay.Clear();
                // Открываем подключение
                database.OpenConnection();

                // Запрос к базе
                string query = "SELECT e.EmployeeId, e.FirstName, e.LastName, e.MiddleName, p.PostName AS PostName, s.Status AS Status, e.Phone, e.DateOfBirth, e.Email, e.Login, e.Password FROM Employees e INNER JOIN Posts p ON e.PostId = p.PostId INNER JOIN Statuses s ON e.StatusId = s.StatusId";
                // Создаем команду
                SqlCommand command = new SqlCommand(query, database.GetConnection());

                // Выполняем команду
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Читаем ответ
                    employeesDisplay.Add(new EmployeeDisplay
                    {
                        EmployeeId = (long)reader["EmployeeId"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        PostName = reader["PostName"].ToString(),
                        Status = reader["Status"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Email = reader["Email"].ToString(),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString()
                    });                    
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
                var selectedEmployee = employeesDisplay.FirstOrDefault(item => item.EmployeeId == Id);
                if (selectedEmployee != null)
                {
                    // Удаляем из коллекции выбранного сотрудника
                    employeesDisplay.Remove(selectedEmployee);

                    //Открываем подключение к бд
                    database.OpenConnection();

                    // Запрос на удаление по shift
                    string queryShift = "DELETE FROM Shifts WHERE EmployeeId = @Id";

                    // Создаем команду
                    SqlCommand commandShift = new SqlCommand(queryShift, database.GetConnection());

                    // Передаем параметры
                    commandShift.Parameters.AddWithValue("@id", Id);

                    // Выполняем команду
                    commandShift.ExecuteNonQuery();

                    // Запрос на удаление по salary
                    string querySalary = "DELETE FROM Salary WHERE EmployeeId = @Id";
                    
                    // Создаем команду
                    SqlCommand commandSalary = new SqlCommand(querySalary, database.GetConnection());

                    // Передаем параметры
                    commandSalary.Parameters.AddWithValue("@id", Id);

                    // Выполняем команду
                    commandSalary.ExecuteNonQuery();

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
                EmployeeDisplay selectedEmployee = DGEmployees.SelectedItem as EmployeeDisplay;
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

        private void Btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            UserAddWin userAddWin = new UserAddWin();
            userAddWin.Show();
            this.Close();
        }
    }
}
