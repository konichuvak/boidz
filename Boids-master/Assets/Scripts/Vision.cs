using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] SphereCollider visionRange;

    Dictionary<string, GameObject> visibleObjects = new Dictionary<string, GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!visibleObjects.ContainsKey(other.gameObject.name) && other.tag == "Player")
            visibleObjects.Add(other.gameObject.name, other.gameObject);
    }

    private void OnTrigerExit(Collider other)
    {
        visibleObjects.Remove(other.gameObject.name);
    }
}
