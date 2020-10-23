using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 5;
    public int damage = 5;

    private bool killed = false;
    public bool Killed { get { return killed; } }
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {

        

        if (other.GetComponent<BulletScript>() != null)
        {
            BulletScript bullet = other.GetComponent<BulletScript>();
            if(bullet.ShotByPlayer == true)
            {
                health -= bullet.damage;
                bullet.gameObject.SetActive(false);
            }


            if (health <= 0)
            {
                if (killed == false)
                {
                    killed = true;
                    OnKill();
                }
               
            }
        }
    }

  
    public void KilledByRayCast()
    {
        if (health <= 0)
        {
            if (killed == false)
            {
                killed = true;
                OnKill();
            }

        }
    }

    protected virtual void OnKill()
    {
        
    }

}
