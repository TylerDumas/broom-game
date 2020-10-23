using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManagerScript : MonoBehaviour
{
    private static ObjectPoolingManagerScript instance;
    public static ObjectPoolingManagerScript Instance { get { return instance; } }

    public GameObject bulletPrefab;
    public int bulletAmount = 20;

    private List<GameObject> bullets;
    
    void Awake()
    {
        instance = this;

        //Preload bullets
        bullets = new List<GameObject>(bulletAmount);


        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            bullets.Add(prefabInstance);
        }
    }

    public GameObject GetBullet(bool shotByPlayer)
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponent<BulletScript>().ShotByPlayer = shotByPlayer;
                return bullet;
            }
        }

        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<BulletScript>().ShotByPlayer = shotByPlayer;
        bullets.Add(prefabInstance);

        return prefabInstance;

    }
}
