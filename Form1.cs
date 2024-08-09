using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Coffee_POS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Login_Click(object sender, EventArgs e)    // MySql DB 활용해서 만들어보기
        {
            //try
            //{
            //    MySqlConnection conn = new MySqlConnection("SERVER = LOCALHOST; DATABASE = LOGIN_DB; Uid = root; PASSWORD = dlwjdals12!;");
            //    conn.Open();

            //    string insertquery = "INSERT INTO LOGIN_DB VALUES ('"+ this.ID_Box.Text + "', '" + this.PW_Box.Text + "')";

            //    MySqlCommand cmd = new MySqlCommand(insertquery, conn);

            //    MySqlDataReader reader = cmd.ExecuteReader();


            //    int login_status = 0;

            //    while (reader.Read())
            //    {
            //        string ID = reader["ID"].ToString();
            //        string PW = reader["PassWord"].ToString();

            //        login_status = 1;

            //        if (login_status == 1)
            //        {
            //            cmd.ExecuteNonQuery();
            //            this.Hide();
            //            new POS().Show();
            //            break;
            //        }
            //        else
            //            MessageBox.Show("정보를 확인해 주세요", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}


            if (ID_Box.Text == "dlwjdals" && PW_Box.Text == "wjdals12")
            {
                this.Hide(); 
                new POS().Show();
            }
            if (ID_Box.Text != "dlwjdals")
                MessageBox.Show("아이디를 확인해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (PW_Box.Text != "wjdals12")
                MessageBox.Show("비밀번호를 확인해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
