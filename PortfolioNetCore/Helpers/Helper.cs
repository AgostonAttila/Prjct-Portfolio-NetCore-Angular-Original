using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using PortfolioNetCore.Core.Model;

namespace PortfolioNetCore
{
    class Helper
    {
        static public int _actualYear = DateTime.Now.Year;

        //public static void GetExcelExportTeletrader(string excelFileName, string txtFile)
        //{

        //    if (!String.IsNullOrEmpty(excelFileName) && !String.IsNullOrEmpty(txtFile))
        //    {
        //        Excel.Application xlApplication = null;
        //        Excel.Workbook xlWorkBook = null;
        //        Excel.Worksheet xlWorkSheet = null;
        //        object misValue = System.Reflection.Missing.Value;

        //        try
        //        {
        //            xlApplication = new Excel.ApplicationClass();
        //            xlWorkBook = xlApplication.Workbooks.Add(misValue);
        //            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

        //            string[] fundArray = File.ReadAllLines(txtFile);
        //            if (fundArray != null)
        //            {
        //                progressBar.Maximum = fundArray.Count() * 2;
        //                progressBar.Step = 1;
        //                progressBar.Value = 0;
        //                progressBar.ForeColor = Color.SeaGreen;

        //                //Adatok leszedése    
        //                List<Fund> fundList = GetDatasFromTeletrader(progressBar, fundArray);

        //                //Task<List<Fund>> parallelTask = Task.Factory.StartNew(() => { List<Fund> fundList = GetDatasFromTeletrader(progressBar, fundArray); return fundList; });
        //                //parallelTask.Wait();

        //                //Excel feltöltése
        //                FillContent(xlWorkSheet, progressBar, fundList);
        //            }
        //            //Mentés                                       
        //            xlWorkBook.SaveAs(excelFileName, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show("Error\n" + e.Message);
        //        }
        //        finally
        //        {
        //            progressBar.ForeColor = Color.Blue;
        //            if (xlWorkBook != null)
        //                xlWorkBook.Close(true, misValue, misValue);
        //            if (xlApplication != null)
        //                xlApplication.Quit();

        //            if (xlWorkSheet != null)
        //                releaseObject(xlWorkSheet);
        //            if (xlWorkBook != null)
        //                releaseObject(xlWorkBook);
        //            if (xlApplication != null)
        //                releaseObject(xlApplication);

        //        }
        //    }
        //    else
        //        MessageBox.Show("Input parameters arempty\n");
        //}

        public static List<Fund> GetTeletraderFundList(string filePath, List<Fund> fundList = null)
        {

            //if (File.Exists(filePath))
            //{

                string[] fundArray = (!String.IsNullOrWhiteSpace(filePath)) ? fundArray = File.ReadAllLines(filePath) : fundArray = fundList.Select(p => p.Url).ToArray();

                if (fundArray != null)
                {
                    //Adatok leszedése    
                    fundList = GetDatasFromTeletrader(fundArray);

                    //Task<List<Fund>> parallelTask = Task.Factory.StartNew(() => { List<Fund> fundList = GetDatasFromTeletrader(progressBar, fundArray); return fundList; });
                    //parallelTask.Wait();

                    fundList = fundList.Where(p => p != null).ToList();

                    return fundList;
                }
            //}

            return null;
        }

        public static List<FundDetail> GetTeletraderFundDetailList(string filePath)
        {

            if (File.Exists(filePath))
            {

                string[] fundArray = File.ReadAllLines(filePath);
                if (fundArray != null)
                {

                    //Adatok leszedése    
                    List<Fund> fundList = GetDatasFromTeletrader(fundArray);
                    fundList = fundList.Where(p => p != null).ToList();


                    List<FundDetail> fundDeatilList = new List<FundDetail>();

                    for (int i = 0; i < fundList.Count; i++)
                    {
                        fundDeatilList.Add(
                            new FundDetail
                            {
                                ISINNumber = fundList[i].ISINNumber,
                                Name = fundList[i].Name,
                                Currency = fundList[i].Currency,
                                Management = fundList[i].Management,
                                Focus = fundList[i].Focus,
                                Type = fundList[i].Type,
                                Url = fundList[i].Url
                            }
                            );
                    }

                    return fundDeatilList;
                }
            }

            return null;
        }

        public static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }


        //Excel műveletek

