using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using Interop.Excel;
using System.IO;

///功能：数据导出 文本 excel
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
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

        //导出Excel
        public bool ExportExcel(string FileName,string TaskName,System.Data.DataTable gData)
        {
            // 定义要使用的Excel 组件接口
            // 定义Application 对象,此对象表示整个Excel 程序

            Interop.Excel.Application excelApp = null;
            Interop.Excel.Workbook workBook=null;
            Interop.Excel.Worksheet ws = null;
            Interop.Excel.Range r;
            int row = 1; 
            int cell = 1;

            try
            {
                //初始化 Application 对象 excelApp
                excelApp = new Interop.Excel.Application();
                workBook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = (Worksheet)workBook.Worksheets[1];

                // 命名工作表的名称为 "Task Management"
                ws.Name = TaskName;


                // 遍历数据表中的所有列
                for (int i = 0; i < gData.Columns.Count; i++)
                {

                    ws.Cells[row, cell] = gData.Columns[i].ColumnName; 
                    r = (Range)ws.Cells[row, cell];
                    ws.get_Range(r, r).HorizontalAlignment = Interop.Excel.XlVAlign.xlVAlignCenter;

                    cell++;

                }

                // 创建行,把数据视图记录输出到对应的Excel 单元格
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

        //导出文本文件
        public  bool ExportTxt(string FileName, System.Data.DataTable gData)
        {
            FileStream  myStream = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            string tempStr = "";

            try
            {
                //写标题 
                for (int i = 0; i < gData.Columns.Count; i++)
                {
                    str += "\t";
                    str += gData.Columns[i].ColumnName;
                }
              
                sw.WriteLine(str);

                //写内容 
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
