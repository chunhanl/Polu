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

    public partial class Form1 : Form
    {
        //data getter
        public string accountStr()
        {
            return this.text_account.Text;
        }

        public string passwordStr()
        {
            return this.text_password.Text;
        }


        //constructor
        public Form1()
        {
            InitializeComponent();
        }

   

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void login_button_Click(object sender, EventArgs e)
        {
       
            //for testing 
            bool account_not_null = (this.text_account.Text.Length != 0);
            bool password_not_null = (this.text_password.Text.Length != 0);

            if(account_not_null && password_not_null)
            {
                this.Hide();
                DialogResult = DialogResult.OK;
                
            }
            else
            {
                MessageBox.Show("Please enter your Account and Password");
            }
        }

        private void text_account_TextChanged(object sender, EventArgs e)
        {
            //try to check if there has available account
        }

        private void text_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_account_Click(object sender, EventArgs e)
        {

        }
    }
}
