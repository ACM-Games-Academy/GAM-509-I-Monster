using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLauncherTest : MonoBehaviour
{
    public GameObject missilePrefab;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            cooldown = 5;
            GameObject newMissile = Instantiate(missilePrefab, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }
}