        /*
       /// <summary>
       /// Fill header
       /// </summary>
       /// <param name="xlWorkSheet"></param>
       public static void FillHeader(Excel.Worksheet xlWorkSheet)
       {
           FillExcelCellBlack(xlWorkSheet, "Alap neve", 1, 1);  //xlWorkSheet.Cells[1, 1] = "Alap neve"; 
           FillExcelCellBlack(xlWorkSheet, "ISIN", 1, 2); //xlWorkSheet.Cells[1, 2] = "ISIN";
           FillExcelCellBlack(xlWorkSheet, "Valuta", 1, 3); //xlWorkSheet.Cells[1, 3] = "Valuta";
           FillExcelCellBlack(xlWorkSheet, "Menedzsment", 1, 4);  //xlWorkSheet.Cells[1, 4] = "Menedzsment";
           FillExcelCellBlack(xlWorkSheet, "Fókusz", 1, 5);  //xlWorkSheet.Cells[1, 5] = "Fókusz";
                                                             //xlWorkSheet.Cells[1, 6] = RegexType;
           FillExcelCellBlack(xlWorkSheet, "1 éves", 1, 6);  //xlWorkSheet.Cells[1, 6] = "1 éves";
           FillExcelCellBlack(xlWorkSheet, "3 éves", 1, 7);  //xlWorkSheet.Cells[1, 7] = "3 éves";
           FillExcelCellBlack(xlWorkSheet, "5 éves", 1, 8);  //xlWorkSheet.Cells[1, 8] = "5 éves";
           FillExcelCellBlack(xlWorkSheet, "Kezdettől", 1, 9);  //xlWorkSheet.Cells[1, 9] = "Kezdettől";
           FillExcelCellBlack(xlWorkSheet, "", 1, 10);
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 9).ToString(), 1, 11); //xlWorkSheet.Cells[1, 11] = "2006";
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 8).ToString(), 1, 12); //xlWorkSheet.Cells[1, 12] = "2007";
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 7).ToString(), 1, 13); //xlWorkSheet.Cells[1, 13] = "2008";
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 6).ToString(), 1, 14); //xlWorkSheet.Cells[1, 14] = "2009";
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 5).ToString(), 1, 15); //xlWorkSheet.Cells[1, 15] = "2010";
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 4).ToString(), 1, 16); //xlWorkSheet.Cells[1, 16] = "2011";
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 3).ToString(), 1, 17); //xlWorkSheet.Cells[1, 17] = "2012";
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 2).ToString(), 1, 18); //xlWorkSheet.Cells[1, 18] = "2013";
           FillExcelCellBlack(xlWorkSheet, (_actualYear - 1).ToString(), 1, 19); //xlWorkSheet.Cells[1, 19] = "2014";
           FillExcelCellBlack(xlWorkSheet, "YTD", 1, 20);  //xlWorkSheet.Cells[1, 20] = "YTD";

           FillExcelCellBlack(xlWorkSheet, "Volatilitás YTD", 1, 21);  //xlWorkSheet.Cells[1, 21] = "Volatilitás YTD";
           FillExcelCellBlack(xlWorkSheet, "Volatilitás 6 hónap", 1, 22);  //xlWorkSheet.Cells[1, 22] = "Volatilitás 6 hónap";
           FillExcelCellBlack(xlWorkSheet, "Volatilitás 1 év", 1, 23);  //xlWorkSheet.Cells[1, 23] = "Volatilitás 1 év";
           FillExcelCellBlack(xlWorkSheet, "Volatilitás 3 év", 1, 24);  //xlWorkSheet.Cells[1, 24] = "Volatilitás 3 év";
           FillExcelCellBlack(xlWorkSheet, "Volatilitás 5 év", 1, 25);  //xlWorkSheet.Cells[1, 25] = "Volatilitás 5 év";

           FillExcelCellBlack(xlWorkSheet, "Sharpe ráta  YTD", 1, 26);  //xlWorkSheet.Cells[1, 26] = "Sharpe ráta  YTD";
           FillExcelCellBlack(xlWorkSheet, "Sharpe ráta 6 hónap", 1, 27);  //xlWorkSheet.Cells[1, 27] = "Sharpe ráta  6 hónap";
           FillExcelCellBlack(xlWorkSheet, "Sharpe ráta 1 év", 1, 28);  //xlWorkSheet.Cells[1, 28] = "Sharpe ráta  1 év";
           FillExcelCellBlack(xlWorkSheet, "Sharpe ráta 3 év", 1, 29);  //xlWorkSheet.Cells[1, 29] = "Sharpe ráta  3 év";
           FillExcelCellBlack(xlWorkSheet, "Sharpe ráta 5 év", 1, 30);  //xlWorkSheet.Cells[1, 30] = "Sharpe ráta  5 év";

           FillExcelCellBlack(xlWorkSheet, "Legjobb hónap YTD", 1, 31); // xlWorkSheet.Cells[1, 31] = "Legjobb hónap YTD";
           FillExcelCellBlack(xlWorkSheet, "Legjobb hónap 6 hónap", 1, 32);  //xlWorkSheet.Cells[1, 32] = "Legjobb hónap 6 hónap";
           FillExcelCellBlack(xlWorkSheet, "Legjobb hónap 1 év", 1, 33);  //xlWorkSheet.Cells[1, 33] = "Legjobb hónap 1 év";
           FillExcelCellBlack(xlWorkSheet, "Legjobb hónap 3 év", 1, 34);  //xlWorkSheet.Cells[1, 34] = "Legjobb hónap 3 év";
           FillExcelCellBlack(xlWorkSheet, "Legjobb hónap 5 év", 1, 35);  //xlWorkSheet.Cells[1, 35] = "Legjobb hónap 5 év";

           FillExcelCellBlack(xlWorkSheet, "Legrosszabb hónap YTD", 1, 36); // xlWorkSheet.Cells[1, 36] = "Legrosszabb hónap YTD";
           FillExcelCellBlack(xlWorkSheet, "Legrosszabb hónap 6 hónap", 1, 37); // xlWorkSheet.Cells[1, 37] = "Legrosszabb hónap 6 hónap";
           FillExcelCellBlack(xlWorkSheet, "Legrosszabb hónap 1 év", 1, 38); // xlWorkSheet.Cells[1, 38] = "Legrosszabb hónap 1 év";
           FillExcelCellBlack(xlWorkSheet, "Legrosszabb hónap 3 év", 1, 39); // xlWorkSheet.Cells[1, 39] = "Legrosszabb hónap 3 év";
           FillExcelCellBlack(xlWorkSheet, "Legrosszabb hónap 5 év", 1, 40); // xlWorkSheet.Cells[1, 40] = "Legrosszabb hónap 5 év";

           FillExcelCellBlack(xlWorkSheet, "Maximális veszteség YTD", 1, 41);  //xlWorkSheet.Cells[1, 41] = "Maximális veszteség  YTD";
           FillExcelCellBlack(xlWorkSheet, "Maximális veszteség 6 hónap", 1, 42);  //xlWorkSheet.Cells[1, 42] = "Maximális veszteség  6 hónap";
           FillExcelCellBlack(xlWorkSheet, "Maximális veszteség 1 év", 1, 43); // xlWorkSheet.Cells[1, 43] = "Maximális veszteség  1 év";
           FillExcelCellBlack(xlWorkSheet, "Maximális veszteség 3 év", 1, 44); // xlWorkSheet.Cells[1, 44] = "Maximális veszteség  3 év";
           FillExcelCellBlack(xlWorkSheet, "Maximális veszteség 5 év", 1, 45);  //xlWorkSheet.Cells[1, 45] = "Maximális veszteség  5 év";

           FillExcelCellBlack(xlWorkSheet, "Túlteljesítés YTD", 1, 46);  //xlWorkSheet.Cells[1, 46] = "Túlteljesítés  YTD";
           FillExcelCellBlack(xlWorkSheet, "Túlteljesítés 6 hónap", 1, 47);  //xlWorkSheet.Cells[1, 47] = "Túlteljesítés  6 hónap";
           FillExcelCellBlack(xlWorkSheet, "Túlteljesítés 1 év", 1, 48);  //xlWorkSheet.Cells[1, 48] = "Túlteljesítés  1 év";
           FillExcelCellBlack(xlWorkSheet, "Túlteljesítés 3 év", 1, 49);  //xlWorkSheet.Cells[1, 49] = "Túlteljesítés  3 év";
           FillExcelCellBlack(xlWorkSheet, "Túlteljesítés 5 év", 1, 50);  //xlWorkSheet.Cells[1, 50] = "Túlteljesítés  5 év";

       }

       /// <summary>
       /// Cella kitöltése
       /// </summary>
       /// <param name="xlWorkSheet"></param>
       /// <param name="sourceString"></param>
       /// <param name="columnString"></param>
       /// <param name="columnNumber"></param>
       /// <param name="i"></param>
       public static void FillExcelCellValue(Excel.Worksheet xlWorkSheet, string sourceString, int columnNumber, int i)
       {
           if (sourceString != null)
           {
               Excel.Range formatRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[i, columnNumber], xlWorkSheet.Cells[i, columnNumber]);
               xlWorkSheet.Cells[i, columnNumber] = sourceString;

               if (sourceString.Contains("-"))
                   formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.IndianRed);
               else if (sourceString.Contains("+"))
                   formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
               else
                   formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

           }
       }

       /// <summary>
       /// Cella kitöltése szürkével
       /// </summary>
       /// <param name="xlWorkSheet"></param>
       /// <param name="sourceString"></param>
       /// <param name="columnString"></param>
       /// <param name="columnNumber"></param>
       /// <param name="i"></param>
       public static void FillExcelCellWhiteOrGrey(Excel.Worksheet xlWorkSheet, string sourceString, int columnNumber, int i)
       {
           xlWorkSheet.Cells[i, columnNumber] = sourceString;
           if (i % 2 == 0)
           {
               //formatRange = xlWorkSheet.get_Range(columnString + i, columnString + i);
               Excel.Range formatRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[i, columnNumber], xlWorkSheet.Cells[i, columnNumber]);
               formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
           }
       }

       /// <summary>
       /// Cella kitöltése feketével
       /// </summary>
       /// <param name="xlWorkSheet"></param>
       /// <param name="sourceString"></param>
       /// <param name="rowNumber"></param>
       /// <param name="columnNumber"></param>
       public static void FillExcelCellBlack(Excel.Worksheet xlWorkSheet, string sourceString, int rowNumber, int columnNumber)
       {
           xlWorkSheet.Cells[rowNumber, columnNumber] = sourceString;
           Excel.Range formatRange = (Excel.Range)xlWorkSheet.Cells[rowNumber, columnNumber];
           formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
           formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
       }

       /// <summary>
       /// Excel kitöltése
       /// </summary>
       /// <param name="xlWorkSheet"></param>
       /// <param name="progressBar"></param>
       /// <param name="fundList"></param>
       public static void FillContent(Excel.Worksheet xlWorkSheet, ProgressBar progressBar, List<Fund> fundList)
       {
           progressBar.ForeColor = Color.Blue;
           FillHeader(xlWorkSheet);

           int i = 0;
           for (int j = 0; j < fundList.Count; j++)
           {
               progressBar.Value += 1;
               i = j + 2;

               try
               {

                   if (fundList[j] != null)
                   {
                       List<string> textList = new List<string> { fundList[j].RegexFundName, fundList[j].RegexISISNNumber, fundList[j].RegexCurrency, fundList[j].RegexManagement, fundList[j].RegexFocus, fundList[j].RegexPerformance1Year, fundList[j].RegexPerformance3Year, fundList[j].RegexPerformance5Year, fundList[j].RegexPerformanceFromBeggining, " ", fundList[j].RegexPerformanceActualMinus9, fundList[j].RegexPerformanceActualMinus8, fundList[j].RegexPerformanceActualMinus7, fundList[j].RegexPerformanceActualMinus6, fundList[j].RegexPerformanceActualMinus5, fundList[j].RegexPerformanceActualMinus4, fundList[j].RegexPerformanceActualMinus3, fundList[j].RegexPerformanceActualMinus2, fundList[j].RegexPerformanceActualMinus1, fundList[j].RegexPerformanceYTD };

                       for (int k = 0; k < textList.Count; k++)
                       {
                           int column = k + 1;

                           if (column < 6)
                               FillExcelCellWhiteOrGrey(xlWorkSheet, textList[k], column, i);
                           else
                               FillExcelCellValue(xlWorkSheet, textList[k], column, i);
                       }

                       if (fundList[j].volatilityArray != null)
                       {
                           for (int k = 0; k < 5; k++)
                           {
                               string text = (fundList[j].volatilityArray[k].ToString().IndexOf("%") != -1) ? fundList[j].volatilityArray[k].ToString() : "";
                               FillExcelCellValue(xlWorkSheet, text, k + 21, i);
                           }
                       }
                       else
                       {
                           for (int k = 0; k < 5; k++)
                               FillExcelCellValue(xlWorkSheet, " ", k + 21, i);
                       }

                       if (fundList[j].sharpRateArray != null)
                       {
                           for (int k = 0; k < 5; k++)
                           {
                               string text = fundList[j].sharpRateArray[k].ToString();
                               FillExcelCellValue(xlWorkSheet, text, k + 26, i);
                           }
                       }
                       else
                       {
                           for (int k = 0; k < 5; k++)
                               FillExcelCellValue(xlWorkSheet, " ", k + 26, i);
                       }

                       if (fundList[j].bestMonthArray != null)
                       {
                           for (int k = 0; k < 5; k++)
                           {
                               string text = (fundList[j].bestMonthArray[k].ToString().IndexOf("%") != -1) ? fundList[j].bestMonthArray[k].ToString() : "";
                               FillExcelCellValue(xlWorkSheet, text, k + 31, i);
                           }

                       }
                       else
                       {
                           for (int k = 0; k < 5; k++)
                               FillExcelCellValue(xlWorkSheet, " ", k + 31, i);
                       }

                       if (fundList[j].worstMonthArray != null)
                       {
                           for (int k = 0; k < 5; k++)
                           {
                               string text = (fundList[j].worstMonthArray[k].ToString().IndexOf("%") != -1) ? fundList[j].worstMonthArray[k].ToString() : "";
                               FillExcelCellValue(xlWorkSheet, text, k + 36, i);
                           }
                       }
                       else
                       {
                           for (int k = 0; k < 5; k++)
                               FillExcelCellValue(xlWorkSheet, " ", k + 36, i);
                       }

                       if (fundList[j].maxLossArray != null)
                       {
                           for (int k = 0; k < 5; k++)
                           {
                               string text = (fundList[j].maxLossArray[k].ToString().IndexOf("%") != -1) ? fundList[j].maxLossArray[k].ToString() : "";
                               FillExcelCellValue(xlWorkSheet, text, k + 41, i);
                           }

                       }
                       else
                       {
                           for (int k = 0; k < 5; k++)
                               FillExcelCellValue(xlWorkSheet, " ", k + 41, i);
                       }

                       if (fundList[j].overFulFilmentArray != null)
                       {
                           for (int k = 0; k < 5; k++)
                           {
                               string text = (fundList[j].overFulFilmentArray[k].ToString().IndexOf("%") != -1) ? fundList[j].overFulFilmentArray[k].ToString() : "";
                               FillExcelCellValue(xlWorkSheet, text, k + 46, i);
                           }
                       }
                       else
                       {
                           for (int k = 0; k < 5; k++)
                               FillExcelCellValue(xlWorkSheet, " ", k + 46, i);
                       }

                   }
                   else
                   {
                       Excel.Range formatRange = xlWorkSheet.get_Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, 50]);
                       formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                   }
               }
               catch (Exception)
               {
               }
           }

           //Formatting
           Excel.Range range = xlWorkSheet.Range["A1", "E1"];
           range.EntireColumn.AutoFit();
       }
       */
        //Internetes műveletek

