using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
  public void OnPlay()
  { 
        //Change this to level 1 once it's completed
        SceneManager.LoadScene("SampleScene");
  }
}
