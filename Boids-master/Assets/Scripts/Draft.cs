using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draft : MonoBehaviour
{
    [SerializeField] GameObject draft;
    GameObject[] draftCubes = new GameObject[3];
    Vector3 lastPos;
    Quaternion lastRot;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<3; i++)
        {
            lastPos = transform.position;
            lastRot = transform.rotation;
            draftCubes[i] = Instantiate(draft, transform.position - (transform.forward * (i + 1)/2), transform.rotation);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 2; i >= 0; i--)
        {
            if (i == 0)
            {
                draftCubes[i].transform.position = lastPos;
                draftCubes[i].transform.rotation = lastRot;
            }
            else
            {
                draftCubes[i].transform.position = draftCubes[i - 1].transform.position;
                draftCubes[i].transform.rotation = draftCubes[i - 1].transform.rotation;
            }
        }
        lastPos = transform.position;
        lastRot = transform.rotation;
    }
}
