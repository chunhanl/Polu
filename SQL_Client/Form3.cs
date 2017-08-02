using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Client
{
    public partial class Form3 : Form
    {

        public int selected_1;
        public int selected_2;
        public int selected_3;
        public string shape;
        public string size;


        public Form3()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            selected_1 = listBox1.SelectedIndex;
            selected_2 = listBox2.SelectedIndex;
            switch (selected_2)
            {
                case -1:
                    size = "notgiven";
                    break;
                case 0:
                    size = "notgiven";
                    break;
                case 1:
                    size = "less30";
                    break;
                case 2:
                    size = "30";
                    break;
                case 3:
                    size = "50";
                    break;
                case 4:
                    size = "60_99";
                    break;
                case 5:
                    size = "100";
                    break;
                case 6:
                    size = "more100";
                    break;
                case 7:
                    size = "no_main";
                    break;                     
            }

            selected_3 = listBox3.SelectedIndex;

            if(selected_1 == 1)
            {
                switch (selected_3)
                {
                    case -1:
                        shape = "none";
                        break;
                    case 0:
                        shape = "none";
                        break;
                    case 1:
                        shape = "circle";
                        break;
                    case 2:
                        shape = "oval";
                        break;
                    case 3:
                        shape = "drop";
                        break;
                    case 4:
                        shape = "god";
                        break;
                    case 5:
                        shape = "eye";
                        break;
                    case 6:
                        shape = "other";
                        break;
                }

            }
            else if (selected_1 == 3 || selected_1 == 5)
            {
                shape = "none";
            }
            else
            {
                switch (selected_3)
                {
                    case -1:
                        shape = "none";
                        break;
                    case 0:
                        shape = "none";
                        break;
                    case 1:
                        shape = "circle";
                        break;
                    case 2:
                        shape = "oval";
                        break;
                    case 3:
                        shape = "drop";
                        break;
                    case 4:
                        shape = "heart";
                        break;
                    case 5:
                        shape = "square";
                        break;
                    case 6:
                        shape = "other";
                        break;
                }

            }


            if (checkEmpty())
            {
                MessageBox.Show("Empty choice, please select");
            }
            else
            {
                this.Close();
                DialogResult = DialogResult.OK;
            }

        }

        private bool checkEmpty()
        {
            switch (selected_1)
            {
                case -1:
                    return true;
                case 0:
                    return (selected_2 == -1 || selected_3 == -1);
                case 1:
                    return (/*selected_2 == -1 */ selected_3 == -1);
                case 2:
                    return (selected_2 == -1 ||selected_3 == -1);
                case 3:
                    return false;               
                case 4:
                    return (selected_2 == -1 || selected_3 == -1);
                case 5:
                    return false;
                default:
                    return true;
            }
        }

  


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            listBox2.SelectedIndex = -1;
            listBox3.SelectedIndex = -1;

            int index = listBox1.SelectedIndex;
            if (index == 1 )
            {
                listBox2.Hide();
                listBox3.Show();
                this.listBox3.Items.Clear();
                this.listBox3.Items.AddRange(new object[] {
                "未知",
                "圓形",
                "橢圓",
                "水滴",
                "神像",
                "馬眼",
                "其他"});
            }

            else if(index ==3 || index == 5)
            {
                listBox2.Hide();
                listBox3.Hide();
            }
            else
            {
                listBox2.Show();
                listBox3.Show();
                this.listBox3.Items.Clear();
                this.listBox3.Items.AddRange(new object[] {
                "未知",
                "圓形",
                "橢圓",
                "水滴",
                "心形",
                "方形",
                "其他"});
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
