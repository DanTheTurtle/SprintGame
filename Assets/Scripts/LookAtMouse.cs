using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public float factor = 0.25f;
    public float limit = 0.08f;

    private Vector3 center;

    void Start()
    {
    }


    void Update()
    {
        //Convert mouse pointer cords into a worldspace vector3
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
        pos.z = 0.0f;

        //Create a dir target based on mouse vector * factor
        Vector3 dir = pos * factor;

        //Clamp the dir target
        dir = Vector3.ClampMagnitude(dir, limit);
        dir.z = -1.7f;

        //Update the pupil position
        transform.position = transform.parent.position + dir;
    }
}
