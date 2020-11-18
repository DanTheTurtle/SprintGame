using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float xFactor = 0.1f;
    public float yFactor = 0.01f;
    public GameObject player;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x * xFactor, player.transform.position.y * yFactor + 1.7f, transform.position.z);
    }
}
