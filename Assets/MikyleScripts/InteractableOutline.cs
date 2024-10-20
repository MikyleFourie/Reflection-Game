using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableOutline : MonoBehaviour
{
    SphereCollider triggerSphere;
    void Start()
    {
        triggerSphere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
