using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using LitJson;
using System.IO;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    public TextAsset fishInfoListText;  //txt格式的物品信息
    private string itemJsonPath = "/Resources/Datas/FishInfo3.json";//json格式物品信息的路径

    private Dictionary<int, Item> itemInfoDic = new Dictionary<int, Item>(); //用于储存物品基本信息的字典

    private static Item[] itemInInventory; //储存实际物品的数组

    private int Gold; //金钱数量

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        ReadInfoFromJson(itemJsonPath);

        InitInventoryAsNull(20);

        Gold = 10000;

        //EventCenter.Broadcast(EventCode.OnManagerInitFinish);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetFish();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            AddGold(100);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            RemoveGold(1000);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LogAllItemsInDic();
        }
    }

    /// <summary>
    /// 从json文件中读取数据并转为dictionay
    /// 输入为json文件的路径
    /// </summary>
    /// <param name="path"></param>
    private void ReadInfoFromJson(string path)
    {
        //从json文件中读取数据并转为dictionay
        //输入为json文件的路径

        JsonData itemInfoData;
        //List<Item> itemInfoList;
        itemInfoData = JsonMapper.ToObject(fishInfoListText.text);
        //itemInfoData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + path));
        /*itemInfoList = JsonMapper.ToObject<List<Item>>(itemInfoData.ToJson());

        foreach (Item item in itemInfoList)
        {
            itemInfoDic.Add(item.ID, item);
        }*/

        for (int i = 0; i < itemInfoData.Count; i++)
        {
            int id = (int)itemInfoData[i]["ID"];
            string name = (string)itemInfoData[i]["Name"];
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), itemInfoData[i]["Type"].ToString());
            string description = (string)itemInfoData[i]["Description"];
            int basePrice = (int)itemInfoData[i]["BasePrice"];
            string iconPath = (string)itemInfoData[i]["IconPath"];

            Item item = new Item(id, name, type, description, basePrice, iconPath);
            itemInfoDic.Add(item.ID, item);
        }
    }

    /// <summary>
    /// 从csv格式的txt文件中读取数据并转为dictionay
    /// 目前不用
    /// </summary>
    /*
    private void ReadInfoFromTxt()
    {
        //从csv格式的txt文件中读取数据并转为dictionay
        //目前不用
        Item info;

        string text = fishInfoListText.text;
        string[] strArrayRaw = text.Split('\n'); ;

        foreach (string str in strArrayRaw)
        {
            string[] proArray = str.Split(',');
            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string description = proArray[2];
            int basePrice = int.Parse(proArray[3]);
            string iconPath = proArray[4];

            info = new Item(id, name, description, basePrice, iconPath);
            itemInfoDic.Add(id, info);
        }
    }
    */

    /// <summary>
    /// 模拟得到鱼的功能
    /// </summary>
    public void GetFish()
    {
        //模拟得到鱼的功能
        int ID = Random.Range(1001, 1005);
        //Debug.Log(ID);
        SaveItemUseID(ID);
    }

    /// <summary>
    /// 根据itemId创造Item的实例
    /// 将Item存入ItemInBag
    /// </summary>
    /// <param name="itemId"></param>
    public void SaveItemUseID(int itemId)
    {
        Item item = new Item(itemInfoDic[itemId]);
        SaveItem(item);
    }

    /// <summary>
    /// 初始化背包为空背包
    /// </summary>
    /// <param name="key"></param>
    public static void InitInventoryAsNull(int length)
    {
        itemInInventory = new Item[length];
        for (int i = 0; i < itemInInventory.Length; i++)
        {
            itemInInventory[i] = null;
        }
    }

    /// <summary>
    /// 将Item存入背包
    /// </summary>
    /// <param name="value"></param>
    public static void SaveItem(Item value)
    {
        for (int i = 0; i < itemInInventory.Length; i++)
        {
            if (itemInInventory[i] == null)
            {
                itemInInventory[i] = value;
                EventCenter.Broadcast<Item>(EventCode.OnGetItem, value);
                EventCenter.Broadcast(EventCode.OnBagUIUpdate);
                return;
            }
        }
        Debug.Log("full");
    }

    /// <summary>
    /// 取出数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Item GetItem(int index)
    {
        if (index < itemInInventory.Length)
        {
            return itemInInventory[index];
        }
        else
        {
            return null;
        }
    }

    ///<summary>
    ///删除数据
    /// </summary>
    public static void DeleteItem(int index)
    {
        itemInInventory[index] = null;
    }

    /// <summary>
    /// 获得物品存储容量
    /// </summary>
    /// <returns></returns>
    public int GetInventoryCap()
    {
        return itemInInventory.Length;
    }

    /// <summary>
    /// 输出数组内数据
    /// 用于debug
    /// </summary>
    public static void LogAllItems()
    {
        Debug.Log("666");
        for (int i = 0; i < itemInInventory.Length; i++)
        {
            if (itemInInventory[i] == null)
            {
                Debug.Log("null");
            }
            else
            {
                Debug.Log(itemInInventory[i].Name);
            }
        }
    }

    /// <summary>
    /// 输出基础字典内的数据
    /// 用于debug
    /// </summary>
    public static void LogAllItemsInDic()
    {
        //Debug.Log("233");
        foreach (Item a in instance.itemInfoDic.Values)
        {
            Debug.Log(a);
        }
    }

    /// <summary>
    /// 输出基础字典中的特定数据
    /// </summary>
    public List<Item> OutputItemsInDic(Item.ItemType type)
    {
        List<Item> items = new List<Item>();

        foreach (Item a in itemInfoDic.Values)
        {
            if (a.Type.Equals(type))
            {
                items.Add(a);
            }
        }

        return items;
    }

    /// <summary>
    /// 交换两个Item的位置
    /// </summary>
    /// <param name="prevIndex"></param>
    /// <param name="targetIndex"></param>
    public static void SwitchItem(int prevIndex, int targetIndex)
    {
        Item temp = itemInInventory[prevIndex];

        itemInInventory[prevIndex] = itemInInventory[targetIndex];
        itemInInventory[targetIndex] = temp;
    }

    /// <summary>
    /// 获得当前金钱
    /// </summary>
    /// <returns></returns>
    public int GetGold()
    {
        return Gold;
    }

    /// <summary>
    /// 增加金钱
    /// </summary>
    /// <param name="add"></param>
    public void AddGold(int add)
    {
        Gold += add;
        EventCenter.Broadcast(EventCode.OnGoldChange);
    }

    /// <summary>
    /// 减少金钱
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    public bool RemoveGold(int current)
    {
        if (Gold - current < 0)
        {
            return false;
        }
        else
        {
            Gold -= current;
            EventCenter.Broadcast(EventCode.OnGoldChange);
            return true;
        }
    }

    /// <summary>
    /// 出售指定位置的物品
    /// </summary>
    /// <param name="index"></param>
    public void SellItem(int index)
    {
        if (index < itemInInventory.Length && itemInInventory[index] != null)
        {
            AddGold(itemInInventory[index].BasePrice);
            itemInInventory[index] = null;
        }
    }

    /// <summary>
    /// 购买指定物品
    /// </summary>
    /// <param name="item"></param>
    public void BuyItem(Item item, int index)
    {
        if (RemoveGold(item.BasePrice))
        {
            itemInInventory[index] = item;
        }
        else
        {
            Flowchart.BroadcastFungusMessage("NoMoney");
        }
    }
}

