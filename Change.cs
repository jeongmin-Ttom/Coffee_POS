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
    public partial class Change : Form
    {
        string address = String.Format("SERVER = LOCALHOST; DATABASE = cafe; Uid = root; Pwd = dlwjdals12!");
        public Change()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(address))
                {
                    conn.Open();
                    string updateQuery = String.Format("UPDATE LOGIN SET PWD = '" + PWD2.Text + "' WHERE PWD = '" + PWD1.Text + "'");
                    MySqlCommand cmd = new MySqlCommand(updateQuery, conn);

                    if(cmd.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("이전 비밀번호를 확인해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("비밃번호가 변경되었습니다.");
                        this.Close();
                        PWD1.Text = "";
                        PWD2.Text = "";
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
