using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityText : MonoBehaviour
{
    // Start is called before the first frame update
    private string message;
    void Start()
    {
        message = GetComponent<TextMesh>().text;
        GetComponent<TextMesh>().text = "";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Player"))
        {
            GetComponent<TextMesh>().text = message;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            GetComponent<TextMesh>().text = "";
        }
    }
}
