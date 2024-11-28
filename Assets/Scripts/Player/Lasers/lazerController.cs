using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class lazerController : MonoBehaviour
{
    [SerializeField] private float laserEnergy;
    public float LaserEnergy
    { get { return laserEnergy; } }

    [SerializeField] private float laserMaxEnergy;
    public float LaserMaxEnergy
    { get { return laserMaxEnergy; } }

    [SerializeField] private float energyDrain;
    [SerializeField] private float energyRegen;

    [SerializeField] private bool overheated = false;
    [SerializeField] private float overheatedTime;

    private bool canFire = true;
    private bool hasFired = false;

    Coroutine overheatedCo;
    Coroutine CantFireCo;

    [SerializeField] private InputActionReference leftGrab;
    [SerializeField] private InputActionReference rightGrab;
    [SerializeField] private GameObject leftLaser;
    [SerializeField] private GameObject rightLaser;

    // Update is called once per frame
    void Update()
    {
        FireLazers(leftGrab, leftLaser, energyDrain);
        FireLazers(rightGrab, rightLaser, energyDrain);

        EnergyRegen();
    }

    private void FireLazers(InputActionReference grab, GameObject lazer, float energyDrain)
    {
        if (grab.action.ReadValue<float>() > 0.5f && laserEnergy > 0 && canFire)   //user is pressing the button and has enough energy to fire - I was going to check if it was overheated 
        {                                                               //but the laserEnergy would be equal to zero in that scenario
            hasFired = true;
            laserEnergy -= energyDrain * Time.deltaTime;   //draining the lasers energy
            
            lazer.SetActive(true);

            if (laserEnergy < 0)   //if the energy is below zero the laser will overheat and won't regenerate energy till its cooled down
            {
                laserEnergy = 0;
                overheated = true;
                canFire = false;

                if (overheatedCo == null)  //this checks if there's already a coroutine running - This works but sometimes the 
                {
                    overheatedCo = StartCoroutine(OverheatedTimer(overheatedTime));
                }
            }

            Debug.Log("Firing");
        }

        else if (grab.action.ReadValue<float>() > 0.5f && laserEnergy <= 0)  //button is presssed but not enough energy
        {
            lazer.SetActive(false);

            if (CantFireCo == null) //this checks if the can't fire effect is already being played
            {
                CantFireCo = StartCoroutine(CantFireEffect());
            }           

            Debug.Log("Firing no energy");
        }

        else  //button is not pressed - Disable lasers
        {
            lazer.SetActive(false);
        }
    }

    private void EnergyRegen()
    {
        //this checks if the gun is not overheated and if the lazer energy is below themax 
        //if this is true it will regenerate the lazers energy
        if (!overheated && laserEnergy < laserMaxEnergy && !hasFired)
        {
            laserEnergy += energyRegen * Time.deltaTime;

            if (laserEnergy > laserMaxEnergy)
            {
                laserEnergy = laserMaxEnergy;
            }
        }  
    }

    private IEnumerator OverheatedTimer(float time)
    {
        Debug.Log("Overheated timer started");
        yield return new WaitForSeconds(time);
        overheated = false;   
        //now it should start regenerating

        //now it needs to wait till its regenerated enough energy
        //I was goijng to use a waitUntil but since it regens based on time.deltatime i can calculate how long it will take and wait that many seconds
        float secondsTillHalf;
        secondsTillHalf = (laserMaxEnergy / 2) / energyRegen;  //this shouldfind the amount of seconds till it regenerates 50%
        
        yield return new WaitForSeconds(secondsTillHalf);
        canFire = true;

        overheatedCo = null;   //a function checks if this coroutine is null so it doesn't make 2. So i need to set it to null when the coroutine is over 
    }

    private IEnumerator CantFireEffect()
    {
        Debug.Log("CantFireEffect has started");

        CantFireCo = null;
        yield return null;
    }
}
