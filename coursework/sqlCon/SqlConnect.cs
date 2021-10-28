using System;
using System.Data;
using System.Data.SqlClient;

namespace coursework.sqlCon
{

    public class SqlConnect
    {
        public SqlConnection Con { get; set; }//the object
        private string conString { get; set; }//the string to store your connection parameters
        public void conOpen()
        {
            conString = "Data Source=.\\NEMOOO;Initial Catalog=students;Integrated Security=True"; //the same as you post in your post
            Con = new SqlConnection(conString);//
            try
            {
                Con.Open();//try to open the connection
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Ошибка подключения к бд!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void conClose()
        {
            Con.Close();//close the connection
        }
        public DataTable select_query(string query)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlCommand sqlCommand = Con.CreateCommand();                    // создаём команду
            sqlCommand.CommandText = query;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }

        public DataTable getpassword(string query,string login, string password)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlCommand sqlCommand = Con.CreateCommand();                    // создаём команду

            sqlCommand.CommandText = query;
            sqlCommand.Parameters.Add("@Login", SqlDbType.NVarChar, 20);
            sqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 200);

            sqlCommand.Parameters["@Login"].Value = login;
            sqlCommand.Parameters["@Password"].Value = password;


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }
        public int insert_command_student(coursework.Student[] student)
        {
            string key = student[0].Name;
            string stmt = "INSERT INTO [dbo].[student] ([name],[surname],[lastname],[birthday],[gender],[base_learn],[stud_number],[stud_debt],[note]) VALUES (@Name , @Surname, @Lastname, @Birthday, @Gender, @Base_Learn, @Stud_Number, @Stud_Debt, @Note)";
            SqlCommand cmd = new SqlCommand(stmt, Con);
            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@Lastname", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@Birthday", SqlDbType.Date);
            cmd.Parameters.Add("@Gender", SqlDbType.Int);
            cmd.Parameters.Add("@Base_Learn", SqlDbType.Int);
            cmd.Parameters.Add("@Stud_Number", SqlDbType.Int);
            cmd.Parameters.Add("@Stud_Debt", SqlDbType.Int);
            cmd.Parameters.Add("@Note", SqlDbType.VarChar, 50);
            cmd.Parameters["@Name"].Value = student[0].Name;
            cmd.Parameters["@Surname"].Value = student[0].Surname;
            cmd.Parameters["@Lastname"].Value = student[0].LName;
            string check_date = Convert.ToString(student[0].DateD.ToString() + '.' + student[0].DateM.ToString() + '.' + student[0].DateY.ToString());
            cmd.Parameters["@Birthday"].Value = check_date;
            cmd.Parameters["@Gender"].Value = student[0].Gender;
            cmd.Parameters["@Base_Learn"].Value = student[0].L_base;
            cmd.Parameters["@Stud_Number"].Value = student[0].S_number;
            if (student[0].Score == "")
            {
                cmd.Parameters["@Stud_Debt"].Value = DBNull.Value;
            }
            else { cmd.Parameters["@Stud_Debt"].Value = student[0].Score; }

            cmd.Parameters["@Note"].Value = student[0].Note;
            cmd.ExecuteNonQuery();
            return 0;
        }



        public int insert_command_user(string login, string password)
        {
            string stmt = "INSERT INTO [dbo].[user] ([login],[password],[role]) VALUES (@Login , @Password, @Role)";
            SqlCommand cmd = new SqlCommand(stmt, Con);
            cmd.Parameters.Add("@Login", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 10);

            cmd.Parameters["@Login"].Value = login;
            cmd.Parameters["@Password"].Value = password;
            cmd.Parameters["@Role"].Value = "user";
            cmd.ExecuteNonQuery();
            return 0;
        }




















        public void delete_command_studentById(int id)
        {
            string stmt = "DELETE FROM [dbo].[student] WHERE id = " + id;
            SqlCommand cmd = new SqlCommand(stmt, Con);
            cmd.ExecuteNonQuery();
        }
        public void delete_command_All()
        {
            string stmt = "DELETE FROM [dbo].[student]";
            SqlCommand cmd = new SqlCommand(stmt, Con);
            cmd.ExecuteNonQuery();
        }
    }
}
