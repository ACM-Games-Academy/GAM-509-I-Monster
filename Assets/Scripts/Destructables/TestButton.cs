using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestButton : MonoBehaviour
{
    public bool button;
    public UnityEvent whenButtonPressed;

    // Update is called once per frame
    void Update()
    {
        if (button)
        {
            button = false;
            whenButtonPressed.Invoke();
        }
    }
}
