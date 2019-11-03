using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class Boid : Agent
{
    private bool isSubmerged = true;
    Rigidbody rBody;
    int numBoids;
    bool isEaten = false;
    bool hasEaten = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        numBoids = GameObject.FindGameObjectsWithTag("Player").Length + GameObject.FindGameObjectsWithTag("Predator").Length;
    }

    // public Transform Target;
    public override void AgentReset()
    {
        if (this.transform.position.y < 0)
        {
            // If the Agent fell, zero its momentum
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 0.5f, 0);
        }

        // // Move the target to a new spot
        // Target.position = new Vector3(Random.value * 8 - 4,
        //                               0.5f,
        //                               Random.value * 8 - 4);
    }

    private bool isPredator(Rigidbody obj){
        return obj.gameObject.tag == "Predator";
    }


    public override void CollectObservations()
    {
        // Target and Agent positions
        Dictionary<string, GameObject> visibleObjects;
        // print(GetComponent<Vision>);
        visibleObjects = GetComponent<Vision>().ReturnVisionVector();

        AddVectorObs(this.transform.position);
        AddVectorObs(rBody.velocity);
        AddVectorObs(0);

        
        foreach(KeyValuePair<string, GameObject> entry in visibleObjects){
            AddVectorObs(entry.Value.GetComponent<Rigidbody>().position); 
            AddVectorObs(entry.Value.GetComponent<Rigidbody>().velocity);
            AddVectorObs(isPredator(entry.Value.GetComponent<Rigidbody>()) ? 1 : 2); 
        }

        int len = 7 * (numBoids - visibleObjects.Count - 1);
        AddVectorObs(new float[len]);
        // AddVectorObs(new float[numBoids * 7]);

    }
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Predator") {
            if (this.gameObject.tag != "Predator") {
                isEaten = true;
            }

        }
        else {
            if (this.gameObject.tag == "Predator") {
                hasEaten = true;
            }
            this.rBody.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Surface") {
            GetComponent<Rigidbody>().drag = 0;

            //GetComponent<Rigidbody>().useGravity = true;
            isSubmerged = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Surface") {
            GetComponent<Rigidbody>().drag = 1;

            //GetComponent<Rigidbody>().useGravity = false;
            isSubmerged = true;
        }
    }

    public float speed = 10;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        controlSignal.y = vectorAction[2];
        if(isSubmerged) rBody.AddForce(controlSignal * speed);
        else rBody.AddForce(new Vector3(0, 9.8f, 0));

        // Reached target
        if (isEaten)
        {
            SetReward(-1.0f);
            isEaten = false;
        }
        else if (hasEaten){
            SetReward(1.0f);
            hasEaten = false;
        }

    }
}