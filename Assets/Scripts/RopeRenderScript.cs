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

    void OnTriggerExit2D(Collider2D col)
    {
        if ("Player".Equals(col.gameObject.tag))
        {
            Burn();
        }
    }

    private void Burn()
    {
        UnityEngine.Debug.Log("rope fire");
        this.GetComponent<ParticleSystem>().Play();
        Invoke("FinishBurn", 1f);
    }

    private void FinishBurn()
    {

        UnityEngine.Debug.Log("rope gone");
        this.GetComponent<LineRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(this.gameObject);
    }
}
