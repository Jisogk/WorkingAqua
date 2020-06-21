using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMethodInFungus : MonoBehaviour
{
    public void Pause()
    {
        PauseManager.instance.IsPause = true;
    }

    public void Resume()
    {
        PauseManager.instance.IsPause = false;
    }

    public void OpenShop()
    {
        EventCenter.Broadcast(EventCode.OnBagOpen);
        EventCenter.Broadcast(EventCode.OnShopOpen);
    }

    /*public void QuitShop()
    {
        EventCenter.Broadcast(EventCode.OnShopOpen);
        EventCenter.Broadcast(EventCode.OnBagOpen);
    }*/
}
