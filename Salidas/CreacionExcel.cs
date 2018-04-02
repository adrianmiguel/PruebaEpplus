using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using OfficeOpenXml;
using System.Xml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;

namespace PruebaEPPlus
{
    class CreacionExcel
    {
        public void ExcelXlSX(DirectoryInfo RutaExcel, DataTable Datos, string CadenaConexion)
        {
            ConexionBd conexion = new ConexionBd();

            var NuevoArchivo = new FileInfo(RutaExcel + @"\Excel1.xlsx");

            if (NuevoArchivo.Exists)
            {
                NuevoArchivo.Delete();
                NuevoArchivo = new FileInfo(RutaExcel + @"\Excel1.xlsx");
            }

            using (var package = new ExcelPackage(NuevoArchivo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Poliza");

                //Agregamos las Columnas
                for (int i = 0; i  < Datos.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = Datos.Columns[i].ColumnName;
                }

                for (int m = 0; m < Datos.Rows.Count; m++)
                {
                    for (int n = 0; n < Datos.Columns.Count; n++)
                    {
                        int Columna = n + 1;
                        int Fila = 2 + m;
                        string Dato = Datos.Rows[m].ItemArray[n].ToString();
                        worksheet.Cells[Fila, Columna].Value = Dato;

                        if (Datos.Columns[n].ColumnName == "Caso")
                        {
                            string Ruta =  conexion.ConsultarRutaArchivo(CadenaConexion, Dato).Rows[0]["RutaArchivo"].ToString();

                            ClienteFTP clienteFTP = new ClienteFTP();
                            clienteFTP.CargarArchivo();

                        }
                    }
                }

                //for (int m = 0; m < Datos.Columns.Count; m++)
                //{

                //    //if (Datos.Columns[m].ColumnName == "Caso")
                //    //{
                //    //    ClienteFTP clienteFTP = new ClienteFTP();
                //    //    clienteFTP.CargarArchivo();
                //    //}

                //    for (int n = 0; n < Datos.Rows.Count; n++)
                //    {
                //        int Columna = m + 1;
                //        int Fila = 2 + n;
                //        if (m == 0)
                //        {
                //            worksheet.Cells[Fila, Columna].Value = Convert.ToInt32(Datos.Rows[n].ItemArray[m]);
                //        }
                //        else
                //        {
                //            worksheet.Cells[Fila, Columna].Value = Datos.Rows[n].ItemArray[m];
                //        }
                //    }
                //}

                worksheet.Cells["A2:A4"].Style.Numberformat.Format = "0";
                worksheet.Cells["B2:B4"].Style.Numberformat.Format = "0";
                worksheet.Cells["D2:D4"].Style.Numberformat.Format = "0";
                worksheet.Cells["O2:O4"].Style.Numberformat.Format = "#,##0";
                worksheet.Cells["E2:E4"].Style.Numberformat.Format = "@";
                worksheet.Cells["M2:M4"].Style.Numberformat.Format = "yyyy/MM/dd";//"dd/MM/yyyy";


                //Add some items...
                //worksheet.Cells["A2"].Value = 12001;
                //worksheet.Cells["B2"].Value = "Nails";
                //worksheet.Cells["C2"].Value = 37;
                //worksheet.Cells["D2"].Value = 3.99;

                //worksheet.Cells["A3"].Value = 12002;
                //worksheet.Cells["B3"].Value = "Hammer";
                //worksheet.Cells["C3"].Value = 5;
                //worksheet.Cells["D3"].Value = 12.10;

                //worksheet.Cells["A4"].Value = 12003;
                //worksheet.Cells["B4"].Value = "Saw";
                //worksheet.Cells["C4"].Value = 12;
                //worksheet.Cells["D4"].Value = 15.37;

                ////Add a formula for the value-column
                ////worksheet.Cells["E2:E4"].Formula = "C2*D2";

                ////Ok now format the values;
                ////using (var range = worksheet.Cells[1, 1, 1, 5])
                ////{
                ////    range.Style.Font.Bold = true;
                ////    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                ////    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                ////    range.Style.Font.Color.SetColor(Color.White);
                ////}

                //worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                //worksheet.Cells["A5:E5"].Style.Font.Bold = true;

                ////worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
                ////worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
                ////worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";

                ////Create an autofilter for the range
                ////worksheet.Cells["A1:E4"].AutoFilter = true;

                //worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text

                ////There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                ////For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                ////you want to use the result of a formula in your program.
                ////worksheet.Calculate();

                ////worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                //// lets set the header text 
                //worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
                //// add the page number to the footer plus the total number of pages
                //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                //    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                //// add the sheet name to the footer
                //worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                //// add the file path to the footer
                //worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                //worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                //worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                //// Change the sheet view to show it in page layout mode
                //worksheet.View.PageLayoutView = true;

                //// set some document properties
                ////package.Workbook.Properties.Title = "Invertory";
                ////package.Workbook.Properties.Author = "Jan Källman";
                ////package.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";

                //// set some extended property values
                ////package.Workbook.Properties.Company = "AdventureWorks Inc.";

                //// set some custom property values
                ////package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                ////package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");
                //// save our new workbook and we are done!
                package.Save();
            }
        }

        public void ExcelXLS(DirectoryInfo RutaExcel, DataTable Datos, string CadenaConexion)
        {
            ConexionBd conexion = new ConexionBd();

            var NuevoArchivo = new FileInfo(RutaExcel + @"\xlExcel7Pass2.xls");

            if (NuevoArchivo.Exists)
            {
                NuevoArchivo.Delete();
                NuevoArchivo = new FileInfo(RutaExcel + @"\xlExcel7Pass2.xls");
            }
                Datos.TableName = "Polizas";

                int inColumn = 0, inRow = 0;

                System.Reflection.Missing Default = System.Reflection.Missing.Value;

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);

                //Create Excel WorkSheet
                Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add(Default, excelWorkBook.Sheets[excelWorkBook.Sheets.Count], 1, Default);
                excelWorkSheet.Name = "Poliza";//Name worksheet

                //Write Column Name
                for (int i = 0; i < Datos.Columns.Count; i++)
                    excelWorkSheet.Cells[1, i + 1] = Datos.Columns[i].ColumnName;//.ToUpper();

                //Write Rows
                for (int m = 0; m < Datos.Rows.Count; m++)
                {
                    for (int n = 0; n < Datos.Columns.Count; n++)
                    {
                        inColumn = n + 1;
                        inRow = 2 + m;//1 + 2 + m;
                        excelWorkSheet.Cells[inRow, inColumn] = Datos.Rows[m].ItemArray[n].ToString();
                        if (m % 2 == 0)
                            excelWorkSheet.get_Range("A" + inRow.ToString(), "W" + inRow.ToString()).Interior.Color = System.Drawing.ColorTranslator.FromHtml("#DAA520");
                    }
                }

                ////Excel Header
                //OfficeExcel.Range cellRang = excelWorkSheet.get_Range("A1", "O1");
                //cellRang.Merge(false);
                //cellRang.Interior.Color = System.Drawing.Color.Blue;
                //cellRang.Font.Color = System.Drawing.Color.Black;
                //cellRang.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                //cellRang.VerticalAlignment = OfficeExcel.XlVAlign.xlVAlignCenter;
                //cellRang.Font.Size = 16;
                //excelWorkSheet.Cells[1, 1] = "Greate Novels Of All Time";

                //Style table column names
                Excel.Range cellRang = excelWorkSheet.get_Range("A1", "W1");
                cellRang.Font.Bold = true;
                cellRang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                cellRang.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#00008B");
                cellRang = excelWorkSheet.get_Range("X1", "Z1");
                cellRang.Font.Bold = true;
                cellRang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                cellRang.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#DAA520");
                excelWorkSheet.get_Range("F4").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //Formate price column
                excelWorkSheet.get_Range("O2").EntireColumn.NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"; //.NumberFormat = "0.00";
                excelWorkSheet.get_Range("O2").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //Auto fit columns
                excelWorkSheet.Columns.AutoFit();

                //Delete First Page
                excelApp.DisplayAlerts = false;
                Microsoft.Office.Interop.Excel.Worksheet lastWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets[1];
                lastWorkSheet.Delete();
                excelApp.DisplayAlerts = true;

                //Set Defualt Page
                (excelWorkBook.Sheets[1] as Excel._Worksheet).Activate();

            excelWorkBook.SaveAs(NuevoArchivo, Excel.XlFileFormat.xlExcel7, Default, Default, false, Default, Excel.XlSaveAsAccessMode.xlNoChange, Default, Default, Default, Default, Default);
            //excelWorkBook.SaveAs(NuevoArchivo, OfficeExcel.XlFileFormat.xlExcel5, Default, Default, false, Default, OfficeExcel.XlSaveAsAccessMode.xlNoChange, Default, Default, Default, Default, Default);
            //excelWorkBook.SaveAs(NuevoArchivo, OfficeExcel.XlFileFormat.xlExcel9795, Default, Default, false, Default, OfficeExcel.XlSaveAsAccessMode.xlNoChange, Default, Default, Default, Default, Default);
            excelWorkBook.Close();
                excelApp.Quit();            
        }

