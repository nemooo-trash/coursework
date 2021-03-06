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


namespace coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Student[] student = new Student[1];
        DataTable dt_user = new DataTable();
        DataTable dt_user_user = new DataTable();
        public string user_login = "";
        public string user_role = "";
        public MainWindow(User[] user) //+ in block
        {
            InitializeComponent();
            user_login = user[0].login;
            user_role = user[0].role;
            if (user[0].role != "admin") {
                if (user[0].role == "user")
                {
                    Tab_Mom.Items.Remove(full_users);
                    gridStudents.IsReadOnly = true;
                    btn_del.Visibility = Visibility.Hidden;
                    btn_res.Visibility = Visibility.Hidden;
                    gridStudents1.IsReadOnly = true;
                }
                else 
                {
                    Tab_Mom.Items.Remove(name);
                    Tab_Mom.Items.Remove(otbor_name);
                    Tab_Mom.Items.Remove(full_users);
                    gridStudents.IsReadOnly = true;
                    btn_del.Visibility = Visibility.Hidden;
                    btn_res.Visibility = Visibility.Hidden;
                }
            }
            gridStudents.CanUserSortColumns = false;
            gridStudents1.CanUserSortColumns = false;
            gridUsers.CanUserSortColumns = false;
            dateTime1.DisplayDateEnd = DateTime.Now;
            name_block.Text += " Логин пользователя - " + user[0].login;
            name_block.Text += ", Ваша роль - " + user[0].role;
            sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
            connect.conOpen();
            dt_user = connect.select_query("SELECT * FROM[dbo].[student] AS st LEFT JOIN[dbo].[gender] AS gr ON(st.gender = gr.id) LEFT JOIN[dbo].[lear] ler ON(st.base_learn = ler.id)"); // получаем данные из таблицы
            dt_user_user = connect.select_query("SELECT * FROM[dbo].[user]"); // получаем данные из таблицы
            connect.conClose();
            Person_for_tables[] persons = new Person_for_tables[dt_user.Rows.Count];
            Users_for_tables[] users = new Users_for_tables[dt_user_user.Rows.Count - 1];
            for (int i = 0; i < dt_user.Rows.Count; i++)
            {
                persons[i] = new Person_for_tables() { FIO = dt_user.Rows[i][2] + " " + dt_user.Rows[i][1] + " " + dt_user.Rows[i][3], st_number = Convert.ToInt32(dt_user.Rows[i][7]), gender_table = Convert.ToString(dt_user.Rows[i][11]), date = Convert.ToString(dt_user.Rows[i][4]), score = Convert.ToString(dt_user.Rows[i][8]), osnova = Convert.ToString(dt_user.Rows[i][13]), prim = Convert.ToString(dt_user.Rows[i][9]) };
            }
            int new_index = 0;
            for (int i = 0; i < dt_user_user.Rows.Count; i++)
            {
                if (Convert.ToString(dt_user_user.Rows[i][1]) != user_login)
                {
                    users[new_index] = new Users_for_tables() { Login = Convert.ToString(dt_user_user.Rows[i][1]), role = Convert.ToString(dt_user_user.Rows[i][3]), password = "" };
                    new_index += 1;
                }
            }
            gridStudents.ItemsSource = persons;
            gridUsers.ItemsSource = users;
            textBlock1.Text = dt_user.Rows.Count.ToString();
        }
        void Load_number2(object sender, RoutedEventArgs e) //Вывод всех
        {
            sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
            connect.conOpen();
            dt_user = connect.select_query("SELECT * FROM[dbo].[student] AS st LEFT JOIN[dbo].[gender] AS gr ON(st.gender = gr.id) LEFT JOIN[dbo].[lear] ler ON(st.base_learn = ler.id)"); // получаем данные из таблицы
            connect.conClose();
            Person_for_tables[] persons = new Person_for_tables[dt_user.Rows.Count];
            for (int i = 0; i < dt_user.Rows.Count; i++)
            {
                persons[i] = new Person_for_tables() { FIO = dt_user.Rows[i][2] + " " + dt_user.Rows[i][1] + " " + dt_user.Rows[i][3], st_number = Convert.ToInt32(dt_user.Rows[i][7]), gender_table = Convert.ToString(dt_user.Rows[i][11]), date = Convert.ToString(dt_user.Rows[i][4]), score = Convert.ToString(dt_user.Rows[i][8]), osnova = Convert.ToString(dt_user.Rows[i][13]), prim = Convert.ToString(dt_user.Rows[i][9]) };
            }
            gridStudents.ItemsSource = persons;
            textBlock1.Text = dt_user.Rows.Count.ToString();
            gridStudents.Height = 410;
            gridStudents.MaxHeight = 410;
        }
        void Load_number3(object sender, RoutedEventArgs e) //Подбор
        {
            sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
            connect.conOpen();
            DataTable dt_user1 = connect.select_query("SELECT * FROM[dbo].[student] AS st INNER JOIN[dbo].[gender] AS gr ON(st.gender = gr.id) INNER JOIN[dbo].[lear] ler ON(st.base_learn = ler.id) where ler.type = 'платная' order by st.birthday DESC"); // получаем данные из таблицы                                                                                                                                                                                                                                  // получаем данные из таблицы
            connect.conClose();
            Person_for_tables[] persons = new Person_for_tables[dt_user1.Rows.Count];
            for (int i = 0; i < dt_user1.Rows.Count; i++)
            {
                persons[i] = new Person_for_tables() { FIO = dt_user1.Rows[i][2] + " " + dt_user1.Rows[i][1] + " " + dt_user1.Rows[i][3], st_number = Convert.ToInt32(dt_user1.Rows[i][7]), gender_table = Convert.ToString(dt_user1.Rows[i][11]), date = Convert.ToString(dt_user1.Rows[i][4]), score = Convert.ToString(dt_user1.Rows[i][8]), osnova = Convert.ToString(dt_user1.Rows[i][13]), prim = Convert.ToString(dt_user1.Rows[i][9]) };
            }
            gridStudents1.ItemsSource = persons;
        }
        void OnClick1(object sender, RoutedEventArgs e)
        {
            var Auth = new Auth();
            Auth.Show();
            this.Close();
        }
        void OnClick2(object sender, RoutedEventArgs e) //+ in block
        {
            int k;
            if (textBox1.Text == "" || Int32.TryParse(textBox1.Text, out k) == true)
            {
                textBox1.Background = Brushes.Red; //Color.FromRgb(237, 105, 105);
                textBox1.Foreground = Brushes.White;
            }
            else if (textBox2.Text == "" || Int32.TryParse(textBox2.Text, out k) == true)
            {
                textBox2.Background = Brushes.Red; //Color.FromRgb(237, 105, 105);
                textBox2.Foreground = Brushes.White;
            }
            else if (textBox3.Text == "" || Int32.TryParse(textBox3.Text, out k) == true)
            {
                textBox3.Background = Brushes.Red; //Color.FromRgb(237, 105, 105);
                textBox3.Foreground = Brushes.White;
            }
            else if (textBox4.Text == "" || Int32.TryParse(textBox4.Text, out k) == false)
            {
                textBox4.Background = Brushes.Red; //Color.FromRgb(237, 105, 105);
                textBox4.Foreground = Brushes.White;
            }
            else if (textBox5.Text != "") //Для задолжностей
            {
                if (Int32.TryParse(textBox5.Text, out k) == false)
                {
                    textBox5.Background = Brushes.Red; //Color.FromRgb(237, 105, 105);
                    textBox5.Foreground = Brushes.White;
                }
            }
            else
            {
                sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
                connect.conOpen();
                dt_user = connect.select_query("SELECT * FROM[dbo].[student]  WHERE stud_number = '" + textBox4.Text + "'"); // получаем данные из таблицы
                connect.conClose();
                if (dt_user.Rows.Count > 0)
                {
                    MessageBox.Show("Номер студ. билета уже есть в базе!", "ERROR!");
                }
                else
                {
                    connect = new sqlCon.SqlConnect();
                    connect.conOpen();
                    int key = gender.SelectedIndex + 1;
                    int key_2 = base_learn.SelectedIndex + 1;
                    student[0] = new Student
                    {
                        Name = textBox2.Text,
                        Surname = textBox1.Text,
                        LName = textBox3.Text,
                        DateD = dateTime1.SelectedDate.Value.Day,
                        DateM = dateTime1.SelectedDate.Value.Month,
                        DateY = dateTime1.SelectedDate.Value.Year,
                        Gender = Convert.ToString(key),
                        S_number = Convert.ToInt32(textBox4.Text),
                        L_base = key_2,
                        Score = textBox5.Text,
                        Note = textBox6.Text
                    };
                    int check = connect.insert_command_student(student);
                    Load_number2(gridStudents, e);
                    Load_number3(gridStudents1, e);
                }
            }
        }
        public class DB_ID
        {
            public int id;
        }
        void OnClick3(object sender, RoutedEventArgs e) // delete
        {
            sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
            connect.conOpen();
            dt_user = connect.select_query("SELECT * FROM[dbo].[student]"); // получаем данные из таблицы
            connect.conClose();
            DB_ID[] ids = new DB_ID[dt_user.Rows.Count];
            for (int i = 0; i < dt_user.Rows.Count; i++)
            {
                ids[i] = new DB_ID
                {
                    id = Convert.ToInt32(dt_user.Rows[i][0])
                };
            }
            var index = gridStudents.SelectedIndex;
            gridStudents.ClearValue(ItemsControl.ItemsSourceProperty);
            connect.conOpen();
            connect.delete_command_studentById(ids[index].id);
            dt_user = connect.select_query("SELECT * FROM[dbo].[student] AS st LEFT JOIN[dbo].[gender] AS gr ON(st.gender = gr.id) LEFT JOIN[dbo].[lear] ler ON(st.base_learn = ler.id)"); // получаем данные из таблицы
            connect.conClose();
            Person_for_tables[] persons = new Person_for_tables[dt_user.Rows.Count];
            for (int i = 0; i < dt_user.Rows.Count; i++)
            {
                persons[i] = new Person_for_tables() { FIO = dt_user.Rows[i][2] + " " + dt_user.Rows[i][1] + " " + dt_user.Rows[i][3], st_number = Convert.ToInt32(dt_user.Rows[i][7]), gender_table = Convert.ToString(dt_user.Rows[i][11]), date = Convert.ToString(dt_user.Rows[i][4]), score = Convert.ToString(dt_user.Rows[i][8]), osnova = Convert.ToString(dt_user.Rows[i][13]), prim = Convert.ToString(dt_user.Rows[i][9]) };
            }
            gridStudents.ItemsSource = persons;
            Load_number3(gridStudents1, e);
            connect.conOpen();
            dt_user = connect.select_query("SELECT * FROM[dbo].[student] AS st LEFT JOIN[dbo].[gender] AS gr ON(st.gender = gr.id) LEFT JOIN[dbo].[lear] ler ON(st.base_learn = ler.id)"); // получаем данные из таблицы
            connect.conClose();
            textBlock1.Text = dt_user.Rows.Count.ToString();
            Load_number3(gridStudents1, e);
            Load_number2(gridStudents, e);
        }
        void OnClick4(object sender, RoutedEventArgs e) // edit
        {
            var index = gridStudents.SelectedIndex;
            Person_for_tables newPerson = new Person_for_tables();
            Person_for_tables obj = gridStudents.SelectedItem as Person_for_tables;
            string Full_name = obj.FIO;
            int st_numb = obj.st_number;
            string gender = obj.gender_table;
            string date = obj.date;
            string score = obj.score;
            string osn = obj.osnova;
            string prim = obj.prim;
            sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
            connect = new sqlCon.SqlConnect();
            connect.conOpen();
            dt_user = connect.select_query("SELECT * FROM[dbo].[student] AS st LEFT JOIN[dbo].[gender] AS gr ON(st.gender = gr.id) LEFT JOIN[dbo].[lear] ler ON(st.base_learn = ler.id)"); // получаем данные из таблицы
            DB_ID[] ids = new DB_ID[dt_user.Rows.Count];
            for (int i = 0; i < dt_user.Rows.Count; i++)
            {
                ids[i] = new DB_ID
                {
                    id = Convert.ToInt32(dt_user.Rows[i][0])
                };
            }
            connect.delete_command_studentById(ids[index].id);
            int key = 0;
            if (gender == "мужской")
            {
                key = 1;
            }
            else {
                key = 2;
            }
            int key_2 = 0;
            if (osn == "бюджетная")
            {
                key_2 = 1;
            }
            else {
                key_2 = 2;
            }
            string[] fullName = Convert.ToString(Full_name).Split(' ');
            DateTime date1 = Convert.ToDateTime(date);
            student[0] = new Student
            {
                Name = fullName[1],
                Surname = fullName[0],
                LName = fullName[2],
                DateD = date1.Day,
                DateM = date1.Month,
                DateY = date1.Year,
                Gender = Convert.ToString(key),
                S_number = st_numb,
                L_base = key_2,
                Score = score,
                Note = prim
            };
            int check = connect.insert_command_student(student);
            connect.conClose();
            Load_number3(gridStudents1, e);
            Load_number2(gridStudents, e);
        }
        public class DB_ID_Users
        {
            public int id;
        }
        void delete_users(object sender, RoutedEventArgs e) {       //удаление пользователей 
            sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
            connect.conOpen();
            dt_user_user = connect.select_query("SELECT * FROM[dbo].[user]"); // получаем данные из таблицы
            connect.conClose();
            DB_ID_Users[] ids_user = new DB_ID_Users[dt_user_user.Rows.Count - 1];
            int new_index = 0;
            for (int i = 0; i < dt_user_user.Rows.Count; i++)
            {
                if (Convert.ToString(dt_user_user.Rows[i][1]) != user_login)
                {
                    ids_user[new_index] = new DB_ID_Users
                    {
                        id = Convert.ToInt32(dt_user_user.Rows[i][0])
                    };
                    new_index += 1;
                }
            }
            var index = gridUsers.SelectedIndex;
            gridUsers.ClearValue(ItemsControl.ItemsSourceProperty);
            connect.conOpen();
            connect.delete_command_userById(ids_user[index].id);
            dt_user_user = connect.select_query("SELECT * FROM[dbo].[user]"); // получаем данные из таблицы
            connect.conClose();
            Users_for_tables[] users = new Users_for_tables[dt_user_user.Rows.Count - 1];
            new_index = 0;
            for (int i = 0; i < dt_user_user.Rows.Count; i++)
            {
                if (Convert.ToString(dt_user_user.Rows[i][1]) != user_login)
                {
                    users[new_index] = new Users_for_tables() { Login = Convert.ToString(dt_user_user.Rows[i][1]), role = Convert.ToString(dt_user_user.Rows[i][3]), password = "" };
                    new_index += 1;
                }
            }
            gridUsers.ItemsSource = users;
        } 
        void update_users(object sender, RoutedEventArgs e) {       //редактирование пользователей
            var index = gridUsers.SelectedIndex;
            Users_for_tables obj = gridUsers.SelectedItem as Users_for_tables;
            string login = obj.Login;
            string password = obj.password;
            string role = obj.role;
            sqlCon.SqlConnect connect = new sqlCon.SqlConnect();
            connect = new sqlCon.SqlConnect();
            connect.conOpen();
            dt_user_user = connect.select_query("SELECT * FROM[dbo].[user]"); // получаем данные из таблицы
            DB_ID_Users[] ids_user = new DB_ID_Users[dt_user_user.Rows.Count - 1];
            int new_index = 0;
            for (int i = 0; i < dt_user_user.Rows.Count; i++)
            {
                if (Convert.ToString(dt_user_user.Rows[i][1]) != user_login)
                {
                    ids_user[new_index] = new DB_ID_Users
                    {
                        id = Convert.ToInt32(dt_user_user.Rows[i][0])
                    };
                    new_index += 1;
                }
            }
            gridUsers.ClearValue(ItemsControl.ItemsSourceProperty);
            connect.delete_command_userById(ids_user[index].id);
            getHash.GetHash get = new getHash.GetHash();
            password = get.GetHashString(password);
            int x = connect.insert_command_user(login, password, role);
            dt_user_user = connect.select_query("SELECT * FROM[dbo].[user]"); // получаем данные из таблицы
            connect.conClose();
            Users_for_tables[] users = new Users_for_tables[dt_user_user.Rows.Count - 1];
            new_index = 0;
            for (int i = 0; i < dt_user_user.Rows.Count; i++)
            {
                if (Convert.ToString(dt_user_user.Rows[i][1]) != user_login)
                {
                    users[new_index] = new Users_for_tables() { Login = Convert.ToString(dt_user_user.Rows[i][1]), role = Convert.ToString(dt_user_user.Rows[i][3]), password = "" };
                    new_index += 1;
                }
            }
            gridUsers.ItemsSource = users;
            MessageBox.Show("Пользователь успешно обновлен!");
        }
            void TextChecker1(object sender, RoutedEventArgs e)
            {
                textBox1.Background = Brushes.LightGray; //Color.FromRgb(237, 105, 105);
                textBox1.Foreground = Brushes.Black;
            }
            void TextChecker2(object sender, RoutedEventArgs e)
            {
                textBox2.Background = Brushes.LightGray; //Color.FromRgb(237, 105, 105);
                textBox2.Foreground = Brushes.Black;
            }
            void TextChecker3(object sender, RoutedEventArgs e)
            {
                textBox3.Background = Brushes.LightGray; //Color.FromRgb(237, 105, 105);
                textBox3.Foreground = Brushes.Black;
            }
            void TextChecker4(object sender, RoutedEventArgs e)
            {
                textBox4.Background = Brushes.LightGray; //Color.FromRgb(237, 105, 105);
                textBox4.Foreground = Brushes.Black;
            }
            void TextChecker5(object sender, RoutedEventArgs e)
            {
                textBox5.Background = Brushes.LightGray; //Color.FromRgb(237, 105, 105);
                textBox5.Foreground = Brushes.Black;
            }
        }
    }
