using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameControllerScript : MonoBehaviour {
    [Header("Game")]
    public PlayerScript player;
    public GameObject enemyContainer;

    [Header("UI")]
    public Text ammoText;
    [SerializeField] private Animator healthBarAnimator;
    public Text infoText;

    private bool gameOver = false;
    private float resetTimer = 3f;

    private void Start(){
        infoText.gameObject.SetActive(false);
    }






    // Update is called once per frame
    void Update(){
        //Basics
        healthBarAnimator.SetInteger( "health", player.Health );
        ammoText.text = player.Ammo.ToString();


        //Enemies Alive
        int aliveEnemies = 0;
        foreach (EnemyScript enemy in enemyContainer.GetComponentsInChildren<EnemyScript>())
        {
            if (enemy.Killed == false)
            {
                aliveEnemies++;
            }
        }

        if (aliveEnemies == 0) {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            //infoText.text = "Enemies Defeated";
        }

        if (player.Killed == true)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "You Died";
        }

        if (gameOver == true)
        {
            resetTimer -= Time.deltaTime;
            if (resetTimer <= 0)
            {
                //If condition is met go to other levels in this case back to menu
                //SceneManager.LoadScene("Menu");
            }
        }


    }
}
