using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform start;
    public Transform end;
    void Start()
    {
        this.GetComponent<ParticleSystem>().Stop();

        LineRenderer lr = this.GetComponent<LineRenderer>();
        lr.SetPosition(0, start.position);
        lr.SetPosition(1, end.position);

    }

}
