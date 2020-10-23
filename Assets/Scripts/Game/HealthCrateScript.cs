using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrateScript : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject container;
    public float rotationSpeed = 180f;
    [Header("Gameplay")]
    public int health = 25;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        container.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
