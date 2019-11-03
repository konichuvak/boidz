using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class Boid : Agent
{
    private bool isSubmerged = true;
    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public Transform Target;
    public override void AgentReset()
    {
        if (this.transform.position.y < 0)
        {
            // If the Agent fell, zero its momentum
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 0.5f, 0);
        }

        // Move the target to a new spot
        Target.position = new Vector3(Random.value * 8 - 4,
                                      0.5f,
                                      Random.value * 8 - 4);
    }

    public override void CollectObservations()
    {
        // Target and Agent positions
        // vec = GetComponent<Vision>().ReturnVisionVector();
        // print(Target.position);
        // print(this.transform.position);
        // print(rBody.velocity);

        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        // Agent velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }
    private void OnCollisionEnter(Collision other){
        this.rBody.velocity = Vector3.zero;

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


        // Rewards
        bool isEaten = false;
        // Dictionary<string, GameObject> neighbours = 
        GetComponent<Vision>().ReturnVisionVector();


        // // Reached target
        // if (distanceToTarget < 1.42f)
        // {
        //     SetReward(-1.0f);
        //     // Done();
        // }

        // Fell off platform
        // if (this.transform.position.y < 0)
        // {
        //     Done();
        // }

    }
}