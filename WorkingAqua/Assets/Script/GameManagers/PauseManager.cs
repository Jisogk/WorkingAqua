using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //静态类，存储是否全局暂停的信息

    public static PauseManager instance = null;

    //暂停变量
    //每个GameObject读取该变量决定自己是否暂停
    public bool IsPause = false;

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
    }

    private void OnDestroy()
    {
        
    }
}
