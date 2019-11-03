using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

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
        // double[] vec = new double[]; 
        print(GameObject.FindObjectsOfType(typeof(Boid))); //returns Object[]
        foreach(KeyValuePair<string, GameObject> entry in visibleObjects){
            // print(.GetType().GetProperties());
            // print(entry.Key);
            print(entry.Value.GetComponent<Rigidbody>().position);
            print(entry.Value.GetComponent<Rigidbody>().velocity);
        }
        // throw new NotImplementedException();
    }
}

//0-self, 1-other boid, 2-predator, -1 none
//Array pos/velocity/type
//array of size 9