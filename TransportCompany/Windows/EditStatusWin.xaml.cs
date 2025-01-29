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
            LoadComboBox_EmployeeStatus();
            LoadComboBox_EmployeePosts();
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
                ComboBox_EmployeeId.ItemsSource = employees;
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

        private void LoadComboBox_EmployeeStatus()
        {
            try
            {
                // Очищаем содержимое коллекции
                statuses.Clear();

                // Открываем подключение
                database.OpenConnection();

                // Запрос к базе
                string query = "SELECT * FROM Statuses";

                // Создаем команду
                SqlCommand command = new SqlCommand(query, database.GetConnection());

                // Выполняем команду
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Читаем ответ
                    Statuses status = new Statuses()
                    {
                        StatusId = Convert.ToInt64(reader["StatusId"]),
                        Status = Convert.ToString(reader["Status"])                    
                    };
                    // Заносим данные в коллекцию
                    statuses.Add(status);
                }

                // Загрузка данных в combobox
                ComboBox_EmployeeStatus.ItemsSource = statuses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке статусов: {ex.Message}");
            }
            finally
            {
                database.CloseConnection();
            }
        }

        private void LoadComboBox_EmployeePosts()
        {
            try
            {
                // Очищаем содержимое коллекции
                posts.Clear();

                // Открываем подключение
                database.OpenConnection();

                // Запрос к базе
                string query = "SELECT * FROM Posts";

                // Создаем команду
                SqlCommand command = new SqlCommand(query, database.GetConnection());

                // Выполняем команду
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Читаем ответ
                    Posts post = new Posts()
                    {
                        PostId = Convert.ToInt64(reader["PostId"]),
                        PostName = Convert.ToString(reader["PostName"])
                    };
                    // Заносим данные в коллекцию
                    posts.Add(post);
                }

                // Загрузка данных в combobox
                ComboBox_EmployeePost.ItemsSource = posts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке должности: {ex.Message}");
            }
            finally
            {
                database.CloseConnection();
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWin adminWin = new AdminWin();
            adminWin.Show();
            this.Close();
        }
        // to do
        private int GetPost(int post)
        {
            return 0;
        }

        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            int status = 0;
            int post = 0;
            //status = GetStatus(status);
            post = GetPost(post);
            try
            {
                if(post == 0)
                {
                    MessageBox.Show("Не удалось обновить статус");
                }
                else
                {
                    database.OpenConnection();

                    string query = $"UPDATE Employees set StatusId = @status, PostId = @post WHERE EmployeeId = @employeeid";

                    SqlCommand command = new SqlCommand(query, database.GetConnection());

                    command.Parameters.AddWithValue("@status", ComboBox_EmployeeStatus.Text.ToString());
                    command.Parameters.AddWithValue("@post", ComboBox_EmployeePost.Text.ToString());
                    command.Parameters.AddWithValue("@employeeid", ComboBox_EmployeeId.Text.ToString());

                    command.ExecuteNonQuery();

                    MessageBox.Show("Статус успешно изменен!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось обновить статус: " + ex.Message);
            }
            finally
            {
                database.CloseConnection();
            }
        }
    }
}
