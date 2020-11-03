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
    private float speed = 7f;
    private float jumpVelo = 17f;
    private float jumpForce = 500f;
    private Boolean hasRejump = true; //?
    private bool isjumpedin = false;
    private bool canjumpin = false;
    private GameObject intObj;
    
    public float maxHealth = 100.0f;
    public float currentHealth;
    private const float coef = 0.5f;
    public HealthBarControl healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        float horzIn = Input.GetAxis("Horizontal");
        if(!isjumpedin) {
            transform.position = transform.position + new Vector3(horzIn * speed * Time.deltaTime, 0, 0);
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
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
            transform.SetParent(intObj.transform);
            isjumpedin = true;
            currentHealth += 25;
        }

        if(isjumpedin && Input.GetKeyDown(KeyCode.UpArrow)) {
            Debug.Log("Jumped out");
            transform.parent = null;
            this.gameObject.transform.localScale = new Vector3(1,1,1);
            isjumpedin = false;
            rb.velocity = Vector2.up * jumpVelo;

        }

        if(transform.parent != null) {
            transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);
        }

        if (IsGrounded())
        {
            refreshJump();
        }

        //Health Depletion
        currentHealth -= coef * Time.deltaTime;
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collider){
        //rain damage
        if("Rain".Equals(collider.gameObject.tag)){
            TakeDamage(20);
        }
    }

    public void refreshJump() { hasRejump = true; }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(cc.bounds.center, cc.radius, Vector2.down, .01f, groundMask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if("CanJumpIn".Equals(col.gameObject.tag)) {
            canjumpin = true;
            intObj = col.gameObject;
            
        }
        //checkpoint
        if("Checkpoint".Equals(col.gameObject.tag)){
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if(canjumpin) {
            canjumpin = false;
        }
    }

    void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
