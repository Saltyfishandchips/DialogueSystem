using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using ExcelDataReader;
using System.Text;
using System.Data;
using UnityEngine.TestTools;

namespace XlsWork
{
    namespace TravelPermitsXls
    {
        public class TravelPermitXls : MonoBehaviour
        {
            /// <summary>
            /// 配表中属性字段的数量
            /// </summary>
            public static int CountOfAttributes = 7;

            public static Dictionary<string, TravelPermitData> LoadExcelAsTravelPermitDictionary(string str)
            {
                Dictionary<string, TravelPermitData> itemDictionary = new Dictionary<string, TravelPermitData>(); // 新建字典，用于存储以行为单位的各个操作单元

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
                                        string lyName = table.Rows[row][1].ToString();
                                        bool npcGender = Convert.ToBoolean(table.Rows[row][2]);
                                        string npcBrithDay = table.Rows[row][3].ToString();
                                        string npcDeadDay = table.Rows[row][4].ToString();
                                        string npcCause = table.Rows[row][5].ToString();
                                        string lyStaffNo = table.Rows[row][6].ToString();

                                        //fill end

                                        TravelPermitData item = new TravelPermitData(npcId, lyName, npcGender, npcBrithDay, npcDeadDay, npcCause, lyStaffNo);

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