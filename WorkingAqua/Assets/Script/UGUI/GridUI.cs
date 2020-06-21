using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum GridType
    {
        Bag,
        Shop,
        Decoration,
        Body,
        Hand,
    }

    public GridType Type;

    public int Index = 0;

    public GameObject ItemUIGo;

    // Use this for initialization
	void Awake () {
        if (this.transform.childCount == 0)
        {
            ItemUIGo = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemUI"));
            ItemUIGo.transform.SetParent(this.transform);
            ItemUIGo.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            ItemUIGo.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
            ItemUIGo.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Action<Transform> OnEnter;

    public static Action OnExit;

    public static Action<Transform> OnLeftBeginDrag;

    public static Action<Transform, Transform> OnLeftEndDrag;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if(OnEnter!=null)
            {
                OnEnter(this.transform);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(OnExit != null)
        {
            OnExit();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftBeginDrag != null)
            {
                OnLeftBeginDrag(transform);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    { }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftBeginDrag != null)
            {
                if (eventData.pointerEnter == null)
                {
                    OnLeftEndDrag(transform, null);
                }
                else
                {
                    OnLeftEndDrag(transform, eventData.pointerEnter.transform);
                }
            }
        }
    }
}
