using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;

    //装备
    public Item Hand;
    public Item Body;
    public Item Decoration;

    //钓鱼相关的人物属性


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

    // Update is called once per frame
    void Update()
    {

    }

    public bool HandEquiped()
    {
        if (Hand == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
