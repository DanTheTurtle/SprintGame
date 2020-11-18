using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ForegroundScroll : MonoBehaviour
{
    public float xFactor = 0.5f;
    public float yFactor = 0.2f;
    public GameObject player;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x * xFactor, player.transform.position.y * yFactor + 0.7f, transform.position.z);
    }
}
