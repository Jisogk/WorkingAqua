using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandHUD : MonoBehaviour
{
    public ItemUI HUD;
    public Text Name;

    // Use this for initialization
    void Awake()
    {
        EventCenter.AddListener(EventCode.OnBagUIUpdate, UpdateHUD);
    }

    private void Start()
    {
        UpdateHUD();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventCode.OnBagUIUpdate, UpdateHUD);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHUD()
    {
        //刷新手
        if (StateManager.instance.Hand == null)
        {
            HUD.gameObject.SetActive(false);
            Name.gameObject.SetActive(false);
        }
        else
        {
            HUD.UpdateItemImage(StateManager.instance.Hand.Icon);
            Name.text = StateManager.instance.Hand.Name;
            HUD.gameObject.SetActive(true);
            Name.gameObject.SetActive(true);
        }
    }
}
