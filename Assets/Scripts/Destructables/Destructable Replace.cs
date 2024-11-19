using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestructableReplace : MonoBehaviour
{
    // Start is called before the first frame update

    //string destructibleObjectName;
    public GameObject destroyedObjectGroup;
    // Update is called once per frame
    public void Destroyed(GameObject deadObject)
    {
        //destructibleObjectName = PrefabUtility.GetCorrespondingObjectFromSource(deadObject).name;
        //destroyedObjectGroup = ;
        Instantiate(destroyedObjectGroup, deadObject.transform);
        deadObject.SetActive(false);
    }
}
