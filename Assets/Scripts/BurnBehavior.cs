using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    //public Collider2D collider;
    //public Renderer renderer;
    void Start()
    {
        this.GetComponent<ParticleSystem>().Stop();

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if ("Player".Equals(col.gameObject.tag))
        {
            Burn();
        }
    }

    private void Burn()
    {
        UnityEngine.Debug.Log("its on fire!");
        this.GetComponent<ParticleSystem>().Play();
        string tag = this.gameObject.tag;
        if (tag.Equals("Combustable"))
            Invoke("FinishBurn", 1f);
        else if (tag.Equals("WillNotBreak") || tag.Equals("Checkpoint"))
            return;
    }

    private void FinishBurn()
    {

        UnityEngine.Debug.Log("it gone");
        //renderer.enabled = false;
        //collider.enabled = false;
        Destroy(this.gameObject);
    }
}
