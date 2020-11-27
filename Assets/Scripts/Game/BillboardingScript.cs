﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardingScript : MonoBehaviour {

    private GameObject player;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate(){
        transform.LookAt(player.transform.position);
    }
}
