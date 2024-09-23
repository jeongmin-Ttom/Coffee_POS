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
    public partial class FIND : Form
    {
        string address = String.Format("SERVER = LOCALHOST; DATABASE = cafe; Uid = root; Pwd = dlwjdals12!");
        public FIND()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Find_Button_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(address))
                {
                    conn.Open();
                    string selectQuery = String.Format("SELECT * FROM LOGIN");
                    MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (Name.Text == reader["Name"].ToString())  
                        {
                            this.Hide();
                            MessageBox.Show((string)reader["ID"] + "\n" + reader["PWD"]);
                        }
                        else
                        {
                            MessageBox.Show("정보를 확인해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