        public void XLS(DirectoryInfo RutaExcel, DataTable Datos)
        {
            //BackgroundWorker bw = sender as BackgroundWorker;

            Excel.Application excel = new Excel.Application();
            Excel._Workbook libro = null;
            Excel._Worksheet hoja = null;
            Excel.Range rango = null;

            try
            {
                //creamos un libro nuevo y la hoja con la que vamos a trabajar
                libro = (Excel._Workbook)excel.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                hoja = (Excel._Worksheet)libro.Worksheets.Add();
                hoja.Name = "EJEMPLO";
                ((Excel.Worksheet)excel.ActiveWorkbook.Sheets["Hoja1"]).Delete();   //Borramos la hoja que crea en el libro por defecto

                //Montamos las cabeceras 
                montaCabeceras(1, ref hoja, Datos);

                //Rellenamos las celdas
                int inColumn = 0, inRow = 0;
                for (int m = 0; m < Datos.Rows.Count; m++)
                {
                    for (int n = 0; n < Datos.Columns.Count; n++)
                    {
                        inColumn = n + 1;
                        inRow = 2 + m;//1 + 2 + m;
                        hoja.Cells[inRow, inColumn] = Datos.Rows[m].ItemArray[n].ToString();
                        if (m % 2 == 0)
                            hoja.get_Range("A" + inRow.ToString(), "W" + inRow.ToString()).Interior.Color = System.Drawing.ColorTranslator.FromHtml("#DAA520");
                    }
                }

                libro.Saved = true;
                libro.SaveAs(Environment.CurrentDirectory + @"\Ejemplo.xlsx");  // Si es un libro nuevo

                libro.Close();
                releaseObject(libro);

                excel.UserControl = false;
                excel.Quit();
                releaseObject(excel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error en creación/actualización del Ejemplo");

                libro.Saved = true;
                libro.SaveAs(Environment.CurrentDirectory + @"\Ejemplo.xlsx");
                //    libro.Save();

                libro.Close();
                releaseObject(libro);

                excel.UserControl = false;
                excel.Quit();
                releaseObject(excel);

                System.Threading.Thread.Sleep(2000);
            }

            
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void montaCabeceras(int fila, ref Excel._Worksheet hoja, DataTable Datos)
        {
            try
            {
                //Excel.Range rango;

                for (int i = 0; i < Datos.Columns.Count; i++)
                    hoja.Cells[1, i + 1] = Datos.Columns[i].ColumnName;//.ToUpper();

                //** Montamos el título en la línea 1 **
                //hoja.Cells[1, 2] = "EJEMPLO DE CREACIÓN DE UN ARCHIVO EXCEL";

                //** Montamos las cabeceras en la línea 3 **
                //hoja.Cells[3, 2] = "Código";
                //hoja.Cells[3, 3] = "Descripción";
                //hoja.Cells[3, 4] = "Observaciones";

                //Ponemos borde a las celdas
                //rango = hoja.Range["B3", "D3"];
                //rango.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                ////Centramos los textos
                //rango = hoja.Rows[3];
                //rango.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                ////Modificamos los anchos de las columnas
                //rango = hoja.Columns[1];
                //rango.ColumnWidth = 1;
                //rango = hoja.Columns[2];
                //rango.ColumnWidth = 10;
                //rango = hoja.Columns[3];
                //rango.ColumnWidth = 20;
                //rango = hoja.Columns[4];
                //rango.ColumnWidth = 20;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error de redondeo", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
