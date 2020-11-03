using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
//? Dan comments
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Rigidbody2D rb;
    private CircleCollider2D cc; //? maybe keep as circle, should not matter much
    private Transform lastCheckpoint;//? last checkpoint collided by player
    public float speed = 9f;
    public float jumpVelo = 17f;
    private float velocityx = 0f;
    private float accelerationx = 12f;
    private float jumpForce = 500f;
    private Boolean hasRejump = false; //?
    private bool isjumpedin = false;
    private bool canjumpin = false;
    private bool canjumpinmv = false;
    private bool isjumpedinmv = false;
    private GameObject intObj;
    void Start()
    {
        lastCheckpoint = transform;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            killPlayer();
        }

        velocityx =+ accelerationx * Input.GetAxis("Horizontal");
        if (Mathf.Abs(velocityx) > speed )
        {
            velocityx = speed * Input.GetAxis("Horizontal");
        }

        if(!isjumpedin && rb.gravityScale == 3.5) {
            transform.position = transform.position + new Vector3(velocityx * Time.deltaTime, 0, 0);
        }


        if (!isjumpedinmv && IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpVelo;
        }
        else if (hasRejump && Input.GetKeyDown(KeyCode.Space))
        {
            hasRejump = false;
            rb.velocity = Vector2.up * jumpVelo;
        }


        if(canjumpin && Input.GetKeyDown(KeyCode.DownArrow)) {
            this.gameObject.transform.localScale = new Vector3(0,0,0);
            transform.SetParent(intObj.transform,true);
            isjumpedin = true;
        } else if (canjumpinmv && Input.GetKeyDown(KeyCode.DownArrow)){
            this.gameObject.transform.localScale = new Vector3(0,0,0);
            isjumpedinmv = true;
        }

        if(isjumpedin && Input.GetKeyDown(KeyCode.UpArrow)) {
            UnityEngine.Debug.Log("Jumped out");
            transform.parent = null;
            this.gameObject.transform.localScale = new Vector3(1,1,1);
            isjumpedin = false;
            rb.velocity = Vector2.up * jumpVelo;
        }
        

        if (isjumpedinmv && Input.GetKeyDown(KeyCode.UpArrow)) {
            // Debug.Log("Jumped out");
            this.gameObject.transform.localScale = new Vector3(1,1,1);
            isjumpedinmv = false;
            rb.velocity = Vector2.up * jumpVelo;
        }

        if (isjumpedin)
        {
            transform.position = intObj.transform.position;
        }

        if (isjumpedinmv)
        {
            intObj.transform.position = new Vector3(this.gameObject.transform.position.x, intObj.transform.position.y, intObj.transform.position.z);
        }

        if (IsGrounded())
        {
            refreshJump();
        }
    }

    //private void OnTriggerEnter2D(Collider2D collider)

    public void refreshJump() { hasRejump = false; }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(cc.bounds.center, cc.radius, Vector2.down, .01f, groundMask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if("Checkpoint".Equals(col.gameObject.tag)) {
            lastCheckpoint = col.transform;
            col.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        else if ("CanJumpIn".Equals(col.gameObject.tag))
        {
            canjumpin = true;
            intObj = col.gameObject;
        } else if("CanJumpInMV".Equals(col.gameObject.tag)) {
            canjumpinmv = true;
            intObj = col.gameObject;
        }
        else if ("Kill".Equals(col.gameObject.tag))
        {
            BeginKillPlayer();
        }
    }
    private void BeginKillPlayer()
    {
        //TODO some death animation with smoke
        //Invoke("killPlayer", 5.0f);
        killPlayer();
    }
    private void killPlayer()
    {
        rb.gravityScale = 3.5f;
        refreshJump();
        transform.position = lastCheckpoint.position;
    }


    void OnCollisionExit2D(Collision2D col) {
        if(canjumpin) {
            canjumpin = false;
        }
        if(canjumpinmv) {
            canjumpinmv = false;
        }
    }
}
