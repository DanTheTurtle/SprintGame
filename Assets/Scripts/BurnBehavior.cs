using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private bool IsBurning = false; //? is this on fire
    private bool canIgnite = false; //? can this be caught on fire. maybe of use later
    public float BurnTime = 5f; //? how long will this burn for
    private float internalTimer = 0f; //? for use in FixedUpdate
    private bool burnable; //? will this invoke FinishBurn() and delete the object

    void Start()
    {
        this.GetComponent<ParticleSystem>().Stop();
        int layer = this.gameObject.layer;//? 14 = combustable, 15 = willnotbreak
        if (tag.Equals("Combustable") || layer == 14)
        {
            burnable = true;
            canIgnite = true;

        }
        else if(tag.Equals("WillNotBreak") || tag.Equals("Checkpoint") || layer == 15)
        {
            burnable = false;
            canIgnite = true;
        }
        else
        {
            burnable = false;
            canIgnite = false;
        }

    }

    void FixedUpdate()
    {
        if(IsBurning)
        {
            internalTimer += Time.deltaTime;
            if(burnable && internalTimer >= BurnTime )
            {
                FinishBurn();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag.Equals("Player")) && canIgnite)
        {
            Burn();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if((col.collider.tag.Equals("Player") )&& canIgnite)
        {
            Burn();
        }
    }

    private void Burn()
    {
        UnityEngine.Debug.Log("its on fire!");
        this.GetComponent<ParticleSystem>().Play();
        IsBurning = true;
    }

    private void FinishBurn()
    {

        UnityEngine.Debug.Log("it gone");
        Destroy(this.gameObject);
    }
}
