using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using ExcelDataReader;
using System.Text;
using System.Data;

namespace XlsWork
{
    namespace ObituariesXls
    {
        public class ObituaryXls : MonoBehaviour
        {
            /// <summary>
            /// 配表中属性字段的数量
            /// </summary>
            public static int CountOfAttributes = 6;

            public static Dictionary<string, List<string>>LoadExcelAsCataloguePageObituaryDictionary(string str)
            {
                Dictionary<string, List<String>> pageTexts = new Dictionary<string, List<String>>();
                string path = Application.dataPath + str;

                 if (!File.Exists(path))
                {
                    Debug.LogError("Excel file not found at path: " + path);
                    return pageTexts;
                }

                try
                {
                    // 确保注册编码
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    // 打开文件流
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(fs))
                        {
                            // 使用 AsDataSet 获取数据
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true // 设置第一行作为表头
                                }
                            });

                            // 处理数据
                            foreach (DataTable table in result.Tables)
                            {
                                for (int row = 0; row < table.Rows.Count; row++)
                                {
                                    try
                                    {   // npc info fill
                                        string dayid = table.Rows[row][0].ToString();
                                        string description = table.Rows[row][1].ToString();
                                        List<string> texts = new List<string>();
                                        texts.Add(description);
                                        //fill end

                                        if (!pageTexts.ContainsKey(dayid))
                                        {
                                            pageTexts.Add(dayid, texts); // 将ID和操作单元写入字典
                                        }
                                        else
                                        {
                                            pageTexts[dayid].Add(description);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.LogError("Error processing row " + row + ": " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    Debug.Log("Excel data load complete.");
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error reading Excel file: " + ex.Message);
                }

                return pageTexts;
            }

            public static Dictionary<string, List<string>>LoadExcelAsFirstPageObituaryDictionary(string str)
            {
                Dictionary<string, List<String>> pageTexts = new Dictionary<string, List<String>>();
                string path = Application.dataPath + str;

                 if (!File.Exists(path))
                {
                    Debug.LogError("Excel file not found at path: " + path);
                    return pageTexts;
                }

                try
                {
                    // 确保注册编码
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    // 打开文件流
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(fs))
                        {
                            // 使用 AsDataSet 获取数据
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true // 设置第一行作为表头
                                }
                            });

                            // 处理数据
                            foreach (DataTable table in result.Tables)
                            {
                                for (int row = 0; row < table.Rows.Count; row++)
                                {
                                    try
                                    {   // npc info fill
                                        string dayid = table.Rows[row][0].ToString();
                                        string description = table.Rows[row][1].ToString();
                                        List<string> texts = new List<string>();
                                        texts.Add(description);
                                        //fill end

                                        if (!pageTexts.ContainsKey(dayid))
                                        {
                                            pageTexts.Add(dayid, texts); // 将ID和操作单元写入字典
                                        }
                                        else
                                        {
                                            pageTexts[dayid].Add(description);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.LogError("Error processing row " + row + ": " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    Debug.Log("Excel data load complete.");
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error reading Excel file: " + ex.Message);
                }

                return pageTexts;
            }

            public static Dictionary<string, ObituaryData> LoadExcelAsObituaryDictionary(string str)
            {
                Dictionary<string, ObituaryData> itemDictionary = new Dictionary<string, ObituaryData>(); // 新建字典，用于存储以行为单位的各个操作单元

                string path = Application.dataPath + str;
                //string path = Application.dataPath + "/Excel/demotest1.xlsx"; // 指定表格的文件路径。在编辑器模式下，Application.dataPath就是Assets文件夹

                if (!File.Exists(path))
                {
                    Debug.LogError("Excel file not found at path: " + path);
                    return itemDictionary;
                }

                try
                {
                    // 确保注册编码
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    // 打开文件流
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(fs))
                        {
                            // 使用 AsDataSet 获取数据
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true // 设置第一行作为表头
                                }
                            });

                            // 处理数据
                            foreach (DataTable table in result.Tables)
                            {
                                for (int row = 0; row < table.Rows.Count; row++)
                                {
                                    try
                                    {   // npc info fill
                                        string npcId = table.Rows[row][0].ToString();
                                        string npcName = table.Rows[row][1].ToString();
                                        string npcTOB = table.Rows[row][2].ToString();
                                        string npcTOD = table.Rows[row][3].ToString();
                                        string npcDescription = table.Rows[row][4].ToString();
                                        string ssbStaffNo = table.Rows[row][5].ToString();

                                        //fill end

                                        ObituaryData item = new ObituaryData(npcId, npcName,npcTOB,npcTOD,npcDescription,ssbStaffNo);

                                        if (!itemDictionary.ContainsKey(npcId))
                                        {
                                            itemDictionary.Add(npcId, item); // 将ID和操作单元写入字典
                                        }
                                        else
                                        {
                                            Debug.LogWarning("Duplicate item ID found: " + npcId);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.LogError("Error processing row " + row + ": " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    Debug.Log("Excel data load complete.");
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error reading Excel file: " + ex.Message);
                }

                return itemDictionary;
            }
        }

    }
}