using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace coursework
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }
        void Close(object sender, RoutedEventArgs e) {
            this.Close();
        }
        void auth_reg(object sender, RoutedEventArgs e) {
            string login = login_box.Text;
            string password = password_box.Password.ToString();
            getHash.GetHash get = new getHash.GetHash();
            password = get.GetHashString(password);
            sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
            connect.conOpen();
            DataTable dt_user1 = connect.getpassword("SELECT * FROM[dbo].[user] WHERE login = @Login AND password = @Password",login, password);
            connect.conClose();
            User[] user = new User[1];
            if (dt_user1.Rows.Count != 0)
            {
                for (int i = 0; i < dt_user1.Rows.Count; i++)
                {
                    user[0] = new User()
                    {
                        login = Convert.ToString(dt_user1.Rows[0][1]),
                        role = Convert.ToString(dt_user1.Rows[0][3])
                    };
                }
                var MainWindow = new MainWindow(user); //create your new form.
                MainWindow.Show(); //show the new form.

                this.Close(); //only if you want to close the current form.
            }
            else { MessageBox.Show("Неверный логин или пароль!"); }
        }
        void Register(object sender, RoutedEventArgs e) {
            string login = loginBoxReg.Text;
            string input = passwordBoxReg.Password.ToString();

            string ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage += "Пароль не должен быть пустым \n";
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{6,20}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage += "Пароль должен содержать хотя бы одну строчную букву\n";
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage += "Пароль должен содержать хотя бы одну заглавную букву\n";
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage += "Пароль должен содержать хотя бы 6 символов\n";
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage += "Пароль должен содержать хотя бы одно числовое значение\n";
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage += "Пароль должен содержать хотя бы 1 спец. символ\n";
            }
            else
            {
                sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
                connect.conOpen();
                DataTable dt_user1 = connect.select_query("SELECT * FROM[dbo].[user] WHERE login = '" + login + "'");
                connect.conClose();
                if (dt_user1.Rows.Count > 0)
                {
                    MessageBox.Show("Такой логин уже есть в базе, введите другой");
                }
                else {
                    getHash.GetHash get = new getHash.GetHash();
                    input = get.GetHashString(input);
                    connect.conOpen();
                    connect.insert_command_user(login, input);
                    connect.conClose();
                    MessageBox.Show("Пользователь успешно зарегистрирован! \n Теперь вам необходимо авторизоваться.");
                }
            }

            if (ErrorMessage != "") { MessageBox.Show(ErrorMessage); }



        }
    }
}
