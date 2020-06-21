using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItemUI : ItemUI {

    // Use this for initialization
	void Awake () {
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hide()
    {
        gameObject.SetActive(false);
        GetComponent<RectTransform>().localPosition = new Vector2(0, 1000);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void setRectPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().localPosition = pos;
    }
}
