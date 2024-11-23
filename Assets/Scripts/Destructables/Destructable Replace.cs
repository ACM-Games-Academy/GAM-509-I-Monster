using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestructableReplace : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;
    public GameObject destroyedVariant;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.SetDensity(1);
    }
    // Update is called once per frame
    public void Destroyed(GameObject deadObject)
    {
        GetComponent<Collider>().isTrigger = true;
        gameObject.isStatic = true;
        transform.localScale = new Vector3(1, 1, 1);
        GameObject destroyedItem = Instantiate(destroyedVariant, transform.position, new Quaternion(transform.rotation.x + destroyedVariant.transform.rotation.x, transform.rotation.y , transform.rotation.z, transform.rotation.w),transform.parent);
        Rigidbody destroyedRB = destroyedItem.GetComponent<Rigidbody>();
        transform.DetachChildren();
        gameObject.SetActive(false);
    }

    public void DestroySelf()
    {
        Destroyed(gameObject);
    }
}
