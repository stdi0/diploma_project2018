using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Solution
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        /*static void Run()
        { 
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //Run();
            }
        }*/

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Authorization());

        }
    }
}
