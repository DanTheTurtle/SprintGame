using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMove : MonoBehaviour
{
    // Start is called before the first frame update
    private bool started = false;
    public GameObject endpoint;
    public float jumpVelo = 17f;
    public float travelTime = 1f;

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if("Player".Equals(col.gameObject.tag))  
        {
            if (!started)
            {
                Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
                rb.velocity = (endpoint.transform.position - col.gameObject.transform.position) * travelTime;
                rb.gravityScale = 0;
            }
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if ("Player".Equals(col.gameObject.tag) && !started)
        {
            ToggleRopes();
        }
        else if("Player".Equals(col.gameObject.tag) && started)
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up * jumpVelo;
            rb.gravityScale = 3.5f;
            ToggleRopes();
        }
    }

    public void ToggleRopes()
    {
        endpoint.GetComponent<RopeMove>().ToggleRope();
        ToggleRope();
    }
    public void ToggleRope()
    {
        started = !started;
    }
}
