using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerModel pm;
    public PlayerView pv;

    private void OnEnable()
    {
        pm.healthChange += OnHealthChange;
    }

    private void OnDisable()
    {
        pm.healthChange -= OnHealthChange;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHealthChange(object sender, EventArgs e)
    {
        pv.UpdateHealthBar(pm.GetHealth());
    }
}
