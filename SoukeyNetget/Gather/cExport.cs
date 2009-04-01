using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using Interop.Excel;
using System.IO;

///���ܣ����ݵ��� �ı� excel
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
///�޶�����
namespace SoukeyNetget.Gather
{
    class cExport
    {
        public cExport()
        {
        }

        ~cExport()
        {
        }

        //����Excel
        public bool ExportExcel(string FileName,string TaskName,System.Data.DataTable gData)
        {
            // ����Ҫʹ�õ�Excel ����ӿ�
            // ����Application ����,�˶����ʾ����Excel ����

            Interop.Excel.Application excelApp = null;
            Interop.Excel.Workbook workBook=null;
            Interop.Excel.Worksheet ws = null;
            Interop.Excel.Range r;
            int row = 1; 
            int cell = 1;

            try
            {
                //��ʼ�� Application ���� excelApp
                excelApp = new Interop.Excel.Application();
                workBook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = (Worksheet)workBook.Worksheets[1];

                // ���������������Ϊ "Task Management"
                ws.Name = TaskName;


                // �������ݱ��е�������
                for (int i = 0; i < gData.Columns.Count; i++)
                {

                    ws.Cells[row, cell] = gData.Columns[i].ColumnName; 
                    r = (Range)ws.Cells[row, cell];
                    ws.get_Range(r, r).HorizontalAlignment = Interop.Excel.XlVAlign.xlVAlignCenter;

                    cell++;

                }

                // ������,��������ͼ��¼�������Ӧ��Excel ��Ԫ��
                for (int i = 0; i < gData.Rows.Count; i++)
                {
                    for (int j = 0; j < gData.Columns.Count; j++)
                    {
                        ws.Cells[i + 2, j+1] = gData.Rows[i][j];
                        Range rg = (Range)ws.get_Range(ws.Cells[i + 2, j + 1], ws.Cells[i + 2, j + 1]);
                        rg.EntireColumn.ColumnWidth = 20;
                        rg.NumberFormatLocal = "@";
                    }
                }

                workBook.SaveCopyAs(FileName);
                workBook.Saved = true;

            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
                excelApp.UserControl = false;
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                GC.Collect();
                
            }

            return true;

        }

        //�����ı��ļ�
        public  bool ExportTxt(string FileName, System.Data.DataTable gData)
        {
            FileStream  myStream = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            string tempStr = "";

            try
            {
                //д���� 
                for (int i = 0; i < gData.Columns.Count; i++)
                {
                    str += "\t";
                    str += gData.Columns[i].ColumnName;
                }
              
                sw.WriteLine(str);

                //д���� 
                for (int i = 0; i < gData.Rows.Count; i++)
                {
                    for (int j = 0; j < gData.Columns.Count; j++)
                    {

                        tempStr += "\t";
                        tempStr += gData.Rows[i][j];
                    }
                    sw.WriteLine(tempStr);
                    tempStr = "";
                }
               

                sw.Close();
                myStream.Close();

            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }


            return true;
        }
    }
}
