﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntObjScript : MonoBehaviour
{

    float step = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = new Vector3(transform.position.x+step, transform.position.y, transform.position.z);
    }
}