        /// <summary>
        ///  Adatok kinyerése
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="fundArray"></param>
        /// <returns></returns>
        public static List<Fund> GetDatasFromTeletrader(string[] fundArray)
        {

            List<Fund> fundList = new List<Fund>();
            //Parallel.For(0, fundArray.Count(), (k) =>
            for (int k = 0; k < fundArray.Count(); k++)
            {

                string sourceUrl = fundArray[k];
                string sourceString = "";

                if (!string.IsNullOrEmpty(sourceUrl) && sourceUrl.IndexOf("www.teletrader.com") != -1)
                {
                    //Weblap leszedése
                    sourceString = GetDataFromUrlWithoutDownload(sourceUrl.Trim());

                    string language = "";
                    List<string> sourcedStringList = new List<string>();
                    //if (!String.IsNullOrEmpty(GetPerformanceSumYearRegex(sourceString, "1 Év</th", 220)))
                    if (sourceString.IndexOf("1 Év</th") != -1)
                    {
                        language = "HU";
                        sourcedStringList = new List<string>() { "YTD</th", "1 Év</th", "3 Év</th", "5 Év</th", "Kezdettől</th", "Volatilitás", "Sharpe ráta", "Legjobb hónap", "Legrosszabb hónap", "Maximális veszteség", "Túlteljesítés" };
                    }
                    //else if (!String.IsNullOrEmpty(GetPerformanceSumYearRegex(sourceString, "1 Jahr</th", 220)))
                    else if (sourceString.IndexOf("1 Jahr</th") != -1)
                    {
                        language = "DE";
                        sourcedStringList = new List<string>() { "lfd. Jahr</th", "1 Jahr</th", "3 Jahre</th", "5 Jahre</th", "seit Beginn</th", "Volatilität", "Sharpe ratio", "Bester Monat", "Schlechtester Monat", "Maximaler Verlust", "Outperformance" };
                    }
                    //else if (!String.IsNullOrEmpty(GetPerformanceSumYearPointRegex(sourceString, "1 Year</th", 220)))
                    else if (sourceString.IndexOf("1 Year</th") != -1)
                    {
                        language = "EN";
                        sourcedStringList = new List<string>() { "YTD</th", "1 Year</th", "3 Years</th", "5 Years</th", "Since start</th", "Volatility", "Sharpe ratio", "Best month", "Worst month", "Maximum loss", "Outperformance" };
                    }

                    Fund actFund = new Fund();
                    if (language == "EN" || language == "HU" || language == "DE")
                    {
                        actFund.Currency = GetCurrencyRegex(sourceString, "cell-last fundCompany", 280);
                        actFund.Name = GetNameRegex(sourceString, "heading-detail", 150);
                        actFund.ISINNumber = GetISINRegex(sourceString, @"class=" + '"' + "isin", 100);
                        actFund.Management = GetManagementRegex(sourceString, "cell-last fundCompany", 100);
                        actFund.Focus = GetFocusRegex(sourceString, "investment-focus", 100);
                        actFund.Type = language == "EN" ? GetTypeRegexEnglish(sourceString, "yield", 50) : GetTypeRegex(sourceString, "yield", 50);

                        actFund.PerformanceYTD = language == "EN" ? GetPerformanceSumYearPointRegex(sourceString, sourcedStringList[0], 225) : GetPerformanceSumYearRegex(sourceString, sourcedStringList[0], 225);
                        actFund.Performance1Year = language == "EN" ? GetPerformanceSumYearPointRegex(sourceString, sourcedStringList[1], 220) : GetPerformanceSumYearRegex(sourceString, sourcedStringList[1], 220);
                        actFund.Performance3Year = language == "EN" ? GetPerformanceSumYearPointRegex(sourceString, sourcedStringList[2], 220) : GetPerformanceSumYearRegex(sourceString, sourcedStringList[2], 220);
                        actFund.Performance5Year = language == "EN" ? GetPerformanceSumYearPointRegex(sourceString, sourcedStringList[3], 220) : GetPerformanceSumYearRegex(sourceString, sourcedStringList[3], 220);
                        actFund.PerformanceFromBeggining = language == "EN" ? GetPerformanceFromBegginingPointRegex(sourceString, sourcedStringList[4], 225) : GetPerformanceFromBegginingRegex(sourceString, sourcedStringList[4], 225);

                        actFund.PerformanceActualMinus9 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 9).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 9).ToString() + "</th", 254);
                        actFund.PerformanceActualMinus8 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 8).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 8).ToString() + "</th", 254);
                        actFund.PerformanceActualMinus7 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 7).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 7).ToString() + "</th", 254);
                        actFund.PerformanceActualMinus6 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 6).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 6).ToString() + "</th", 254);
                        actFund.PerformanceActualMinus5 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 5).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 5).ToString() + "</th", 254);
                        actFund.PerformanceActualMinus4 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 4).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 4).ToString() + "</th", 254);
                        actFund.PerformanceActualMinus3 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 3).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 3).ToString() + "</th", 254);
                        actFund.PerformanceActualMinus2 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 2).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 2).ToString() + "</th", 254);
                        actFund.PerformanceActualMinus1 = language == "EN" ? GetYearsPointRegex(sourceString, (_actualYear - 1).ToString() + "</th", 254) : GetYearsRegex(sourceString, (_actualYear - 1).ToString() + "</th", 254);
                        //RegexPerformanceAverage = String.Format("{0:0.##}", GetAverage(RegexPerformance2006, RegexPerformance2007, RegexPerformance2008, RegexPerformance2009, RegexPerformance2010, RegexPerformance2011, RegexPerformance2012, RegexPerformance2013, RegexPerformance2014));
                        actFund.VolatilityArray = GetVolatility(sourceString, sourcedStringList[5], 180);
                        actFund.SharpRateArray = GetVolatility(sourceString, sourcedStringList[6], 180);
                        actFund.BestMonthArray = GetOthers(sourceString, sourcedStringList[7], 300);
                        actFund.WorstMonthArray = GetOthers(sourceString, sourcedStringList[8], 300);
                        actFund.MaxLossArray = GetOthers(sourceString, sourcedStringList[9], 300);
                        actFund.OverFulFilmentArray = GetOthers(sourceString, sourcedStringList[10], 300);

                        actFund.Url = sourceUrl;

                        fundList.Add(actFund);
                    }
                    else
                        fundList.Add(null);
                }
                else
                    fundList.Add(null);

            }
            //});
            return fundList;
        }

        /// <summary>
        /// Adatok leszedése weblapról
        /// </summary>
        /// <param name="urlAdress"></param>
        public static void GetDataFromUrlDownload(string urlAdress)
        {
            try
            {
                urlAdress = "http://www.teletrader.com/aegon-alfa-szarmaztatott-befektetesi-alap/funds/details/FU_5222";
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                webClient.DownloadFile(urlAdress, @"D:\localfile.txt");
            }
            catch { }

        }

        /// <summary>
        /// Adatok leszedése weblapról
        /// </summary>
        /// <param name="urlAdress"></param>
        /// <returns></returns>
        static public string GetDataFromUrlWithoutDownload(string urlAdress)
        {
            try
            {

                WebClient client = new WebClient();

                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                Stream data = client.OpenRead(urlAdress);
                StreamReader reader = new StreamReader(data);
                string resultString = reader.ReadToEnd();
                data.Close();
                reader.Close();

                return resultString;
            }
            catch (Exception e) { return null; }

        }

        /// <summary>
        /// Internet kapcsolat ellenőrzése
        /// </summary>
        /// <returns></returns>
        public static bool IsInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #region Teletrader
        static public string GetCurrencyRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;
                if (sourceText.IndexOf(searchedText) != -1)
                {
                    swapText = sourceText.Substring(sourceText.IndexOf(searchedText) + 25, cutLong);

                    Regex regData = new Regex(@"[A-Z]+");
                    Match matData = regData.Match(swapText);
                    string Data = matData.Value;
                    result = Data;
                    return result;
                }
            }
            catch
            {

            }
            return "";


        }

        static public string GetTypeRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                if (sourceText.LastIndexOf(searchedText) != -1)
                {
                    string swapText, result;

                    swapText = sourceText.Substring(sourceText.LastIndexOf(searchedText) + 7, cutLong);
                    swapText = swapText.Replace("&#218;", "U"); //Ú
                    swapText = swapText.Replace("&#233;", "e"); //é

                    Regex regData = new Regex(@"[A-Z|a-z]*");
                    Match matData = regData.Match(swapText);
                    string Data = matData.Value;
                    result = Data;
                    return result;
                }
            }
            catch
            {

            }
            return "";
        }

        static public string GetTypeRegexEnglish(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;

                swapText = sourceText.Substring(sourceText.LastIndexOf(searchedText) + 21, cutLong);
                swapText = swapText.Replace("<th>", "");
                swapText = swapText.ToLower();

                Regex regData = new Regex(@"[A-Z|a-z]*");
                Match matData = regData.Match(swapText);
                string Data = matData.Value;
                result = Data;
                return result;
            }
            catch
            {
                return "";

            }
        }

        static public string GetManagementRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                if (sourceText.LastIndexOf(searchedText) != -1)
                {
                    string swapText, result;

                    swapText = sourceText.Substring(sourceText.LastIndexOf(searchedText) + 21, cutLong);
                    swapText = WebUtility.HtmlDecode(swapText);

                    Regex regData = new Regex(@"[A-Z]+[a-z|á|é|ö\s\w]+[A-Z]+[a-z]*");
                    Match matData = regData.Match(swapText);
                    string Data = matData.Value;
                    result = Data;

                    return result;
                }
            }
            catch
            {
            }
            return "";


        }

        static public string GetFocusRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                if (sourceText.LastIndexOf(searchedText) != -1)
                {
                    string swapText, result;

                    swapText = sourceText.Substring(sourceText.LastIndexOf(searchedText), cutLong);

                    Regex regData = new Regex(@"[A-Z]+[a-z|�|é|ö|ú|á]*");
                    Match matData = regData.Match(swapText);
                    string Data = matData.Value;
                    result = Data;

                    return result;
                }
            }
            catch
            {

            }
            return "";
        }

        static public string GetISINRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;
                if (sourceText.IndexOf(searchedText) != -1)
                {
                    swapText = sourceText.Substring(sourceText.IndexOf(searchedText), cutLong);
                    swapText = swapText.Substring(swapText.IndexOf(">/") + 2, 30);

                    Regex regData = new Regex(@"[A-Z]+[0-9]*");
                    Match matData = regData.Match(swapText);
                    string Data = matData.Value;
                    result = Data;

                    return result;
                }
            }
            catch
            {

            }
            return "";
        }

        static public string GetNameRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;
                if (sourceText.IndexOf(searchedText) != -1)
                {
                    swapText = sourceText.Substring(sourceText.IndexOf(searchedText), cutLong);
                    int index = swapText.IndexOf("<span class");

                    if (index != -1)
                    {
                        int firstIndex = swapText.IndexOf(">") + 1;
                        result = swapText.Substring(firstIndex, (index != -1) ? index - firstIndex : 66);
                        //result = swapText.Replace(swapText.Substring(index, swapText.Length - index), "");
                    }
                    else
                    {
                        Regex regData = new Regex(@"[a-z|ö|é|ó|-|A-Z|\s|\w.]*");
                        Match matData = regData.Match(swapText);
                        string Data = matData.Value;
                        result = Data;
                    }
                    return result;
                }
            }
            catch
            {

            }
            return "";
        }

        static public string GetPerformanceSumYearRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;

                swapText = sourceText.Substring(sourceText.LastIndexOf(searchedText), cutLong);
                swapText = swapText.Substring(swapText.Length - 30);
                //swapText = swapText.Substring(swapText.IndexOf(">") + 1, 11);

                Regex regData = new Regex(@"[+|-]+\d+,+\d+");
                Match matData = regData.Match(swapText);
                string Data = matData.Value;
                result = Data;

                return result;
            }
            catch
            {
                return "";

            }

        }
        //uaz mint az előző
        static public string GetPerformanceFromBegginingRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;

                swapText = sourceText.Substring(sourceText.IndexOf(searchedText), cutLong);
                swapText = swapText.Substring(swapText.Length - 15);
                //swapText = swapText.Substring(swapText.IndexOf(">") + 1, 11);

                Regex regData = new Regex(@"[+|-]+\d+,+\d+");
                Match matData = regData.Match(swapText);
                string Data = matData.Value;
                result = Data;

                return result;
            }
            catch
            {
                return "";

            }
        }

        static public string GetYearsRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;

                swapText = sourceText.Substring(sourceText.IndexOf(searchedText), cutLong);
                swapText = swapText.Substring(swapText.Length - 15);
                swapText = swapText.Substring(swapText.IndexOf(">") + 1, 8);

                Regex regData = new Regex(@"[+|-]+\d+,+\d+");
                Match matData = regData.Match(swapText);
                string Data = matData.Value;
                result = Data;

                return result;
            }
            catch
            {
                return "";

            }
        }

        static public List<string> GetVolatility(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText = "";
                List<string> result = new List<string>();

                if (sourceText.IndexOf(searchedText) != -1)
                {
                    swapText = sourceText.Substring(sourceText.LastIndexOf(searchedText), cutLong);
                    if (swapText.IndexOf("</tr>") != -1)
                        swapText = swapText.Substring(0, swapText.IndexOf("</tr>"));

                    swapText = swapText.Replace(searchedText + "</td>", "");
                    swapText = swapText.Replace("<td>", "");
                    swapText = swapText.Replace("</td>", "");
                    swapText = swapText.Replace("<td class=" + '"' + "cell-last" + '"' + ">", "");
                    swapText = Regex.Replace(swapText, @"\t|\r", "");
                    swapText = Regex.Replace(swapText, @"\n", " ");
                    swapText = swapText.Trim();
                    string[] digits = Regex.Split(swapText, @"\s+");

                    return digits.ToList();
                }
                else
                    return null;
            }
            catch
            {
                return null;

            }
        }

        static public List<string> GetOthers(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText = "";
                List<string> result = new List<string>();

                if (sourceText.IndexOf(searchedText) != -1)
                {
                    swapText = sourceText.Substring(sourceText.IndexOf(searchedText), cutLong);
                    if (swapText.IndexOf("</tr>") != -1)
                        swapText = swapText.Substring(0, swapText.IndexOf("</tr>"));

                    swapText = swapText.Replace(searchedText + "</td>", "");
                    swapText = swapText.Replace("<td>", "");
                    swapText = swapText.Replace("</td>", "");
                    swapText = swapText.Replace("<span class=" + '"' + "up" + '"' + ">", "");
                    swapText = swapText.Replace("<span class=" + '"' + "down" + '"' + ">", "");
                    swapText = swapText.Replace("<span class=" + '"' + "cell-last" + '"' + ">", "");
                    swapText = swapText.Replace("<td class=" + '"' + "cell-last" + '"' + ">", "");
                    swapText = swapText.Replace("<span class=" + '"' + "nochange" + '"' + ">", "");
                    swapText = swapText.Replace("</span>", "");
                    swapText = Regex.Replace(swapText, @"\t|\r", "");
                    swapText = Regex.Replace(swapText, @"\n", " ");
                    swapText = swapText.Trim();
                    string[] digits = Regex.Split(swapText, @"\s+");

                    return digits.ToList();
                }
                else
                    return null;
            }
            catch
            {
                return null;

            }
        }

        //Angol
        static public string GetPerformanceSumYearPointRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;

                swapText = sourceText.Substring(sourceText.LastIndexOf(searchedText), cutLong);
                swapText = swapText.Substring(swapText.Length - 30);
                //swapText = swapText.Substring(swapText.IndexOf(">") + 1, 11);

                Regex regData = new Regex(@"[+|-]+\d+.+\d+");
                Match matData = regData.Match(swapText);
                string Data = matData.Value;
                result = Data;
                result = result.Replace(".", ",");

                return result;
            }
            catch
            {
                return "";

            }

        }

        static public string GetPerformanceFromBegginingPointRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;

                swapText = sourceText.Substring(sourceText.IndexOf(searchedText), cutLong);
                swapText = swapText.Substring(swapText.Length - 15);
                //  swapText = swapText.Substring(swapText.IndexOf(">") + 1, 11);

                Regex regData = new Regex(@"[+|-]+\d+.+\d+");
                Match matData = regData.Match(swapText);
                string Data = matData.Value;
                result = Data;
                result = result.Replace(".", ",");

                return result;
            }
            catch
            {
                return "";

            }
        }

        static public string GetYearsPointRegex(string sourceText, string searchedText, int cutLong)
        {
            try
            {
                string swapText, result;

                swapText = sourceText.Substring(sourceText.IndexOf(searchedText), cutLong);
                swapText = swapText.Substring(swapText.Length - 15);
                swapText = swapText.Substring(swapText.IndexOf(">") + 1, 8);

                Regex regData = new Regex(@"[+|-]+\d+.+\d+");
                Match matData = regData.Match(swapText);
                string Data = matData.Value;
                result = Data;
                result = result.Replace(".", ",");

                return result;
            }
            catch
            {
                return "";

            }
        }

        //egyeb
        static public bool GetLinkRegex(string sourceText)
        {
            try
            {

                Regex regData = new Regex("www.teletrader.com/+[a-z|-]*");
                Match matData = regData.Match(sourceText);
                string Data = matData.Value;
                if (!String.IsNullOrEmpty(Data))
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;

            }
        }
        #endregion  

        #region Atlag
        //Átlag kiszámítása
        static public double GetAverage(string Performance2006, string Performance2007, string Performance2008, string Performance2009, string Performance2010, string Performance2011, string Performance2012, string Performance2013, string Performance2014)
        {
            try
            {
                double result = 0;

                double swapPerformance2005;
                double swapPerformance2006;
                double swapPerformance2007;
                double swapPerformance2008;
                double swapPerformance2009;
                double swapPerformance2010;
                double swapPerformance2011;
                double swapPerformance2012;
                double swapPerformance2013;

                //2014
                try
                {
                    if (Performance2014[0] == '-')
                    {
                        Performance2014 = RegexGetAverage(Performance2014);
                        Performance2014 = Performance2014.Replace(',', '.');
                        swapPerformance2005 = -1 * double.Parse(Performance2014, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2014 = RegexGetAverage(Performance2014);
                        Performance2014 = Performance2014.Replace(",", ".");
                        swapPerformance2005 = double.Parse(Performance2014, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2005 = 0;

                }
                //2006
                try
                {
                    if (Performance2006[0] == '-')
                    {
                        Performance2006 = RegexGetAverage(Performance2006);
                        Performance2006 = Performance2006.Replace(',', '.');
                        swapPerformance2006 = -1 * double.Parse(Performance2006, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2006 = RegexGetAverage(Performance2006);
                        Performance2006 = Performance2006.Replace(",", ".");
                        swapPerformance2006 = double.Parse(Performance2006, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2006 = 0;

                }
                //2007
                try
                {
                    if (Performance2007[0] == '-')
                    {
                        Performance2007 = RegexGetAverage(Performance2007);
                        Performance2007 = Performance2007.Replace(',', '.');
                        swapPerformance2007 = -1 * double.Parse(Performance2007, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2007 = RegexGetAverage(Performance2007);
                        Performance2007 = Performance2007.Replace(",", ".");
                        swapPerformance2007 = double.Parse(Performance2007, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2007 = 0;

                }
                //2008
                try
                {
                    if (Performance2008[0] == '-')
                    {
                        Performance2008 = RegexGetAverage(Performance2008);
                        Performance2008 = Performance2008.Replace(',', '.');
                        swapPerformance2008 = -1 * double.Parse(Performance2008, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2008 = RegexGetAverage(Performance2008);
                        Performance2008 = Performance2008.Replace(",", ".");
                        swapPerformance2008 = double.Parse(Performance2008, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2008 = 0;

                }
                //2009
                try
                {
                    if (Performance2009[0] == '-')
                    {
                        Performance2009 = RegexGetAverage(Performance2009);
                        Performance2009 = Performance2009.Replace(',', '.');
                        swapPerformance2009 = -1 * double.Parse(Performance2009, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2009 = RegexGetAverage(Performance2009);
                        Performance2009 = Performance2009.Replace(",", ".");
                        swapPerformance2009 = double.Parse(Performance2009, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2009 = 0;

                }
                //2010
                try
                {
                    if (Performance2010[0] == '-')
                    {
                        Performance2010 = RegexGetAverage(Performance2010);
                        Performance2010 = Performance2010.Replace(',', '.');
                        swapPerformance2010 = -1 * double.Parse(Performance2010, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2010 = RegexGetAverage(Performance2010);
                        Performance2010 = Performance2010.Replace(",", ".");
                        swapPerformance2010 = double.Parse(Performance2010, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2010 = 0;

                }
                //2011
                try
                {
                    if (Performance2011[0] == '-')
                    {
                        Performance2011 = RegexGetAverage(Performance2011);
                        Performance2011 = Performance2011.Replace(',', '.');
                        swapPerformance2011 = -1 * double.Parse(Performance2011, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2011 = RegexGetAverage(Performance2011);
                        Performance2011 = Performance2011.Replace(",", ".");
                        swapPerformance2011 = double.Parse(Performance2011, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2011 = 0;

                }
                //2012
                try
                {
                    if (Performance2012[0] == '-')
                    {
                        Performance2012 = RegexGetAverage(Performance2012);
                        Performance2012 = Performance2012.Replace(',', '.');
                        swapPerformance2012 = -1 * double.Parse(Performance2012, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2012 = RegexGetAverage(Performance2012);
                        Performance2012 = Performance2012.Replace(",", ".");
                        swapPerformance2012 = double.Parse(Performance2012, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2012 = 0;

                }
                //2013
                try
                {
                    if (Performance2013[0] == '-')
                    {
                        Performance2013 = RegexGetAverage(Performance2013);
                        Performance2013 = Performance2013.Replace(',', '.');
                        swapPerformance2013 = -1 * double.Parse(Performance2013, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2013 = RegexGetAverage(Performance2013);
                        Performance2013 = Performance2013.Replace(",", ".");
                        swapPerformance2013 = double.Parse(Performance2013, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2013 = 0;

                }
                //Átlag kiszámítása

                int piece = 0;

                if (swapPerformance2005 != 0)
                {
                    result = result + swapPerformance2005;
                    piece += 1;
                }
                if (swapPerformance2006 != 0)
                {
                    result = result + swapPerformance2006;
                    piece += 1;
                }
                if (swapPerformance2007 != 0)
                {
                    result = result + swapPerformance2007;
                    piece += 1;
                }
                if (swapPerformance2008 != 0)
                {
                    result = result + swapPerformance2008;
                    piece += 1;
                }
                if (swapPerformance2009 != 0)
                {
                    result = result + swapPerformance2009;
                    piece += 1;
                }
                if (swapPerformance2010 != 0)
                {
                    result = result + swapPerformance2010;
                    piece += 1;
                }
                if (swapPerformance2011 != 0)
                {
                    result = result + swapPerformance2011;
                    piece += 1;
                }
                if (swapPerformance2012 != 0)
                {
                    result = result + swapPerformance2012;
                    piece += 1;
                }
                if (swapPerformance2013 != 0)
                {
                    result = result + swapPerformance2013;
                    piece += 1;
                }

                if (piece != 0)
                {
                    result = result / piece;
                    string swapResult = String.Format("{0:0.##}", result);
                    swapResult = swapResult.Replace(',', '.');
                    result = double.Parse(swapResult, System.Globalization.CultureInfo.InvariantCulture);
                }
                else result = 0;

                return result;
            }
            catch
            {

                return 0;
            }

        }

        static public double GetAverageDot(string Performance2006, string Performance2007, string Performance2008, string Performance2009, string Performance2010, string Performance2011, string Performance2012, string Performance2013, string Performance2014)
        {
            try
            {
                double result = 0;

                double swapPerformance2006;
                double swapPerformance2007;
                double swapPerformance2008;
                double swapPerformance2009;
                double swapPerformance2010;
                double swapPerformance2011;
                double swapPerformance2012;
                double swapPerformance2013;
                double swapPerformance2014;

                //20114
                try
                {
                    if (Performance2014[0] == '-')
                    {
                        Performance2014 = RegexGetAverage(Performance2014);
                        swapPerformance2014 = -1 * double.Parse(Performance2014, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2014 = RegexGetAverage(Performance2014);
                        swapPerformance2014 = double.Parse(Performance2014, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2014 = 0;

                }
                //2006
                try
                {
                    if (Performance2006[0] == '-')
                    {
                        Performance2006 = RegexGetAverage(Performance2006);
                        swapPerformance2006 = -1 * double.Parse(Performance2006, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2006 = RegexGetAverage(Performance2006);
                        swapPerformance2006 = double.Parse(Performance2006, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2006 = 0;

                }
                //2007
                try
                {
                    if (Performance2007[0] == '-')
                    {
                        Performance2007 = RegexGetAverage(Performance2007);
                        swapPerformance2007 = -1 * double.Parse(Performance2007, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2007 = RegexGetAverage(Performance2007);
                        swapPerformance2007 = double.Parse(Performance2007, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2007 = 0;

                }
                //2008
                try
                {
                    if (Performance2008[0] == '-')
                    {
                        Performance2008 = RegexGetAverage(Performance2008);
                        swapPerformance2008 = -1 * double.Parse(Performance2008, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2008 = RegexGetAverage(Performance2008);
                        swapPerformance2008 = double.Parse(Performance2008, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2008 = 0;

                }
                //2009
                try
                {
                    if (Performance2009[0] == '-')
                    {
                        Performance2009 = RegexGetAverage(Performance2009);
                        swapPerformance2009 = -1 * double.Parse(Performance2009, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2009 = RegexGetAverage(Performance2009);
                        swapPerformance2009 = double.Parse(Performance2009, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2009 = 0;

                }
                //2010
                try
                {
                    if (Performance2010[0] == '-')
                    {
                        Performance2010 = RegexGetAverage(Performance2010);
                        swapPerformance2010 = -1 * double.Parse(Performance2010, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2010 = RegexGetAverage(Performance2010); ;
                        swapPerformance2010 = double.Parse(Performance2010, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2010 = 0;

                }
                //2011
                try
                {
                    if (Performance2011[0] == '-')
                    {
                        Performance2011 = RegexGetAverage(Performance2011);
                        swapPerformance2011 = -1 * double.Parse(Performance2011, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2011 = RegexGetAverage(Performance2011);
                        swapPerformance2011 = double.Parse(Performance2011, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2011 = 0;

                }
                //2012
                try
                {
                    if (Performance2012[0] == '-')
                    {
                        Performance2012 = RegexGetAverage(Performance2012);
                        swapPerformance2012 = -1 * double.Parse(Performance2012, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2012 = RegexGetAverage(Performance2012);
                        swapPerformance2012 = double.Parse(Performance2012, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2012 = 0;

                }
                //2013
                try
                {
                    if (Performance2013[0] == '-')
                    {
                        Performance2013 = RegexGetAverage(Performance2013);
                        swapPerformance2013 = -1 * double.Parse(Performance2013, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Performance2013 = RegexGetAverage(Performance2013);
                        swapPerformance2013 = double.Parse(Performance2013, System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    swapPerformance2013 = 0;

                }
                //Átlag kiszámítása

                int piece = 0;

                if (swapPerformance2014 != 0)
                {
                    result = result + swapPerformance2014;
                    piece += 1;
                }
                if (swapPerformance2006 != 0)
                {
                    result = result + swapPerformance2006;
                    piece += 1;
                }
                if (swapPerformance2007 != 0)
                {
                    result = result + swapPerformance2007;
                    piece += 1;
                }
                if (swapPerformance2008 != 0)
                {
                    result = result + swapPerformance2008;
                    piece += 1;
                }
                if (swapPerformance2009 != 0)
                {
                    result = result + swapPerformance2009;
                    piece += 1;
                }
                if (swapPerformance2010 != 0)
                {
                    result = result + swapPerformance2010;
                    piece += 1;
                }
                if (swapPerformance2011 != 0)
                {
                    result = result + swapPerformance2011;
                    piece += 1;
                }
                if (swapPerformance2012 != 0)
                {
                    result = result + swapPerformance2012;
                    piece += 1;
                }
                if (swapPerformance2013 != 0)
                {
                    result = result + swapPerformance2013;
                    piece += 1;
                }

                if (piece != 0)
                {
                    result = result / piece;
                    string swapResult = String.Format("{0:0.##}", result);
                    swapResult = swapResult.Replace(',', '.');
                    result = double.Parse(swapResult, System.Globalization.CultureInfo.InvariantCulture);
                }
                else result = 0;

                return result;
            }
            catch
            {

                return 0;
            }

        }

        static public string RegexGetAverage(string sourceString)
        {
            try
            {
                Regex regData = new Regex("");
                if (sourceString.Contains(','))
                {
                    regData = new Regex(@"\d+,+\d+");
                }
                else regData = new Regex(@"\d+.+\d+");

                Match matData = regData.Match(sourceString);
                string Data = matData.Value;
                return Data;
            }
            catch { return null; }

        }
        #endregion

    }
}
