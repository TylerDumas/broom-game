using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public string keyName = "Default";
    private Inventory playerInventory;
    private bool hasBeenPickedUp = false;

    private void Awake() {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other) {
        if( hasBeenPickedUp )
            return;
        
        if( other.tag == "Player" ){
            playerInventory.AddKey( this );
            hasBeenPickedUp = true;
            Destroy( gameObject );
        }
    }
}
