using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TransportCompany.Classes;
using TransportCompany.database;

namespace TransportCompany.Windows
{

    public partial class UserAddWin : Window
    {
        // Создаем объект базы данных
        Database database = new Database();
        private ObservableCollection<Posts> posts = new ObservableCollection<Posts>();
        public UserAddWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox_EmployeePosts();
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
                MessageBox.Show($"Ошибка при загрузке должности: {ex.Message}");
            }
            finally
            {
                database.CloseConnection();
            }
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            // Заносим данные
            string login = TextBox_Login.Text;
            string password = PassBox_Password.Password;
            DateTime dateofbirth = Convert.ToDateTime(DatePicker_DateOfBirth.SelectedDate.Value.ToString());
            string firstname = TextBox_FirstName.Text;
            string lastname = TextBox_LastName.Text;
            string middlename = TextBox_MiddleName.Text;
            string phone = TextBox_Phone.Text;
            string email = TextBox_Email.Text;
            int post = GetPost(ComboBox_EmployeePost.Text.ToString());
            // проверка на пустые поля
            if (login == "" || password == "" || dateofbirth == null || firstname == "" || lastname == "" || middlename==""|| phone==""|| email==""||post==null)
            {
                MessageBox.Show("Не удалось зарегистрировать пользователя!");
                return;
            }
            // добавление пользователя
            try
            {
                // создаем запрос
                string query = $"INSERT INTO Employees" +
                    $"(StatusId, PostId, FirstName, LastName, MiddleName, DateOfBirth, Phone, Email, Login, Password) VALUES " +
                    $"(1, @Post, @FirstName, @LastName, @MiddleName, @Date, @Phone, @Email, @Login, @Password)";
                SqlCommand command = new SqlCommand(query, database.GetConnection());
                // Задаем параметры
                command.Parameters.AddWithValue("@FirstName", firstname);
                command.Parameters.AddWithValue("@LastName", lastname);
                command.Parameters.AddWithValue("@MiddleName", middlename);
                command.Parameters.AddWithValue("@Date", dateofbirth);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Post", post);
                // Открываем подключение
                database.OpenConnection();

                if (command.ExecuteNonQuery() == 1)
                    MessageBox.Show("Успешно!");
                else
                    MessageBox.Show("Не удалось зарегистрировать пользователя!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                database.CloseConnection();
            }
        }
        // Функция для получения id роли
        private int GetPost(string text)
        {
            if (text == null)
                return 0;
            else if (text == "Администратор")
                return 1;
            else if (text == "Оператор")
                return 2;
            else if (text == "Водитель")
                return 3;
            else
                return 0;
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            // Назад в окно администрации
            AdminWin adminWin = new AdminWin();
            adminWin.Show();
            this.Close();
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            // Очистить содержимое textbox'ов
            TextBox_Login.Clear();
            PassBox_Password.Clear();
            TextBox_FirstName.Clear();
            TextBox_LastName.Clear();
            TextBox_MiddleName.Clear();
            TextBox_Phone.Clear();
            TextBox_Email.Clear();
            ComboBox_EmployeePost.SelectedItem = null;
            DatePicker_DateOfBirth.SelectedDate = null;

        }
    }
}
