using System.Data.SqlClient;
using System.Windows;
using TransportCompany.database;

namespace TransportCompany.Windows
{
    public partial class LogInWin : Window
    {
        // Создаем объект базы данных
        Database database = new Database();
        public LogInWin()
        {
            InitializeComponent();
        }

        private void Btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            // Заносим данные из полей в переменные
            string login = TextBox_Login.Text;
            string userpassword = PassBox_Password.Password;

            // Проверка на заполненные поля
            if (login == "")
                MessageBox.Show("Введите логин!");
            else if (userpassword == "")
                MessageBox.Show("Введите пароль!");
            else
            {
                // Если проверка прошла, делаем запрос к бд
                string query = "SELECT EmployeeId, LastName, PostId FROM Employees WHERE Login = @Login AND Password = @Password";

                // Открываем подключение
                database.OpenConnection();

                // Создаем команду
                SqlCommand command = new SqlCommand(query, database.GetConnection());

                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", userpassword);

                // Создаем чтение запроса
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    long id = reader.GetInt64(0);
                    string LastName = reader.GetString(1);
                    long PostId = reader.GetInt64(2);

                    try
                    {
                        switch (PostId)
                        {
                            case 1:
                                MessageBox.Show($"Добро пожаловать администратор: {LastName}!");
                                AdminWin adminWin = new AdminWin();
                                adminWin.Show();
                                this.Close();
                                break;
                            case 2:
                                MessageBox.Show($"Добро пожаловать оператор: {LastName}!");
                                OperatorWin operatorWin = new OperatorWin();
                                operatorWin.Show();
                                this.Close();
                                break;
                            case 3:
                                MessageBox.Show($"Добро пожаловать водитель: {LastName}!");
                                DriverWin driverWin = new DriverWin();
                                driverWin.SetUserInfo(id, LastName);
                                driverWin.Show();
                                this.Close();
                                break;
                            default:
                                MessageBox.Show("Некорректная должность пользователя!");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка авторизации:" + ex.Message);
                    }
                    finally
                    {
                        database.CloseConnection();
                    }
                }
                else
                {
                    MessageBox.Show("Такого аккаунта не существует!");
                    database.CloseConnection();
                }
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Login.Clear();
            PassBox_Password.Clear();
        }
    }
}
