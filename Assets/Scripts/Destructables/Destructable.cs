using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [HideInInspector]
    private DestructableReplace replacementScript;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        replacementScript = GetComponent<DestructableReplace>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            replacementScript.Destroyed(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        health -= collision.relativeVelocity.magnitude * (collision.collider.bounds.size.magnitude / 2);
        if (health <= 0)
        {
            replacementScript.Destroyed(gameObject);
        }
    }
}