/// <summary>
/// 道具的基本信息
/// 创建道具随机属性时参考的基本信息
/// </summary>
public class ItemBase
{
    #region 属性
    /// <summary>
    ///ID
    ///</summary>
    public int ID { get; set; }

    /// <summary>
    /// 物品名称
    /// </summary>
    public string Name { get; set; }

    //物品类型
    //public ItemType Type { get; set; }

    //品质
    //暂无

    /// <summary>
    /// 物品描述
    /// </summary>
    public string Description { get; set; }

    //基本价格
    public int BasePrice { get; set; }

    //出售价格
    //public int SellPrice { get; set; }

    //购买价格
    //public int BuyPrice { get; set; }

    /// <summary>
    /// 图标路径
    /// </summary>
    public string IconPath { get; set; }

    //图标
    //public Sprite Icon { get; set; }

    #endregion 属性

    #region 枚举
    //物品类型
    /*public enum ItemType
    {
        Fish,       //鱼类
        Material,   //材料
        Weapon,     //武器
        Consumable, //消耗品
    }*/

    #endregion 枚举

    #region 构造方法
    /// <summary>
    /// 有参数的构造
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="iconPath"></param>
    public ItemBase(int id, string name, /*ItemType type,*/ string description, /*int basePrice,*/ string iconPath)
    {
        this.ID = id;
        this.Name = name;
        //this.Type = type;
        this.Description = description;
        //this.BasePrice = basePrice;
        this.IconPath = iconPath;

        //this.Icon = Resources.Load<Sprite>(this.IconPath);
    }

