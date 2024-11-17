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
            if (lifetime > 0)
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
            if (hit.transform != null || Vector3.Distance(transform.position, target.position) < 0.5f)
                if ((hit.transform != null || Vector3.Distance(transform.position, target.position) < 0.5f) && lifetime > -1)
                {
                    Explode();
                }
        }
        else
        {
            lifetime = -4;
        }
    }

    public void Explode()
    {
        GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        playerController pc = FindObjectOfType<playerController>();

        if (Vector3.Distance(Camera.main.transform.position, transform.position) < 3)
        {
            try
            {
                pc.TakeDamage(50);
            }
            catch
            {

            }
        }

        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < 3)
            {
                enemy.giveDamage(50);
            }
        }

        Destroy(gameObject);
    }
}
