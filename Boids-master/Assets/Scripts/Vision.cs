using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using MLAgents;

public class Vision : MonoBehaviour
{
    [SerializeField] SphereCollider visionRange;

    Dictionary<string, GameObject> visibleObjects = new Dictionary<string, GameObject>();
    // int numBoids;

    // void Awake(){
    //     numBoids = GameObject.FindGameObjectsWithTag("Player").Length;
    //     print(numBoids);
    // }

    private void OnTriggerEnter(Collider other)
    {
        // print("does it trigger")
        if (!visibleObjects.ContainsKey(other.gameObject.name) && (other.tag == "Player" || other.tag == "Predator")) {
            // print("here");
            visibleObjects.Add(other.gameObject.name, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        visibleObjects.Remove(other.gameObject.name);
    }

    internal Dictionary<string, GameObject> ReturnVisionVector()
    { 
        return visibleObjects;
        // double[] v = new double[numBoids * 7]; 
        // int c;
        // foreach(KeyValuePair<string, GameObject> entry in visibleObjects){
        //     // print(.GetType().GetProperties());
        //     // print(entry.Key);
        //     Vector3 pos = entry.Value.GetComponent<Rigidbody>().position;            
        //     Vector3 vel = entry.Value.GetComponent<Rigidbody>().velocity;
        //     v[c] = pos[0];
        //     v[c+1] = pos[1];
        //     v[c+2] = pos[2];


        //     v[c+3] = vel[0];
        //     v[c+4] = vel[1];
        //     v[c+5] = vel[2];

        //     v[c+6] = GetBoidType(entry.Value.GetComponent<Rigidbody>());            
        //     print(v);
        //     c += 7;

        //     // print(typeof(entry.Value.GetComponent<Rigidbody>().position));
        // }
        // throw new NotImplementedException();
    }
}

//0-self, 1-other boid, 2-predator, -1 none
//Array pos/velocity/type
//array of size 9