using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GedanLaser : MonoBehaviour
{
    public LineRenderer[] laserRenderers;
    public Transform laserImpact;
    public ParticleSystem stripes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPoint = transform.position + (transform.forward * 0.5f);
        Vector3 endPoint = Vector3.zero;
        RaycastHit hit = new RaycastHit();
        int LayersToIgnore = ~(1 << LayerMask.NameToLayer("No Collision") | 1 << LayerMask.NameToLayer("Player Attack Hitbox") | 1 << LayerMask.NameToLayer("2D Section") | 1 << LayerMask.NameToLayer("Viewmodel") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Player 1") | 1 << LayerMask.NameToLayer("Player 2") | 1 << LayerMask.NameToLayer("Player 3") | 1 << LayerMask.NameToLayer("Player 4") | 1 << LayerMask.NameToLayer("Enemy"));
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100, LayersToIgnore);
        if (hit.transform != null)
        {
            endPoint = hit.point + (transform.TransformDirection(Vector3.back) * 0.5f);
            laserImpact.position = endPoint + (transform.TransformDirection(Vector3.forward) * 0.5f);
            laserImpact.rotation = Quaternion.LookRotation(hit.normal);
            print(hit.transform.name);
        }
        else
        {
            endPoint = startPoint + (transform.TransformDirection(Vector3.forward) * 100);
            laserImpact.position = endPoint + (transform.TransformDirection(Vector3.forward) * 0.5f);
            laserImpact.rotation = transform.rotation;
        }

        stripes.transform.position = transform.position;
        stripes.startLifetime = Vector3.Distance(startPoint, endPoint) / 14;
        stripes.transform.LookAt(laserImpact);

        foreach (LineRenderer line in laserRenderers)
        {
            line.SetPosition(0, startPoint);
            line.SetPosition(1, endPoint);
        }



        RaycastHit[] hits;
        hits = Physics.SphereCastAll(startPoint, 1, stripes.transform.forward, Vector3.Distance(startPoint, endPoint), (int)(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Player 1") | 1 << LayerMask.NameToLayer("Player 2") | 1 << LayerMask.NameToLayer("Player 3") | 1 << LayerMask.NameToLayer("Player 4") | 1 << LayerMask.NameToLayer("Enemy")));

        foreach (RaycastHit currentHit in hits)
        {
            
        }
    }
}
