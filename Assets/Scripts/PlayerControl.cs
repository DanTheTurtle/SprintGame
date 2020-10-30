﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//? Dan comments
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Rigidbody2D rb;
    private CircleCollider2D cc; //? maybe keep as circle, should not matter much
    private Transform lastCheck;
    private float speed = 7f;
    private float jumpVelo = 17f;
    private float jumpForce = 500f;
    private Boolean hasRejump = true; //?
    private bool isjumpedin = false;
    private bool canjumpin = false;
    private bool canjumpinmv = false;
    private bool isjumpedinmv = false;
    private GameObject intObj;
    void Start()
    {
        lastCheck = transform;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        float horzIn = Input.GetAxis("Horizontal");
        if(!isjumpedin) {
            transform.position = transform.position + new Vector3(horzIn * speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            killPlayer();
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
        //? relic of old game
        /*else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = (Vector2.up * jumpVelo * -1);
        }*/

        if(canjumpin && Input.GetKeyDown(KeyCode.DownArrow)) {
            this.gameObject.transform.localScale = new Vector3(0,0,0);
            isjumpedin = true;
        } else if (canjumpinmv && Input.GetKeyDown(KeyCode.DownArrow)){
            this.gameObject.transform.localScale = new Vector3(0,0,0);
            isjumpedinmv = true;
        }

        if(isjumpedin && Input.GetKeyDown(KeyCode.UpArrow)) {
            // Debug.Log("Jumped out");
            this.gameObject.transform.localScale = new Vector3(1,1,1);
            isjumpedin = false;
            rb.velocity = Vector2.up * jumpVelo;
        }

        if(isjumpedinmv && Input.GetKeyDown(KeyCode.UpArrow)) {
            // Debug.Log("Jumped out");
            this.gameObject.transform.localScale = new Vector3(1,1,1);
            isjumpedinmv = false;
            rb.velocity = Vector2.up * jumpVelo;
        }

        if(isjumpedin) {
            transform.position = intObj.transform.position; 
        } 

        if(isjumpedinmv) {
            intObj.transform.position = new Vector3(this.gameObject.transform.position.x, intObj.transform.position.y, intObj.transform.position.z);
        }

        if (IsGrounded())
        {
            refreshJump();
        }
    }

    //private void OnTriggerEnter2D(Collider2D collider)

    public void refreshJump() { hasRejump = true; }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(cc.bounds.center, cc.radius, Vector2.down, .01f, groundMask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if("Checkpoint".Equals(col.gameObject.tag)) {
            lastCheck = col.transform;
        }
        else if ("CanJumpIn".Equals(col.gameObject.tag))
        {
            canjumpin = true;
            intObj = col.gameObject;
        } else if("CanJumpInMV".Equals(col.gameObject.tag)) {
            canjumpinmv = true;
            intObj = col.gameObject;
        }
    }

    private void killPlayer()
    {
        transform.position = lastCheck.position;
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
