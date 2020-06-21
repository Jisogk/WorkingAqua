using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPanel : MonoBehaviour
{
    public EquipSpace Hand;
    public EquipSpace Body;
    public EquipSpace Decoration;

    // Use this for initialization
    void Awake()
    {
        EventCenter.AddListener(EventCode.OnBagUIUpdate, UpdateEquipUI);
    }

    private void Start()
    {
        UpdateEquipUI();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventCode.OnBagUIUpdate, UpdateEquipUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEquipUI()
    {
        //刷新手
        if (StateManager.instance.Hand == null)
        {
            Hand.Grid.ItemUIGo.SetActive(false);
            Hand.Name.gameObject.SetActive(false);
        }
        else
        {
            Hand.Grid.ItemUIGo.GetComponent<ItemUI>().UpdateItemImage(StateManager.instance.Hand.Icon);
            Hand.Name.text = StateManager.instance.Hand.Name;
            Hand.Grid.ItemUIGo.SetActive(true);
            Hand.Name.gameObject.SetActive(true);
        }

        //刷新身体
        if (StateManager.instance.Body == null)
        {
            Body.Grid.ItemUIGo.SetActive(false);
            Body.Name.gameObject.SetActive(false);
        }
        else
        {
            Body.Grid.ItemUIGo.GetComponent<ItemUI>().UpdateItemImage(StateManager.instance.Body.Icon);
            Body.Name.text = StateManager.instance.Body.Name;
            Body.Grid.ItemUIGo.SetActive(true);
            Body.Name.gameObject.SetActive(true);
        }

        //刷新饰品
        if (StateManager.instance.Decoration == null)
        {
            Decoration.Grid.ItemUIGo.SetActive(false);
            Decoration.Name.gameObject.SetActive(false);
        }
        else
        {
            Decoration.Grid.ItemUIGo.GetComponent<ItemUI>().UpdateItemImage(StateManager.instance.Body.Icon);
            Decoration.Name.text = StateManager.instance.Body.Name;
            Decoration.Grid.ItemUIGo.SetActive(true);
            Decoration.Name.gameObject.SetActive(true);
        }
    }
}
