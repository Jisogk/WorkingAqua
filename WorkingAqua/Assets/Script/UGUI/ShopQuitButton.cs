using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ShopQuitButton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShopUIContorl()
    {
        Flowchart.BroadcastFungusMessage("OnShopQuit");
    }
}
