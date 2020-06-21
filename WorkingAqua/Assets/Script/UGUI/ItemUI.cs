using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{

    public Image itemImage;

    // Use this for initialization
    void Awake()
    {
        itemImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateItemImage(Sprite sp)
    {
        itemImage.sprite = sp;
    }
}
