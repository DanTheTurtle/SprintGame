using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntObjScript : MonoBehaviour
{

    double timer = 0;
    float step = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if(timer < 10) {
            transform.position = new Vector3(transform.position.x+step, transform.position.y, transform.position.z);
        }

        Debug.Log(timer);

        if(timer >=10) {
            transform.position = new Vector3(transform.position.x-step, transform.position.y, transform.position.z);
            timer = 0;
        }

    }
}
