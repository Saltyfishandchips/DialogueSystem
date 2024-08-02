//using Mono.Cecil.Cil;
using UnityEngine;
using XlsWork.DailyXls;
using XlsWork.NPCsXls;
using XlsWork;
using System.Collections.Generic;
using TMPro;
using XlsWork.ObituariesXls;
using XlsWork.TravelPermitsXls;
using UnityEngine.UI;
using Unity.VisualScripting;

public class FlowDataManager : MonoBehaviour
{
    // 静态实例变量
    private static FlowDataManager _instance;

    // 公共属性来访问实例
    public static FlowDataManager Instance
    {
        get
        {
            // 如果实例为空，尝试找到一个已经存在的实例
            if (_instance == null)
            {
                _instance = FindObjectOfType<FlowDataManager>();

                // 如果仍然为空，创建一个新的 GameObject 并添加 FlowDataManager 组件
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<FlowDataManager>();
                    singletonObject.name = typeof(FlowDataManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    // 确保实例在场景切换时不被销毁
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);

            // 初始化属性
            InitializeProperties();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }


    // 全局属性
    public int trueNum ;
    public int fanyangNum;
    public int guiyinNum;
    public int brieryNum;
    public Sprite  riliSprite;
    public GameObject riliGo;
    
    // 今日死人
    public bool isWrittingBrushOn;
    public Dictionary<string, IndividualData> todayVisitors;

    public NPCInfomations currentNPC;
    public int todayVisitorsNum;
    public int currentNpcId;

    // 地府小报相关
    public Dictionary<int,DailyData> todayDaily;
    public DailyInfo currentDaily;
    public int day;
    public TMP_Text[] dailyTexts;
    

    // 生死簿
    public Dictionary<string,ObituaryData> todayObituary;
    public ObituaryInfo currentObituary;
    public ObituaryTotal obituaryTotal;
    public Dictionary<string, List<string>> firstPageDictionary;
    public Dictionary<string, List<string>> CataloguePageDictionary;
    public GameObject prefab;  // 要实例化的预制体
    public GameObject firstPageTitlePrefab;  // 要实例化的预制体
    public GameObject cataloguePrefab; 
    public RectTransform parentLeftTransform;  // 父级 Transform
    public RectTransform parentRightTransform;  // 父级 Transform
    public List<GameObject> firstPageGOs;
    public List<GameObject> cataloguePageGos;
    public TMP_Text[] ghostsPageTexts;
    public Button downBtn;
    public Button leftBtn;


    // 路引
    public Dictionary<string,TravelPermitData> todayTravelPermit;
    public TravelPermitInfo currentTravelPermit;
    public TMP_Text[] travelPermitTexts;

    // 初始化属性的方法
    private void InitializeProperties()
    {
        day = DayManageInScene.dayCount;
        // 死人
        isWrittingBrushOn = false; // 例如：将写刷状态设为打开

        string str = "/Excel/npcInfoDay" + day.ToString() + ".xlsx";
        todayVisitors = NPCXls.LoadExcelAsDictionary("/Excel/npcInfoDay" + day.ToString() + ".xlsx");
        todayVisitorsNum = todayVisitors.Count;
        currentNpcId = 0;
        currentNPC = new NPCInfomations();
        // currentNPC.Settings = new NPCSettings();
        // currentNPC.InitFromID = -1;

        // 地府小报
        todayDaily = DailyPaperXls.LoadExcelAsDailyDictionary("/Excel/daily.xlsx");
        currentDaily = new DailyInfo();


        // 生死簿
        todayObituary = ObituaryXls.LoadExcelAsObituaryDictionary("/Excel/Obituary.xlsx");
        currentObituary = new ObituaryInfo();
        obituaryTotal = new ObituaryTotal();
        obituaryTotal.obituaryDictionary = new Dictionary<int, ObituarySettings>();
        firstPageDictionary = ObituaryXls.LoadExcelAsFirstPageObituaryDictionary("/Excel/FirstPage.xlsx");
        CataloguePageDictionary = ObituaryXls.LoadExcelAsCataloguePageObituaryDictionary("/Excel/CataloguePage.xlsx");
        firstPageGOs = new List<GameObject>();
        cataloguePageGos = new List<GameObject>();

        // 路引
        todayTravelPermit = TravelPermitXls.LoadExcelAsTravelPermitDictionary("/Excel/TravelPermit.xlsx");
        currentTravelPermit = new TravelPermitInfo();


        //全局
        trueNum = 0;
        fanyangNum = 0;
        guiyinNum = 0;
        brieryNum = 0;
        if(day==2)
        {
            riliGo.GetComponent<Image>().sprite = riliSprite;
        }
        
        Debug.Log("FlowDataManager properties initialized.");
    }

    public void updateCurrentNPC()
    {
        currentNpcId++;
        currentNPC.InitFromID = currentNpcId;
        string searchId = "D" + day.ToString() + "_" +currentNpcId.ToString();
        currentNPC.Settings = todayVisitors[searchId];
        currentNPC.Settings.PrintLog();
    }

    public void updateObituary()
    {
        firstPageGOs.Add(Instantiate(firstPageTitlePrefab, parentLeftTransform));
        // 处理首页
        List<string> temp = firstPageDictionary[day.ToString()];
        for(int i=0; i<temp.Count;i++)
        {
            GameObject instance = Instantiate(prefab, parentLeftTransform); 
            instance.GetComponent<TMP_Text>().text = temp[i];
            firstPageGOs.Add(instance);
        }

        // 目录页
        List<string> catalogueTemp = CataloguePageDictionary[day.ToString()];
        for(int i=0; i<catalogueTemp.Count;i++)
        {
            if(i<=4)
            {
                GameObject instance = Instantiate(cataloguePrefab, parentLeftTransform); 
                instance.GetComponent<TMP_Text>().text = catalogueTemp[i];
                instance.SetActive(true);
                cataloguePageGos.Add(instance);
            }
            else
            {
                GameObject instance = Instantiate(cataloguePrefab, parentRightTransform); 
                instance.GetComponent<TMP_Text>().text = catalogueTemp[i];
                instance.SetActive(true);
                cataloguePageGos.Add(instance);
            }
        }
        
        // 人物页
        obituaryTotal.totalPageNum = 2;
        foreach(var key in todayVisitors.Keys)
        {
            if(todayObituary.ContainsKey(key) )
            {
                currentObituary.Settings = todayObituary[key];
                obituaryTotal.totalPageNum++;
                obituaryTotal.obituaryDictionary.Add(obituaryTotal.totalPageNum,currentObituary.Settings);
            }
        }

        obituaryTotal.currentPage = 1;

    }

    public void updateCurrentDaily()
    {   
        if(day<=1)
        {
            //do nothing
        }
        else
        {
            currentDaily.Settings = todayDaily[day];
            currentDaily.Settings.PrintLog();
            //day++;

            dailyTexts[0].text = currentDaily.Settings.newsATitle;
            dailyTexts[1].text = currentDaily.Settings.newsA;
            dailyTexts[2].text = currentDaily.Settings.newsBTitle;
            dailyTexts[3].text = currentDaily.Settings.newsB;
            dailyTexts[4].text = currentDaily.Settings.newsCTitle;
            dailyTexts[5].text = currentDaily.Settings.newsC;
        }
    }

    public void updateCurrentTravelPermit()
    {
        if(todayTravelPermit.ContainsKey(currentNPC.Settings.npcId))
        {
            currentTravelPermit.Settings = todayTravelPermit[currentNPC.Settings.npcId];

            //路引
            travelPermitTexts[0].text = "姓名:" + currentTravelPermit.Settings.lyName;
            if(currentTravelPermit.Settings.npcGender == false)
            {
                travelPermitTexts[1].text = "性别:" + "男";
            }
            else
            {
                travelPermitTexts[1].text = "性别:" + "女";
            }
            travelPermitTexts[2].text = "死期:" + currentTravelPermit.Settings.npcDeadDay;
            travelPermitTexts[3].text = "死因:" + currentTravelPermit.Settings.npcCause;
            travelPermitTexts[4].text = "鬼差:" + currentTravelPermit.Settings.lyStaffNo;
            travelPermitTexts[5].text = "生日:"  + currentTravelPermit.Settings.npcBrithDay;
        }
    }

    public void pageTurning()
    {
        switch(obituaryTotal.currentPage)
        {
            case 1:
                foreach(var go in firstPageGOs)
                {
                    go.SetActive(true);
                }
                foreach(var go in cataloguePageGos)
                {
                    go.SetActive(false);
                }
                foreach(var go in ghostsPageTexts)
                {
                    go.gameObject.SetActive(false);
                }
                leftBtn.gameObject.SetActive(false);
                break;

            case 2:
                foreach(var go in firstPageGOs)
                {
                    go.SetActive(false);
                }
                foreach(var go in cataloguePageGos)
                {
                    go.SetActive(true);
                }
                foreach(var go in ghostsPageTexts)
                {
                    go.gameObject.SetActive(false);
                }
                leftBtn.gameObject.SetActive(true);

                break;

            default:
                foreach(var go in firstPageGOs)
                {
                    go.SetActive(false);
                }
                foreach(var go in cataloguePageGos)
                {
                    go.SetActive(false);
                }
                foreach(var go in ghostsPageTexts)
                {
                    go.gameObject.SetActive(true);
                }
                // reading
                ghostsPageTexts[0].text = "姓名:" + obituaryTotal.obituaryDictionary[obituaryTotal.currentPage].ssbName;
                ghostsPageTexts[1].text = "诞辰:" + obituaryTotal.obituaryDictionary[obituaryTotal.currentPage].npcTOB;
                ghostsPageTexts[2].text = "卒日:" + obituaryTotal.obituaryDictionary[obituaryTotal.currentPage].npcTOD;
                ghostsPageTexts[3].text = "负责鬼差:" + obituaryTotal.obituaryDictionary[obituaryTotal.currentPage].ssbStaffNo;
                ghostsPageTexts[4].text = "生平:" + obituaryTotal.obituaryDictionary[obituaryTotal.currentPage].npcDescription;
                break;
        }

        if(obituaryTotal.currentPage == obituaryTotal.totalPageNum)
        {
            downBtn.gameObject.SetActive(false);
        }
        else if(obituaryTotal.currentPage == obituaryTotal.totalPageNum - 1)
        {
            downBtn.gameObject.SetActive(true);
        }
    }


    public class ObituaryTotal
    {
        public int totalPageNum;
        public int currentPage;

        public TMP_Text[] rulesPageTexts;
        public TMP_Text[] catalogueTexts;
        public TMP_Text[] ghostsTexts;

        public Dictionary<int,ObituarySettings> obituaryDictionary;
    }
}