    /// <summary>
    /// 复制构造
    /// </summary>
    /// <param name="itemInfo"></param>
    public ItemBase(ItemBase itemInfo)
    {
        this.ID = itemInfo.ID;
        this.Name = itemInfo.Name;
        //this.Type = item.Type;
        this.Description = itemInfo.Description;
        //this.BasePrice = itemInfo.BasePrice;
        this.IconPath = itemInfo.IconPath;

        //this.Icon = Resources.Load<Sprite>(this.IconPath);
    }

    /// <summary>
    /// 空构造
    /// </summary>
    public ItemBase()
    {
        this.ID = -1;
    }
    #endregion 构造方法
}

/// <summary>
/// 实体道具类
/// 记录包括随机属性在内的全部属性
/// 用于实例化道具
/// </summary>
public class Item
{
    #region 属性
    /// <summary>
    ///ID
    ///</summary>
    public int ID { get; set; }

    /// <summary>
    /// 物品名称
    /// </summary>
    public string Name { get; set; }

    //物品类型
    public ItemType Type { get; set; }

    //品质
    //暂无

    //描述
    public string Description { get; set; }

    //基本价格
    public int BasePrice { get; set; }

    //出售价格
    //public int SellPrice { get; set; }

    //购买价格
    //public int BuyPrice { get; set; }

    //图标路径
    public string IconPath { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public Sprite Icon { get; set; }

    #endregion 属性

    #region 枚举
    //物品类型
    public enum ItemType
    {
        Fish,       //鱼类
        Material,   //材料
        Weapon,     //武器
        Tool,       //工具
        Consumable, //消耗品
    }

    #endregion 枚举

    #region 构造方法
    /// <summary>
    /// 根据ItemBase构造
    /// </summary>
    /// <param name="itemBase"></param>
    public Item(ItemBase itemBase)
    {
        this.ID = itemBase.ID;
        this.Name = itemBase.Name;
        //this.Type = itemBase.Type;
        this.Description = itemBase.Description;
        this.BasePrice = itemBase.BasePrice;
        this.IconPath = itemBase.IconPath;

        this.Icon = Resources.Load<Sprite>(itemBase.IconPath);
    }

    //有参数的构造
    public Item(int id, string name, ItemType type, string description, int basePrice, string iconPath)
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Description = description;
        this.BasePrice = basePrice;
        this.IconPath = iconPath;

        this.Icon = Resources.Load<Sprite>(this.IconPath);
    }
    //复制构造
    public Item(Item itemInfo)
    {
        this.ID = itemInfo.ID;
        this.Name = itemInfo.Name;
        this.Type = itemInfo.Type;
        this.Description = itemInfo.Description;
        this.BasePrice = itemInfo.BasePrice;
        this.IconPath = itemInfo.IconPath;

        this.Icon = itemInfo.Icon;
    }
    //空构造
    public Item()
    {
        this.ID = -1;
    }
    #endregion 构造方法
}