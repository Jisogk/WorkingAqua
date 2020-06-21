using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridPanelUI : MonoBehaviour
{

    public List<GameObject> Grids;

    // Use this for initialization
    void Awake()
    {
        init();
        UpdateItemUI();
        EventCenter.AddListener(EventCode.OnBagUIUpdate, UpdateItemUI);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventCode.OnBagUIUpdate, UpdateItemUI);
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
        Grids = new List<GameObject>();

        if (this.transform.childCount > 0)
        {
            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < InventoryManager.instance.GetInventoryCap(); i++)
        {
            Grids.Add(Instantiate(Resources.Load<GameObject>("Prefabs/UI/Grid")));
        }

        for (int i = 0; i < Grids.Count; i++)
        {
            Grids[i].transform.SetParent(this.transform);
            Grids[i].GetComponent<GridUI>().Index = i;
            Grids[i].GetComponent<GridUI>().Type = GridUI.GridType.Bag;
        }
    }

    public void UpdateItemUI()
    {
        for (int i = 0; i < Grids.Count; i++)
        {
            if (InventoryManager.GetItem(Grids[i].gameObject.GetComponent<GridUI>().Index) != null)
            {
                Grids[i].gameObject.GetComponent<GridUI>().ItemUIGo.GetComponent<ItemUI>().UpdateItemImage(InventoryManager.GetItem(Grids[i].gameObject.GetComponent<GridUI>().Index).Icon);
                Grids[i].gameObject.GetComponent<GridUI>().ItemUIGo.SetActive(true);
            }
            else
            {
                Grids[i].gameObject.GetComponent<GridUI>().ItemUIGo.GetComponent<ItemUI>().UpdateItemImage(null);
                Grids[i].gameObject.GetComponent<GridUI>().ItemUIGo.SetActive(false);
            }
        }
    }
}
