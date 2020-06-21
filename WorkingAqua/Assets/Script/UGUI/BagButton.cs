using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagButton : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        EventCenter.AddListener(EventCode.OnShopOpen, ChangeActive);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventCode.OnShopOpen, ChangeActive);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BagUIContorl()
    {
        EventCenter.Broadcast(EventCode.OnBagOpen);
        EventCenter.Broadcast(EventCode.OnEquipOpen);
    }

    private void ChangeActive()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
