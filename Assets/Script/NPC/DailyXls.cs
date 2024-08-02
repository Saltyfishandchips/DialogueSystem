using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using ExcelDataReader;
using System.Text;
using System.Data;

namespace XlsWork
{
    namespace DailyXls
    {
        public class DailyPaperXls : MonoBehaviour
        {
            /// <summary>
            /// 配表中属性字段的数量
            /// </summary>
            public static int CountOfAttributes = 7;

            public static Dictionary<int, DailyData> LoadExcelAsDailyDictionary(string str)
            {
                Dictionary<int, DailyData> itemDictionary = new Dictionary<int, DailyData>(); // 新建字典，用于存储以行为单位的各个操作单元

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
                                        int id = Convert.ToInt32(table.Rows[row][0]);
                                        string newsATitle = table.Rows[row][1].ToString();
                                        string newsA = table.Rows[row][2].ToString();
                                        string newsBTitle = table.Rows[row][3].ToString();
                                        string newsB = table.Rows[row][4].ToString();
                                        string newsCTitle = table.Rows[row][5].ToString();
                                        string newsC = table.Rows[row][6].ToString();

                                        //fill end

                                        DailyData item = new DailyData(id, newsATitle,newsA,newsBTitle,newsB,newsCTitle,newsC);

                                        if (!itemDictionary.ContainsKey(id))
                                        {
                                            itemDictionary.Add(id, item); // 将ID和操作单元写入字典
                                        }
                                        else
                                        {
                                            Debug.LogWarning("Duplicate item ID found: " + id);
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

