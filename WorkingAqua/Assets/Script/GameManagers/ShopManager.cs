using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public List<Item> merchandiseList;

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

        //EventCenter.AddListener(EventCode.OnManagerInitFinish, shopInit);
    }

    private void Start()
    {
        shopInit();
    }

    private void OnDestroy()
    {
        //EventCenter.RemoveListener(EventCode.OnManagerInitFinish, shopInit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void shopInit()
    {
        merchandiseList = InventoryManager.instance.OutputItemsInDic(Item.ItemType.Tool);
    }

    public Item GetItem(int i)
    {
        if (i < merchandiseList.Count)
        {
            return merchandiseList[i];
        }
        else
        {
            return null;
        }
    }
}
