using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestructableReplace : MonoBehaviour
{
    // Start is called before the first frame update

    public string destructibleObjectName;
    // Update is called once per frame
    public void Destroyed(GameObject deadObject)
    {
        //destructibleObjectName = PrefabUtility.GetCorrespondingObjectFromSource(deadObject).name;
        //destroyedObjectGroup = ;
        Instantiate(Resources.Load("Buildings/"+destructibleObjectName), deadObject.transform.position, deadObject.transform.rotation);
        deadObject.SetActive(false);
    }

    public void DestroySelf()
    {
        Destroyed(gameObject);
    }
}
