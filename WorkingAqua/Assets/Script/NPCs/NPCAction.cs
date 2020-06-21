using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NPCAction : MonoBehaviour {

    public string TalkMessage;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TalkTrigger()
    {
        Flowchart.BroadcastFungusMessage(TalkMessage);
    }
}
