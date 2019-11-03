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

    internal void ReturnVisionVector()
    { 
        print("fukkkk");
        // throw new NotImplementedException();
    }
}

//0-self, 1-other boid, 2-predator, -1 none
//Array pos/velocity/type
//array of size 9