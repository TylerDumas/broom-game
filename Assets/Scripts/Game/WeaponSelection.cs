using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour {

    private int selectedWeapon = 0;
    [SerializeField] private GameObject weaponHolder = null;
    private void Start() {
        SelectWeapon();
    }

    private void Update() {
        if( Input.GetAxis( "Mouse ScrollWheel" ) > 0f ){
            if( selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
    }

    void SelectWeapon(){
        int i = 0;
        foreach( Transform weapon in weaponHolder.transform ){
            if( i == selectedWeapon ){
                weapon.gameObject.SetActive(true);
            }else{
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
