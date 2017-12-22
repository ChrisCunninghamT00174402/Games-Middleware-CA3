using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {

        //sets camera and player to localplayer
        if (isLocalPlayer)
        {
            GetComponent<Player>().enabled = true;
            GetComponentInChildren<Camera>().enabled = true;

        }

	}
	

}
