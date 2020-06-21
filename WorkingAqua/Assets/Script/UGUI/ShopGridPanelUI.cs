using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopGridPanelUI : MonoBehaviour
{
    public int GoodNum;
    //public ShopManager shop;
    public List<GameObject> Grids;

    // Use this for initialization
    void Awake()
    {
        Grids = new List<GameObject>();
        init();
        UpdateItemUI();
        //EventCenter.AddListener(EventCode.OnBagUIUpdate, UpdateItemUI);
    }

    private void OnDestroy()
    {
        //EventCenter.RemoveListener(EventCode.OnBagUIUpdate, UpdateItemUI);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 初始化Inventory
    /// </summary>
    private void init()
    {
        if (this.transform.childCount > 0)
        {
            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < GoodNum; i++)
        {
            Grids.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/Grid")));
        }

        for (int i = 0; i < Grids.Count; i++)
        {
            Grids[i].transform.SetParent(this.transform);
            Grids[i].GetComponent<GridUI>().Index = i;
            Grids[i].GetComponent<GridUI>().Type = GridUI.GridType.Shop;
        }
    }

    public void UpdateItemUI()
    {
        for (int i = 0; i < Grids.Count; i++)
        {
            GridUI g = Grids[i].gameObject.GetComponent<GridUI>();
            if (ShopManager.instance.GetItem(g.Index) != null)
            {
                g.ItemUIGo.GetComponent<ItemUI>().UpdateItemImage(ShopManager.instance.GetItem(g.Index).Icon);
                g.ItemUIGo.SetActive(true);
            }
            else
            {
                g.ItemUIGo.GetComponent<ItemUI>().UpdateItemImage(null);
            }
        }
    }
}
