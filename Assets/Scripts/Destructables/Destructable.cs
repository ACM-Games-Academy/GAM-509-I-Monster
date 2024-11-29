using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [HideInInspector]
    private DestructableReplace replacementScript;
    public float health;
    bool objectDestroyed = false;

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
        if (collision.impulse.magnitude>1) health -= collision.impulse.magnitude * (collision.collider.bounds.size.magnitude / 200);
        if (health < 0&&objectDestroyed==false)
        {
            replacementScript.Destroyed(gameObject);
            objectDestroyed = true;
        }
    }
}
