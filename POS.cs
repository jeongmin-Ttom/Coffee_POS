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
    public partial class POS : Form
    {
        string address = String.Format("SERVER = LOCALHOST; DATABASE = cafe; Uid = root; Pwd = dlwjdals12!");

        private void POS_Load(object sender, EventArgs e)   //  실시간으로 변하는 시간이 변하도록 시간 카운트
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)//  실시간으로 변하는 시간을 라벨에 표시해줌
        {
            Nowtime.Text = DateTime.Now.ToString("F");  // "F" <- 자세하게 날짜를 나타냄
        }
        // ----------------------------------------------주문관리 변수
        int hcount1 = 1;    //  각각 음료 선택 수량
        int hcount2 = 1;
        int hcount3 = 1;
        int hcount4 = 1;
        int hcount5 = 1;
        int icount1 = 1;
        int icount2 = 1;
        int icount3 = 1;
        int icount4 = 1;
        int icount5 = 1;

        int hmoney1 = 0;    //  각각 선택한 음료 가격
        int hmoney2 = 0;
        int hmoney3 = 0;
        int hmoney4 = 0;
        int hmoney5 = 0;
        int imoney1 = 0;
        int imoney2 = 0;
        int imoney3 = 0;
        int imoney4 = 0;
        int imoney5 = 0;

        int ccount1 = 1;    //  각각 디저트 선택 수량
        int ccount2 = 1;
        int ccount3 = 1;
        int bcount1 = 1;
        int bcount2 = 1;
        int bcount3 = 1;
        int bcount4 = 1;
        int bcount5 = 1;

        int cmoney1 = 0;    //  각각 선택한 디저트 가격
        int cmoney2 = 0;
        int cmoney3 = 0;
        int bmoney1 = 0;
        int bmoney2 = 0;
        int bmoney3 = 0;
        int bmoney4 = 0;
        int bmoney5 = 0;

        int totalmoney = 0; // 선택한 모든 가격
        int Chargemoney = 0;    // 거스름돈

        // ---------------------------------------------------재고관리 변수

        int bean = 50; // 50잔당1통(1KG) 소요, 1잔당 20g  // 초코라떼를 제외한 모든 음료에 들어감
        int milk = 100;  // 4잔당 1통(1L) 소요, 1잔당 250ml // 아메리카노를 제외한 모든 음료에 들어감
        int choco = 25; // 40잔당 1통(1L) 소요, 1잔당 25ml // 카페모카, 초코라떼에만 들어감
        int bean_gram = 1000; 
        int milk_gram = 1000;
        int choco_gram = 1000;

        int cake_1 = 30;
        int cake_2 = 30;
        int cake_3 = 30;
        int brownie = 20;
        int muffin = 20;
        int bagel = 20;
        int crople = 30;
        int Tiramisu = 30;

        //string[] ItemName = new string[] {"원두", "우유", "초코시럽", "딸기케익", "초코케익", "치즈케익",
        //                                  "브라우니", "머핀", "베이글", "크로플", "티라미슈"};

        // ---------------------------------------------------매출관리 변수
        int salesmoney = 0; // 매출금
        
        public POS()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Order_G.Show();
            this.Inventory_G.Hide();
            this.Sales_G.Hide();
            this.Drinklist.Show();
            this.Desertlist.Hide();
        }
        private void Order_B_Click(object sender, EventArgs e)  // 주문관리
        {
            this.Order_G.Show();
            this.Inventory_G.Hide();
            this.Sales_G.Hide();
        }
        
        private void Drink_B_Click(object sender, EventArgs e)  // 음료
        {
            this.Drinklist.Show();
            this.Desertlist.Hide();
        }
        private void Desert_B_Click(object sender, EventArgs e) // 디저트
        {
            this.Drinklist.Hide();
            this.Desertlist.Show();
        }

        private void HOT1_Click(object sender, EventArgs e) //  아메리카노(HOT)
        {
            int count = listView_order.Items.Count;
            hmoney1 += 4000;
            bean_gram -= 20;
            if(bean_gram == 0)
            {
                bean -= 1;
                MessageBox.Show("원두 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bean_gram = 1000;
                if(bean == 10)
                    MessageBox.Show("원두 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (hcount1 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == HOT1.Text)
                    {
                        listView_order.Items[i].Focused = true;     // 해당 항목이 포커스 되어있는지
                        listView_order.Items[i].Selected = true;    // 해당 항목이 선택 되어있는지
                        listView_order.FocusedItem.SubItems[0].Text = HOT1.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(hcount1);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(hmoney1);
                        hcount1++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.HOT1.Text, Convert.ToString(hcount1), Convert.ToString(hmoney1) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                hcount1++;
            }
            totalmoney += 4000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void HOT2_Click(object sender, EventArgs e) //  카페라떼(HOT)
        {
            int count = listView_order.Items.Count;
            hmoney2 += 4500;
            bean_gram -= 20;
            milk_gram -= 250;
            if (bean_gram == 0)
            {
                bean -= 1;
                MessageBox.Show("원두 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bean_gram = 1000;
                if (bean == 10)
                    MessageBox.Show("원두 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if(milk_gram == 0)
            {
                milk -= 1;
                MessageBox.Show("우유 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                milk_gram = 1000;
                if (milk == 10)
                    MessageBox.Show("우유 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (hcount2 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == HOT2.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = HOT2.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(hcount2);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(hmoney2);
                        hcount2++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.HOT2.Text, Convert.ToString(hcount2), Convert.ToString(hmoney2) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                hcount2++;
            }
            totalmoney += 4500;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void HOT3_Click(object sender, EventArgs e) //  카푸치노(HOT)
        {
            int count = listView_order.Items.Count;
            hmoney3 += 4500;
            bean_gram -= 20;
            milk_gram -= 250;
            if (bean_gram == 0)
            {
                bean -= 1;
                MessageBox.Show("원두 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bean_gram = 1000;
                if (bean == 10)
                    MessageBox.Show("원두 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (milk_gram == 0)
            {
                milk -= 1;
                MessageBox.Show("우유 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                milk_gram = 1000;
                if (milk == 10)
                    MessageBox.Show("우유 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (hcount3 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == HOT3.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = HOT3.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(hcount3);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(hmoney3);
                        hcount3++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.HOT3.Text, Convert.ToString(hcount3), Convert.ToString(hmoney3) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                hcount3++;
            }
            totalmoney += 4500;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void HOT4_Click(object sender, EventArgs e) //  카페모카(HOT)
        {
            int count = listView_order.Items.Count;
            hmoney4 += 4500;
            bean_gram -= 20;
            milk_gram -= 250;
            choco_gram -= 25;
            if (bean_gram == 0)
            {
                bean -= 1;
                MessageBox.Show("원두 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bean_gram = 1000;
                if (bean == 10)
                    MessageBox.Show("원두 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (milk_gram == 0)
            {
                milk -= 1;
                MessageBox.Show("우유 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                milk_gram = 1000;
                if (milk == 10)
                    MessageBox.Show("우유 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (choco_gram == 0)
            {
                choco -= 1;
                MessageBox.Show("초코시럽 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                choco_gram = 1000;
                if (choco == 5)
                    MessageBox.Show("초코시럽 5통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (hcount4 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == HOT4.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = HOT4.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(hcount4);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(hmoney4);
                        hcount4++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.HOT4.Text, Convert.ToString(hcount4), Convert.ToString(hmoney4) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                hcount4++;
            }
            totalmoney += 4500;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void HOT5_Click(object sender, EventArgs e) //  초코라떼(HOT)
        {
            int count = listView_order.Items.Count;
            hmoney5 += 5000;
            milk_gram -= 250;
            choco_gram -= 25;
            if (milk_gram == 0)
            {
                milk -= 1;
                MessageBox.Show("우유 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                milk_gram = 1000;
                if (milk == 10)
                    MessageBox.Show("우유 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (choco_gram == 0)
            {
                choco -= 1;
                MessageBox.Show("초코시럽 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                choco_gram = 1000;
                if (choco == 5)
                    MessageBox.Show("초코시럽 5통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (hcount5 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == HOT5.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = HOT5.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(hcount5);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(hmoney5);
                        hcount5++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.HOT5.Text, Convert.ToString(hcount5), Convert.ToString(hmoney5) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                hcount5++;
            }
            totalmoney += 5000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void ICE1_Click(object sender, EventArgs e) //  아메리카노(ICE)
        {
            int count = listView_order.Items.Count;
            imoney1 += 4500;
            bean_gram -= 20;
            if (bean_gram == 0)
            {
                bean -= 1;
                MessageBox.Show("원두 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bean_gram = 1000;
                if (bean == 10)
                    MessageBox.Show("원두 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (icount1 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == ICE1.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = ICE1.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(icount1);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(imoney1);
                        icount1++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.ICE1.Text, Convert.ToString(icount1), Convert.ToString(imoney1) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                icount1++;
            }
            totalmoney += 4500;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void ICE2_Click(object sender, EventArgs e) //  카페라떼(ICE)
        {
            int count = listView_order.Items.Count;
            imoney2 += 5000;
            bean_gram -= 20;
            milk_gram -= 250;
            if (bean_gram == 0)
            {
                bean -= 1;
                MessageBox.Show("원두 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bean_gram = 1000;
                if (bean == 10)
                    MessageBox.Show("원두 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (milk_gram == 0)
            {
                milk -= 1;
                MessageBox.Show("우유 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                milk_gram = 1000;
                if (milk == 10)
                    MessageBox.Show("우유 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (icount2 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == ICE2.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = ICE2.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(icount2);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(imoney2);
                        icount2++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.ICE2.Text, Convert.ToString(icount2), Convert.ToString(imoney2) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                icount2++;
            }
            totalmoney += 5000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void ICE3_Click(object sender, EventArgs e) //  카푸치노(ICE)
        {
            int count = listView_order.Items.Count;
            imoney3 += 5000;
            bean_gram -= 20;
            milk_gram -= 250;
            if (bean_gram == 0)
            {
                bean -= 1;
                MessageBox.Show("원두 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bean_gram = 1000;
                if (bean == 10)
                    MessageBox.Show("원두 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (milk_gram == 0)
            {
                milk -= 1;
                MessageBox.Show("우유 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                milk_gram = 1000;
                if (milk == 10)
                    MessageBox.Show("우유 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (icount3 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == ICE3.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = ICE3.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(icount3);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(imoney3);
                        icount3++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.ICE3.Text, Convert.ToString(icount3), Convert.ToString(imoney3) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                icount3++;
            }
            totalmoney += 5000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void ICE4_Click(object sender, EventArgs e) //  카페모카(ICE)
        {
            int count = listView_order.Items.Count;
            imoney4 += 5000;
            bean_gram -= 20;
            milk_gram -= 250;
            choco_gram -= 25;
            if (bean_gram == 0)
            {
                bean -= 1;
                MessageBox.Show("원두 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bean_gram = 1000;
                if (bean == 10)
                    MessageBox.Show("원두 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (milk_gram == 0)
            {
                milk -= 1;
                MessageBox.Show("우유 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                milk_gram = 1000;
                if (milk == 10)
                    MessageBox.Show("우유 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (choco_gram == 0)
            {
                choco -= 1;
                MessageBox.Show("초코시럽 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                choco_gram = 1000;
                if (choco == 5)
                    MessageBox.Show("초코시럽 5통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (icount4 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == ICE4.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = ICE4.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(icount4);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(imoney4);
                        icount4++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.ICE4.Text, Convert.ToString(icount4), Convert.ToString(imoney4) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                icount4++;
            }
            totalmoney += 5000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void ICE5_Click(object sender, EventArgs e) //  초코라떼(ICE)
        {
            int count = listView_order.Items.Count;
            imoney5 += 5500;
            milk_gram -= 250;
            choco_gram -= 25;
            if (milk_gram == 0)
            {
                milk -= 1;
                MessageBox.Show("우유 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                milk_gram = 1000;
                if (milk == 10)
                    MessageBox.Show("우유 10통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (choco_gram == 0)
            {
                choco -= 1;
                MessageBox.Show("초코시럽 1통 사용하였습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                choco_gram = 1000;
                if (choco == 5)
                    MessageBox.Show("초코시럽 5통 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (icount5 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == ICE5.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = ICE5.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(icount5);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(imoney5);
                        icount5++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var coffee = new string[] { this.ICE5.Text, Convert.ToString(icount5), Convert.ToString(imoney5) };
                var lvi = new ListViewItem(coffee);
                this.listView_order.Items.Add(lvi);
                icount5++;
            }
            totalmoney += 5500;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }
        private void cake1_Click(object sender, EventArgs e)    // 딸기케익
        {
            int count = listView_order.Items.Count;
            cmoney1 += 7000;
            cake_1 -= 1;
            if(cake_1 == 10)
                MessageBox.Show("딸기케익이 10개 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (ccount1 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == cake1.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = cake1.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(ccount1);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(cmoney1);
                        ccount1++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var desert = new string[] { this.cake1.Text, Convert.ToString(ccount1), Convert.ToString(cmoney1) };
                var lvi = new ListViewItem(desert);
                this.listView_order.Items.Add(lvi);
                ccount1++;
            }
            totalmoney += 7000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void cake2_Click(object sender, EventArgs e)    // 초코케익
        {
            int count = listView_order.Items.Count;
            cmoney2 += 7000;
            cake_2 -= 1;
            if (cake_2 == 10)
                MessageBox.Show("초코케익이 10개 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (ccount2 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == cake2.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = cake2.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(ccount2);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(cmoney2);
                        ccount2++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var desert = new string[] { this.cake2.Text, Convert.ToString(ccount2), Convert.ToString(cmoney2) };
                var lvi = new ListViewItem(desert);
                this.listView_order.Items.Add(lvi);
                ccount2++;
            }
            totalmoney += 7000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void cake3_Click(object sender, EventArgs e)    // 치즈케익
        {
            int count = listView_order.Items.Count;
            cmoney3 += 7000;
            cake_3 -= 1;
            if (cake_3 == 10)
                MessageBox.Show("치즈케익이 10개 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (ccount3 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == cake3.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = cake3.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(ccount3);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(cmoney3);
                        ccount3++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var desert = new string[] { this.cake3.Text, Convert.ToString(ccount3), Convert.ToString(cmoney3) };
                var lvi = new ListViewItem(desert);
                this.listView_order.Items.Add(lvi);
                ccount3++;
            }
            totalmoney += 7000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void bread1_Click(object sender, EventArgs e)    // 브라우니
        {
            int count = listView_order.Items.Count;
            bmoney1 += 4000;
            brownie -= 1;
            if (brownie == 10)
                MessageBox.Show("브라우니가 10개 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (bcount1 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == bread1.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = bread1.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(bcount1);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(bmoney1);
                        bcount1++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var desert = new string[] { this.bread1.Text, Convert.ToString(bcount1), Convert.ToString(bmoney1) };
                var lvi = new ListViewItem(desert);
                this.listView_order.Items.Add(lvi);
                bcount1++;
            }
            totalmoney += 4000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void bread2_Click(object sender, EventArgs e)    // 머핀
        {
            int count = listView_order.Items.Count;
            bmoney2 += 3000;
            muffin -= 1;
            if (muffin == 10)
                MessageBox.Show("머핀이 10개 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (bcount2 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == bread2.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = bread2.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(bcount2);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(bmoney2);
                        bcount2++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var desert = new string[] { this.bread2.Text, Convert.ToString(bcount2), Convert.ToString(bmoney2) };
                var lvi = new ListViewItem(desert);
                this.listView_order.Items.Add(lvi);
                bcount2++;
            }
            totalmoney += 3000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void bread3_Click(object sender, EventArgs e)    // 베이글
        {
            int count = listView_order.Items.Count;
            bmoney3 += 3500;
            bagel -= 1;
            if (bagel == 10)
                MessageBox.Show("베이글이 10개 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (bcount3 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == bread3.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = bread3.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(bcount3);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(bmoney3);
                        bcount1++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var desert = new string[] { this.bread3.Text, Convert.ToString(bcount3), Convert.ToString(bmoney3) };
                var lvi = new ListViewItem(desert);
                this.listView_order.Items.Add(lvi);
                bcount3++;
            }
            totalmoney += 3500;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void bread4_Click(object sender, EventArgs e)    // 크로플
        {
            int count = listView_order.Items.Count;
            bmoney4 += 5000;
            crople -= 1;
            if (crople == 10)
                MessageBox.Show("크로플이 10개 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (bcount4 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == bread4.Text)
                    {
                        listView_order.Items[i].Focused = true;
                        listView_order.Items[i].Selected = true;
                        listView_order.FocusedItem.SubItems[0].Text = bread4.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(bcount4);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(bmoney4);
                        bcount4++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var desert = new string[] { this.bread4.Text, Convert.ToString(bcount4), Convert.ToString(bmoney4) };
                var lvi = new ListViewItem(desert);
                this.listView_order.Items.Add(lvi);
                bcount4++;
            }
            totalmoney += 5000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }

        private void bread5_Click(object sender, EventArgs e)    // 티라미슈
        {
            int count = listView_order.Items.Count;
            bmoney5 += 7000;
            Tiramisu -= 1;
            if (Tiramisu == 10)
                MessageBox.Show("티라미슈가 10개 남았습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (bcount5 >= 2)
            {
                for (int i = 0; i < count; i++)
                {
                    if (listView_order.Items[i].SubItems[0].Text == bread5.Text)
                    {
                        listView_order.Items[i].Focused = true;     // 해당 항목이 포커스 되어있는지
                        listView_order.Items[i].Selected = true;    // 해당 항목이 선택 되어있는지
                        listView_order.FocusedItem.SubItems[0].Text = bread5.Text;
                        listView_order.FocusedItem.SubItems[1].Text = Convert.ToString(bcount5);
                        listView_order.FocusedItem.SubItems[2].Text = Convert.ToString(bmoney5);
                        bcount5++;
                        listView_order.Items[i].Selected = false;
                    }
                }
            }
            else
            {
                var desert = new string[] { this.bread5.Text, Convert.ToString(bcount5), Convert.ToString(bmoney5) };
                var lvi = new ListViewItem(desert);
                this.listView_order.Items.Add(lvi);
                bcount5++;
            }
            totalmoney += 7000;
            Allpay_B.Text = Convert.ToString(totalmoney);
        }
        private void AllC_B_Click(object sender, EventArgs e)   // 모두 취소는 카드 혹은 현금 결제 후 코드와 동일
        {
            hcount1 = 1; hcount2 = 1; hcount3 = 1; hcount4 = 1; hcount5 = 1;
            icount1 = 1; icount2 = 1; icount3 = 1; icount4 = 1; icount5 = 1;

            hmoney1 = 0; hmoney2 = 0; hmoney3 = 0; hmoney4 = 0; hmoney5 = 0;
            imoney4 = 0; imoney3 = 0; imoney1 = 0; imoney2 = 0; imoney5 = 0;

            totalmoney = 0;
            Chargemoney = 0;

            Allpay_B.Text = "";
            Receive_B.Text = "";
            Charge_B.Text = "";
            listView_order.Items.Clear();
        }
        private void SelectC_B_Click(object sender, EventArgs e)
        {
            int count = listView_order.Items.Count;
            if (listView_order.FocusedItem.SubItems[0].Text == HOT1.Text)
            {
                totalmoney -= hmoney1;
                hcount1 = 1;
                hmoney1 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == HOT2.Text)
            {
                totalmoney -= hmoney2;
                hcount2 = 1;
                hmoney2 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == HOT3.Text)
            {
                totalmoney -= hmoney3;
                hcount3 = 1;
                hmoney3 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == HOT4.Text)
            {
                totalmoney -= hmoney4;
                hcount4 = 1;
                hmoney4 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == HOT5.Text)
            {
                totalmoney -= hmoney5;
                hcount5 = 1;
                hmoney5 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == ICE1.Text)
            {
                totalmoney -= imoney1;
                icount1 = 1;
                imoney1 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == ICE2.Text)
            {
                totalmoney -= imoney2;
                icount2 = 1;
                imoney2 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == ICE3.Text)
            {
                totalmoney -= imoney3;
                icount3 = 1;
                imoney3 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == ICE4.Text)
            {
                totalmoney -= imoney4;
                icount4 = 1;
                imoney4 = 0;
            }
            else if (listView_order.FocusedItem.SubItems[0].Text == ICE5.Text)
            {
                totalmoney -= imoney5;
                icount5 = 1;
                imoney5 = 0;
            }
            if (totalmoney == 0)
            {
                Allpay_B.Text = "";
                listView_order.Items.Remove(listView_order.FocusedItem);
            }
            else
            {
                Allpay_B.Text = Convert.ToString(totalmoney);
                listView_order.Items.Remove(listView_order.FocusedItem);    // 컨트롤 부분을 제거하는거
            }
        }

        private void Card_B_Click(object sender, EventArgs e)   // 카드결제
        {
            if (Receive_B.Text != "")
            {
                MessageBox.Show("현금을 받았습니다!", "경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("카드로 결제 하시겠습니까.?", "확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                salesmoney += totalmoney;
                TodaySales.Text = Convert.ToString(salesmoney);

                hcount1 = 1; hcount2 = 1; hcount3 = 1; hcount4 = 1; hcount5 = 1;
                icount1 = 1; icount2 = 1; icount3 = 1; icount4 = 1; icount5 = 1;

                hmoney1 = 0; hmoney2 = 0; hmoney3 = 0; hmoney4 = 0; hmoney5 = 0;
                imoney4 = 0; imoney3 = 0; imoney1 = 0; imoney2 = 0; imoney5 = 0;

                totalmoney = 0;
                Chargemoney = 0;

                Allpay_B.Text = "";
                Receive_B.Text = "";
                listView_order.Items.Clear();
            }
            else
            {

            }
        }

        private void Cash_B_Click(object sender, EventArgs e)   // 현금결제
        {
            if (Receive_B.Text == "")
                MessageBox.Show("받은 현금이 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            else if (MessageBox.Show("현금으로 결제 하시겠습니까.?", "확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                salesmoney += totalmoney;
                TodaySales.Text = Convert.ToString(salesmoney);

                hcount1 = 1; hcount2 = 1; hcount3 = 1; hcount4 = 1; hcount5 = 1;
                icount1 = 1; icount2 = 1; icount3 = 1; icount4 = 1; icount5 = 1;

                hmoney1 = 0; hmoney2 = 0; hmoney3 = 0; hmoney4 = 0; hmoney5 = 0;
                imoney4 = 0; imoney3 = 0; imoney1 = 0; imoney2 = 0; imoney5 = 0;

                totalmoney = 0;
                Chargemoney = 0;

                Allpay_B.Text = "";
                Receive_B.Text = "";
                Charge_B.Text = "";
                listView_order.Items.Clear();
            }
            else
            {

            }
        }

        private void Num0_Click(object sender, EventArgs e) //  숫자버튼
        {
            Button Numbutton = (Button)sender;
            SetNum(Numbutton.Text);
        }
        public void SetNum(string num)  //  모든 숫자버튼 사용 함수
        {
            if (Receive_B.Text == "")
                Receive_B.Text = num;
            else
                Receive_B.Text += num;
        }

        private void NumEnter_Click(object sender, EventArgs e) //  E 버튼(enter)
        {
            Chargemoney = Convert.ToInt32(Receive_B.Text) - totalmoney;
            Charge_B.Text = Convert.ToString(Chargemoney);
            if (Chargemoney < 0)
            {
                MessageBox.Show("금액이 부족합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void NumClear_Click(object sender, EventArgs e) // CE 버튼(초기화)
        {
            Chargemoney = 0;
            Receive_B.Text = "";
            Charge_B.Text = "";
        }

        // ----------------------------------------------------------------------------------

        private void Inventory_B_Click(object sender, EventArgs e)  //  재고 관리
        {
            this.Order_G.Hide();
            this.Inventory_G.Show();
            this.Sales_G.Hide();
        }
        private void listView_Inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            inventoryN.Text = listView_Inventory.FocusedItem.SubItems[0].Text;
            inventoryQ.Text = listView_Inventory.FocusedItem.SubItems[1].Text;
            inventorynote.Text = listView_Inventory.FocusedItem.SubItems[2].Text;
        }

        private void inventoryS_B_Click(object sender, EventArgs e)
        {
            int count = listView_Inventory.Items.Count;

            for (int i = 0; i < count; i++)
            {
                if (inventoryS.Text == listView_Inventory.Items[i].SubItems[0].Text)
                {
                    inventoryN.Text = listView_Inventory.Items[i].SubItems[0].Text;
                    inventoryQ.Text = listView_Inventory.Items[i].SubItems[1].Text;
                    inventorynote.Text = listView_Inventory.Items[i].SubItems[2].Text;
                    break;
                }
            }
        }

        private void inventoryC_B_Click(object sender, EventArgs e)
        {
            int count = listView_Inventory.Items.Count;

            for (int i = 0; i < count; i++)
            {
                if (inventoryN.Text == listView_Inventory.Items[i].SubItems[0].Text)
                {
                    listView_Inventory.Items[i].SubItems[0].Text = inventoryN.Text;
                    listView_Inventory.Items[i].SubItems[1].Text = inventoryQ.Text;
                    listView_Inventory.Items[i].SubItems[2].Text = inventorynote.Text;
                }
                else if(inventoryN.Text == "")
                {
                    MessageBox.Show("수정할 내용이 없습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

            }
        }

        // ----------------------------------------------------------------------------------

        private void Sales_B_Click(object sender, EventArgs e)  //  매출 관리
        {
            this.Order_G.Hide();
            this.Inventory_G.Hide();
            this.Sales_G.Show();
        }

        private void Calendar1_DateSelected(object sender, DateRangeEventArgs e)    // 캘린더 날짜 선택 시 날짜 표시
        {
            DateTime date = Calendar1.SelectionStart;
            Date.Text = date.ToString("yyyy-MM-dd");
        }
        private void listView_Sales_SelectedIndexChanged(object sender, EventArgs e)
        {
            Date.Text = listView_Sales.FocusedItem.SubItems[0].Text;
            sale.Text = listView_Sales.FocusedItem.SubItems[1].Text;
        }

        private void Sales_Search_Click(object sender, EventArgs e)
        {
            int count = listView_Sales.Items.Count;

            for (int i = 0; i < count; i++)
            {
                if (Date.Text == listView_Sales.Items[i].SubItems[0].Text)
                      sale.Text = listView_Sales.Items[i].SubItems[1].Text;

            }
        }
        private void Sales_Plus_Click(object sender, EventArgs e)   // 매출 추가(영업 종료 전 추가하면서 하루 매출 리스트에 표시 후 매출금 초기화)
        {
            DateTime date = DateTime.Now;   //  매출 리스트에 오늘 날짜 표시
            //var sales = new string[] { date.ToString("yyyy-MM-dd"), Convert.ToString(salesmoney) }; // 리스트에 오늘 날짜와 매출금 표시
            //var lvi = new ListViewItem(sales);
            //this.listView_Sales.Items.Add(lvi);
            //salesmoney = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(address))
                {
                    conn.Open();

                    string insertQuery = string.Format("INSERT INTO sales (day, sales) VALUES ('" + date.ToString("yyyy-MM-dd") + "', '" + Convert.ToString(salesmoney) + "')");
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);

                    if (cmd.ExecuteNonQuery() != 1)
                        MessageBox.Show("Failed to insert data.");

                    selectTable();

                    salesmoney = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void selectTable()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(address)) // address는 mysql 주소(SERVER, DATABASE, Uid, Pwd)
                {
                    conn.Open();

                    string selectQuery = string.Format("SELECT * FROM sales");
                    MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //listView_Sales.Clear();

                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = reader["day"].ToString();
                        item.SubItems.Add(reader["sales"].ToString());

                        listView_Sales.Items.Add(item);
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
