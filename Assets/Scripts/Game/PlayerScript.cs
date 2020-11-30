using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] GameObject gunRaycast;
    [SerializeField] GameObject gunProjectile;
    [SerializeField] GameObject meleeWeapon;
    [SerializeField] float shootRange = 40f;
    [SerializeField] float shootRate = 10f;
    [SerializeField] LineRenderer laser;
    private float nextShootTime = 0;
    private int weaponTypeID;
    private int weaponID;
    

    [Header("Visuals")]
    public Camera playerCamera;
    [SerializeField] Transform gunEnd;

    [Header("Gameplay")]
    public int initialHealth = 100;
    public int initialAmmo = 12;
    public float knockbackForce = 10;
    public float hurtDuration = 0.5f;
    public int raycastWeaponDamage = 1;



    private int health;
    public int Health { get { return health; } }

    private int ammo;
    public int Ammo { get { return ammo; } }

    private bool killed;
    public bool Killed { get { return killed;  } }

    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        weaponID = 1;
        gunProjectile.SetActive(true);
        gunRaycast.SetActive(false);

        health = initialHealth;
        ammo = initialAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            weaponID = 1;
            gunProjectile.SetActive(true);
            gunRaycast.SetActive(false);
            meleeWeapon.SetActive(false);
        }else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Raycast");
            weaponID = 2;
            gunProjectile.SetActive(false);
            gunRaycast.SetActive(true);
            meleeWeapon.SetActive(false);
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            weaponID = 3;
            meleeWeapon.SetActive(true);
            gunProjectile.SetActive(false);
            gunRaycast.SetActive(false);
        }




        //May need to change this for different weapon types
        if (Input.GetMouseButtonDown(0) && weaponID == 1)
        {
            if (ammo > 0 && Killed == false)
            {
                ammo--;
                GameObject bulletObject = ObjectPoolingManagerScript.Instance.GetBullet(true);
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }

        }

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
        if (Time.time > nextShootTime)
        {
            laser.enabled = false;
            if (Input.GetMouseButtonDown(0) && weaponID == 2 && Killed == false)
            {
                laser.enabled = true;
                laser.SetPosition(0, gunEnd.position);
                //Add a coroutine later for this
                nextShootTime = Time.time + (1 / shootRate);
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitinfo, shootRange))
                {
                    //Debug.Log(hitinfo.collider.gameObject.name);
                    laser.SetPosition(1, hitinfo.point);
                    if (hitinfo.collider.GetComponent<EnemyScript>() != null)
                    {
                        Debug.Log(hitinfo.collider.gameObject.name);
                        EnemyScript enemy = hitinfo.collider.GetComponent<EnemyScript>();
                        enemy.health -= raycastWeaponDamage;
                        if (enemy.health <= 0)
                        {
                            enemy.KilledByRayCast();
                        }
                        
                    }


                    
                }
                else
                {
                    laser.SetPosition(1, gunEnd.position + (playerCamera.transform.forward * 100));
                }

            }
        }
        

    }

    //Check for collisions
    private void OnTriggerEnter(Collider otherCollider)
    {
        //Collect AmmoCrate
        if(otherCollider.GetComponent<AmmoCrate>() != null)
        {
            //Collect AmmoCrate
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);

        }
        else if (otherCollider.GetComponent<HealthCrateScript>() != null)
        {
            //Collect HealthCrate
            HealthCrateScript healthCrate = otherCollider.GetComponent<HealthCrateScript>();
            health += healthCrate.health;

            Destroy(healthCrate.gameObject);

        }
        if (isHurt == false) 
        {
            GameObject hazard = null;

             if(otherCollider.GetComponent<EnemyScript>() != null)
             {
                EnemyScript enemy = otherCollider.GetComponent<EnemyScript>();
                if (enemy.Killed == false)
                {
                    hazard = enemy.gameObject;
                    health -= enemy.damage;

                }
 

            } 
            else if (otherCollider.GetComponent<BulletScript>() != null)
            {
                BulletScript bullet = otherCollider.GetComponent<BulletScript>();
                if (bullet.ShotByPlayer == false)
                {
                    hazard = bullet.gameObject;
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                }
            }
            if (hazard != null)
            {
                isHurt = true;
                // Perform the knockback effect.
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiverScript>().AddForce(knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutine());
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

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;

    }

    private void OnKill()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false; 
    }

}
