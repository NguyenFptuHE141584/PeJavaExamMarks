using PeJavaExamMarksProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeJavaExamMarks_Project
{
    public partial class FormLogin : Form
    {

        public FormLogin()
        {
            InitializeComponent();
        }


        private void btLogin_Click(object sender, EventArgs e)
        {
            if (!CheckLogin())
            {
                return;
            }
            else
            {
                using (var context = new PeJavaExamMarksContext())
                {
                    Account account = context.Accounts.FirstOrDefault
                    (account => account.UserName.Equals(txUserName.Text)
                                           && account.Password.Equals(txPassword.Text));
                    FormManage formManage = new FormManage(account);
                    this.Hide();
                    formManage.Show();
                }
            }
        }

        public bool CheckLogin()
        {

            if (String.IsNullOrEmpty(txUserName.Text))
            {
                MessageBox.Show("User name is not null");
                return false;
            }
            else if (String.IsNullOrEmpty(txPassword.Text))
            {
                MessageBox.Show("Password is not null");
                return false;
            }
            using (var context = new PeJavaExamMarksContext())
            {
                Account account = context.Accounts.FirstOrDefault
                    (account => account.UserName.Equals(txUserName.Text)
                                                && account.Password.Equals(txPassword.Text));
                if (account == null)
                {
                    MessageBox.Show("User name or password not correct");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
