using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace TimesheetHelper
{
    class ExcelOperator
    {
        private Excel.Application mApplication;
        private Excel.Workbook mWorkbook;
        private Excel.Worksheet mWorksheet;

        private string mFileName;

        public ExcelOperator(string fileName)
        {
            this.mFileName = fileName;
            mApplication = new Excel.Application();

            if (System.IO.File.Exists(fileName))
            {
                mWorkbook = mApplication.Workbooks.Open(fileName);
                //TODO(batuhan): open a copy to work on.
                //mApplication.NewWorkbook;
            }
            else
            {
                mWorkbook = mApplication.Workbooks.Add();
            }
            mWorksheet = mWorkbook.Sheets[1];
        }


        public void SaveChanges()
        {

        }

        ~ExcelOperator()
        {
            this.mWorksheet?.SaveAs(mFileName);
        }
    }
}
