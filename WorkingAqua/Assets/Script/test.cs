using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    public Sprite sprite;

    // Use this for initialization
	void Awake () {

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/FishIcon/Albacore");
        sprite = Resources.Load<Sprite>("Albacore");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
