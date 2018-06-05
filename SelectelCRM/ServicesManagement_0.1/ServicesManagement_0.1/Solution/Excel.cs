using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Solution
{
    //Взаимодействие с Microsoft Office Excel
    class Excel
    {
        //Объект приложения Excel
        static public Ex._Application exApp;
        //Объект рабочей книги
        static public Ex._Workbook workBook;
        //Объект рабочей таблицы
        static public Ex._Worksheet workSheet;

        static public void Open(string path, bool visible)
        {
            exApp = new Ex.Application();
            exApp.Visible = visible;
            exApp.DisplayAlerts = false;
            workBook = exApp.Workbooks.Open(path);
            workSheet = workBook.ActiveSheet;
        }

        static public void Close()
        {
            Process[] processes = Process.GetProcessesByName("EXCEL"); //Список Excel процессов
            foreach (Process proc in processes)
            {
                proc.Kill();
            }
        }

    }
}
