﻿using System;
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
        string address = String.Format("SERVER = LOCALHOST; DATABASE = cafe; Uid = root; Pwd = dlwjdals12!");
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Login_Click(object sender, EventArgs e)    // MySql DB 활용해서 만들어보기
        {
            //if (ID_Box.Text == "dlwjdals" && PW_Box.Text == "wjdals12")
            //{
            //    this.Hide(); 
            //    new POS().Show();
            //}
            //if (ID_Box.Text != "dlwjdals")
            //    MessageBox.Show("아이디를 확인해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //else if (PW_Box.Text != "wjdals12")
            //    MessageBox.Show("비밀번호를 확인해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            try
            {
                using (MySqlConnection conn = new MySqlConnection(address))
                {
                    conn.Open();
                    string selectQuery = String.Format("SELECT * FROM LOGIN");
                    MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                    cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        if(ID_Box.Text == reader["ID"].ToString() && PW_Box.Text == reader["PWD"].ToString())
                        {
                            this.Hide();
                            new POS().Show();
                        }
                        else
                        {
                            MessageBox.Show("정보를 확인해주세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Find_Button_Click(object sender, EventArgs e)  // 찾기 버튼
        {
            new FIND().Show();
        }

    }
}
