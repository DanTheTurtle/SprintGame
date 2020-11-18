using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalSpawner : MonoBehaviour
{    public GameObject coalthrowPrefab;
    private GameObject Coalthrowable;
    private float coalthrust = 10.0f;
    private Rigidbody2D coalrb;
    public Transform coalspawn;
    // Start is called before the first frame update
    void Start()
    {

    }
  
    // Update is called once per frame
    void Update() {
        // coalspawn = player.transform;

        // if(coalspawn.transform.position.x != player.transform.position.x) {
        //     Debug.Log("Transform's off");
        // }
    }
    // Update is called once per frame
    public void Spawn() {
        Coalthrowable = Instantiate(coalthrowPrefab, coalspawn);
        coalrb = Coalthrowable.GetComponent<Rigidbody2D>();
        coalrb.velocity = Vector2.up * coalthrust;
    }
}
