using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using ExcelDataReader;
using System.Text;
using System.Data;

namespace XlsWork
{
    namespace NPCsXls
    {
        public class NPCXls : MonoBehaviour
        {
            /// <summary>
            /// 配表中属性字段的数量
            /// </summary>
            public static int CountOfAttributes = 8;

            public static Dictionary<string, IndividualData> LoadExcelAsDictionary(string str)
            {
                Dictionary<string, IndividualData> itemDictionary = new Dictionary<string, IndividualData>(); // 新建字典，用于存储以行为单位的各个操作单元

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
                                        string id = table.Rows[row][0].ToString();
                                        string name = table.Rows[row][1].ToString();
                                        string iconName = table.Rows[row][2].ToString();
                                        string preDialogues = table.Rows[row][3].ToString();
                                        NpcSpecialEvent specialEvent = EnumConverter.StringToSpecialEvent(table.Rows[row][4].ToString());
                                        NpcResultHandle correctResultHandle = EnumConverter.StringToNpcResultHandle(table.Rows[row][5].ToString());
                                        string afterDialogues = table.Rows[row][6].ToString();
                                        string qgwDialogues = table.Rows[row][7].ToString();

                                        //fill end

                                        IndividualData item = new IndividualData(id, name,iconName,preDialogues,specialEvent,correctResultHandle,afterDialogues,qgwDialogues);

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
