using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Infrastructure.Utils.ToolsUtilities
{
    public class Functions
    {
        /*public Functions()
        {
            //
            // TODO: Add constructor logic here
            //
        }*/
        public static DataTable ExcelToDataTableBaseSCTR(string filename)
        {
            DataTable dt = new DataTable();
            try
            {

                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filename, false))
                {

                    WorkbookPart workbookPart = doc.WorkbookPart;

                    IEnumerable<Sheet> sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();

                    string relationshipId = sheets.First().Id.Value;

                    WorksheetPart worksheetPart = (WorksheetPart)doc.WorkbookPart.GetPartById(relationshipId);

                    Worksheet workSheet = worksheetPart.Worksheet;

                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();

                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dt.Columns.Add(GetCellValue(doc, cell));
                    }

                    foreach (Row row in rows)
                    {
                        //               'this will also include your header row...

                        DataRow tempRow = dt.NewRow();

                        var contadorIndex = 0;
                        for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        {

                            //tempRow[i] = GetCellValue(doc, row.Descendants<Cell>().ElementAt(i));
                            Cell cell = row.Descendants<Cell>().ElementAt(i);
                            //int actualCellIndex = CellReferenceToIndex(cell);
                            tempRow[contadorIndex] = GetCellValue(doc, cell);
                            contadorIndex++;
                        }
                        dt.Rows.Add(tempRow);
                    }
                }



                dt.Rows.RemoveAt(0);

                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            try
            {

                if ((cell.CellValue) == null)
                {
                    return "";
                }

                string value = cell.CellValue.InnerXml;

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
                    return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                }
                else
                {
                    return value;
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string ConverDecimalFront(string valor)
        // este funcion hace que las cantidad menores a 10, no muestren decimales y se vean como enteros
        {
            if (decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal numero))
            {
                if (valor == null)
                {
                    return "0.0";
                }
                else
                {
                    string format = numero < 10 ? "0.##" : "0,0.00";
                    return numero.ToString(format, CultureInfo.InvariantCulture);
                }
            }
            else
            {
                return valor;
            }

        }
    }

}
