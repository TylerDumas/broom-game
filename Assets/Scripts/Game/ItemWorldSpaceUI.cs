using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWorldSpaceUI : MonoBehaviour {
    [SerializeField] private string itemName = "Default";
    private Text worldSpaceText = null;
    private bool displayName = false;
    [SerializeField] private float fadeTime = 0.1f;

    private void Awake() {
        worldSpaceText = GetComponentInChildren<Text>();
    }

    void Start(){
        worldSpaceText.color = Color.clear;
    }

    private void Update() {
        FadeText();
    }

    void FadeText(){
        if( displayName ){
            worldSpaceText.text = itemName;
            worldSpaceText.color = Color.Lerp( worldSpaceText.color, Color.white, fadeTime * Time.deltaTime );
        }else{
            worldSpaceText.color = Color.Lerp( worldSpaceText.color, Color.clear, fadeTime * Time.deltaTime );
        }
    }

    private void OnMouseOver() {
        displayName = true;
    }

    private void OnMouseExit() {
        displayName = false;
    }
}
