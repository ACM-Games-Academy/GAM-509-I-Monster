using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "FuelContainerData" ,menuName = "Level/FuelContainer" ,order = 1)]
public class fuelContainerData : ScriptableObject
{
    public float damageRange;
    public float damage;

    public float health;

}
