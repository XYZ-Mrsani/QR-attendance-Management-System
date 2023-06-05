using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupNo5
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter the Password");
            }
            else
            {
                if (comboBoxRole.SelectedIndex > -1)
                {
                    if (comboBoxRole.SelectedItem.ToString() == "Admin")
                    {
                        if (txtPassword.Text == "admin123")
                        {
                            StudentRegistration f1 = new StudentRegistration();
                            f1.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Password is incorrect.");
                        }
                    }
                    else
                    {
                        if (txtPassword.Text == "teacher123")
                        {
                            StudentRegistration f1 = new StudentRegistration();
                            f1.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Password is incorrect.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select hte role.");
                }
            }
        }
    }
}
