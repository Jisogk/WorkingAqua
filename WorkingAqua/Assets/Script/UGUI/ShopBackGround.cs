using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBackGround : MonoBehaviour {

    private Animator Anim;

    // Use this for initialization
    void Awake() {
        Anim = GetComponent<Animator>();
        this.gameObject.SetActive(false);
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, 0);
        EventCenter.AddListener(EventCode.OnShopOpen, ShopOperation);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventCode.OnShopOpen, ShopOperation);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ShopOperation()
    {
        if (this.gameObject.activeInHierarchy)
        {
            HideBag();
        }
        else if (!this.gameObject.activeInHierarchy)
        {
            ShowBag();
        }
    }

    public void ShowBag()
    {
        PauseManager.instance.IsPause = true;

        this.gameObject.SetActive(true);
        if (Anim == null)
        {
            Anim = GetComponent<Animator>();
        }
        Anim.SetBool("isShow", true);
    }

    public void HideBag()
    {
        Anim.SetBool("isHide", true);
    }

    public void Stable()
    {
        Anim.SetBool("isShow", false);
    }

    public void InActive()
    {
        PauseManager.instance.IsPause = false;
        this.gameObject.SetActive(false);
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2 (-400,0);
    }
}
