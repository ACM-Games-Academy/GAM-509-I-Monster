using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UI.GridLayoutGroup;

public class Missile : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float lifetime = 0;
    public GameObject explosionPrefab;
    public Transform aimingDummy;

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
        lifetime += Time.deltaTime;
        if (lifetime > 2)
        {
            aimingDummy.LookAt(target.transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, aimingDummy.rotation, Time.deltaTime * (2 + (10 * Mathf.Max(10 - Vector3.Distance(transform.position, target.position), 0))));
        }
        else
        {

        }
        transform.position += transform.forward * speed * Time.deltaTime;


        int LayersToIgnore = ~(1 << LayerMask.NameToLayer("No Collision"));
        RaycastHit hit;
        Physics.Raycast(transform.position + transform.TransformDirection(Vector3.back * 1f), transform.TransformDirection(Vector3.forward), out hit, 2f);
        if (hit.transform != null || Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            Explode();
        }
    }

    public void Explode()
    {
        GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        /*PlayerModel pm = FindObjectsOfType<PlayerModel>()[0];

        if (pm ? Vector3.Distance(transform.position, pm.transform.position) < 2.5f : false)
        {
            pm.ReduceHealth(10);
        }
        */
        Destroy(gameObject);
    }
}
