using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenDensity : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.SetDensity(2);
    }


}
