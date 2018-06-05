using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Solution
{
    //Форма авторизации
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        //Дескриптор формы
        static public Authorization form;

        //Событие при загрузке формы, выполняет подключение к базе данных
        private void Authorization_Load(object sender, EventArgs e)
        {
            form = this;
            DBConnection.Connect();
        }

        //Кнопка входа, вызывает процедуру авторизации по введенному логину и паролю
        private void signinBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection.Authorization(login.Text, password.Text);
                if (DBConnection.id != null)
                {
                    this.Hide();
                    Form1 mainForm = new Form1();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Введенный логин или пароль неверный!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
