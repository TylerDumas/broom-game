using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private List<Key> keys;
    [SerializeField] private GameObject keyObjectParent = null;
    [SerializeField] private GameObject keyObjectPrefab = null;

    void Awake(){
        keys = new List<Key>();
    }

    public void AddKey( Key key ){
        keys.Add( key );
        GameObject newKey = Instantiate( keyObjectPrefab, Vector3.zero, transform.rotation );
        newKey.transform.SetParent( keyObjectParent.transform );
    }

    public void UseKey( Key key ){
        keys.Remove( key );
        Destroy( keyObjectParent.GetComponentInChildren<Key>().gameObject );
    }
}