using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDepth : MonoBehaviour {

    //设置物体显示的深度，实现遮挡效果

    //设置物体是否移动，不移动的物体无需实时更新深度
    public bool isMoving = false;

    // Use this for initialization
	void Start () {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.y);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isMoving)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.y);
        }
    }
}
