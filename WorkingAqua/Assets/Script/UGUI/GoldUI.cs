using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public Text GoldText;

    private void Awake()
    {
        EventCenter.AddListener(EventCode.OnGoldChange, GoldUpdate);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventCode.OnGoldChange, GoldUpdate);
    }

    public void GoldUpdate()
    {
        GoldText.text = InventoryManager.instance.GetGold().ToString();
    }
}
