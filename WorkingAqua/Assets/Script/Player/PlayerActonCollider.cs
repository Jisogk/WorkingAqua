using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActonCollider : MonoBehaviour {

    public PlayerControll playerControll;

    private Vector3 posUP = new Vector3(0, 0.2f,0);
    private Vector3 posDown = new Vector3(0, -0.2f,0);
    private Vector3 posLeft = new Vector3(-0.2f, 0,0);
    private Vector3 posRight = new Vector3(0.2f, 0,0);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(playerControll.m_playerDirection)
        {
            case PlayerControll.PlayerDirection.UP:
                this.transform.localPosition = posUP;
                break;
            case PlayerControll.PlayerDirection.DOWN:
                this.transform.localPosition = posDown;
                break;
            case PlayerControll.PlayerDirection.LEFT:
                this.transform.localPosition = posLeft;
                break;
            case PlayerControll.PlayerDirection.RIGHT:
                this.transform.localPosition = posRight;
                break;

        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        playerControll.Contacts.Add(other.transform);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        playerControll.Contacts.Remove(collision.transform);
    }
}
