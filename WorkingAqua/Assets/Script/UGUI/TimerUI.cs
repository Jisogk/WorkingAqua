using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

    public TimeManager timeManager;

    public Text dateText;
    public Text timeText;
    public Image hourHandImage;

    private float rulerAngleZ = 0.0f;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rulerAngleZ = -(360.0f * timeManager.Hour / 12 + 30 * timeManager.Minute / 60);
        dateText.text = timeManager.Season_Str + " 月 " + timeManager.Date.ToString() + " 日";
        timeText.text = timeManager.Hour.ToString("D2") +":" + timeManager.Minute.ToString("D2");
        hourHandImage.gameObject.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f,0.0f, rulerAngleZ);
    }

    private void setHourHandAngle()
    {

    }
}
