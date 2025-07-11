using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Missile : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float lifetime = 0;
    public GameObject explosionPrefab;
    public Transform aimingDummy;
    public XRGrabInteractable grabInteractable;
    public Rigidbody rb;
    public ParticleSystem fire;
    public bool thrown;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.EmissionModule emission = fire.emission;
        emission.rateOverTime = (grabInteractable.isSelected) ? 0 : 100;
        if (!grabInteractable.isSelected)
        {
            lifetime += Time.deltaTime;
            if (lifetime > 0 && !thrown)
            {
                aimingDummy.LookAt(target.transform.position);
                aimingDummy.Rotate(90, 0, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, aimingDummy.rotation, Time.deltaTime * (2 + (10 * Mathf.Max(10 - Vector3.Distance(transform.position, target.position), 0))));
            }
            else
            {

            }
            //transform.position += transform.forward * speed * Time.deltaTime;
            rb.velocity = transform.up * speed;


            int LayersToIgnore = ~(1 << LayerMask.NameToLayer("No Collision"));
            RaycastHit hit;
            Physics.Raycast(transform.position + transform.TransformDirection(Vector3.up * 0.5f), transform.TransformDirection(Vector3.up), out hit, 2f);
            if ((hit.transform != null || (Vector3.Distance(transform.position, target.position) < 0.5f && !thrown)) && lifetime > -1)
            {
                Explode();
            }
        }
        else
        {
            thrown = true;
            lifetime = 0.1f;
            rb.angularVelocity = Vector3.zero;
            rb.freezeRotation = true;
        }
    }

    public void Explode()
    {
        GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (target ? target.GetComponentInParent<playerController>() : false)
        {
            if (Vector3.Distance(target.position, transform.position) < 5)
            {
                target.GetComponentInParent<playerController>().TakeDamage(20);
            }
        }
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < 5)
            {
                enemy.giveDamage(100);
            }
        }

        Destroy(gameObject);
    }
}