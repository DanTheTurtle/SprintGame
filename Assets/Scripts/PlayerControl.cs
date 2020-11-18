using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//? Dan comments
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask explosionMask;
    private Rigidbody2D rb;
    private CircleCollider2D cc; //? maybe keep as circle, should not matter much
    private float speed = 7f;
    private float jumpVelo = 17f;
    private float explVelo = 50f;
    private float velocityx = 0f;
    private float accelerationx = 12f;
    private float jumpForce = 500f;
    private Boolean hasRejump = false;
    private bool isjumpedin = false;
    private bool canjumpin = false;
    private bool canjumpinmv = false;
    private bool isjumpedinmv = false;
    private Transform lastCheckpoint;    //? last checkpoint collided by player
    private GameObject intObj;

    public float maxHealth = 100.0f;
    public float currentHealth;
    private const float coef = 0.5f;
    public HealthBarControl healthBar;

    public Text coalText;
    private float coalCount = 0f;
    public GameObject coalSpawnPoint;


    public Text message;

    void Start()
    {
        lastCheckpoint = transform;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        coalText.text = coalCount.ToString();
    }

    void Update()
    {
        //Respawn player
        if (Input.GetKeyDown(KeyCode.R))
        {
            BeginKillPlayer();
        }

        //Player movement
        velocityx =+ accelerationx * Input.GetAxis("Horizontal");
        if (Mathf.Abs(velocityx) > speed)
        {
            velocityx = speed * Input.GetAxis("Horizontal");
        }

        if (!isjumpedin && rb.gravityScale == 3.5)
        {
            transform.position = transform.position + new Vector3(velocityx * Time.deltaTime, 0, 0);
        }

        //Player jumping
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpVelo;
        }
        else if (hasRejump && Input.GetKeyDown(KeyCode.Space))
        {
            hasRejump = false;
            rb.velocity = Vector2.up * jumpVelo;
        }
        
        if (IsGrounded())
        {
            refreshJump();
        } 
        //? relic of old game
        /*else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = (Vector2.up * jumpVelo * -1);
        }*/

        //Jump in and out functionality
        if(canjumpin && Input.GetKeyDown(KeyCode.DownArrow)) {
            this.gameObject.transform.localScale = new Vector3(0,0,0);
            isjumpedin = true;
            currentHealth += 15;
        } else if (canjumpinmv && Input.GetKeyDown(KeyCode.DownArrow)) {
            this.gameObject.transform.localScale = new Vector3(0,0,0);
            isjumpedinmv = true;
            currentHealth += 15;
        }

        if(isjumpedin) {
            transform.position = intObj.transform.position;
        } else if (isjumpedinmv) {
            intObj.transform.position = new Vector3(this.transform.position.x, intObj.transform.position.y, intObj.transform.position.z);
        }
        if(isjumpedin && Input.GetKeyDown(KeyCode.UpArrow)) {
            this.gameObject.transform.localScale = new Vector3(1,1,1);
            isjumpedin = false;
            rb.velocity = Vector2.up * jumpVelo;
        } else if (isjumpedinmv && Input.GetKeyDown(KeyCode.UpArrow)) {
            this.gameObject.transform.localScale = new Vector3(1,1,1);
            isjumpedinmv = false;
            rb.velocity = Vector2.up * jumpVelo;
        }

        //Coal throwing && decrement
        if(coalCount != 0 && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("Coal thrown");
            coalSpawnPoint.GetComponent<CoalSpawner>().Spawn();
            decrementCoal(1);
        }

        //Explosive ground code
        if(onExplosive()) {
            rb.velocity = Vector2.up * explVelo;
        }

        //Health Depletion
        if(currentHealth > 0){
            currentHealth -= coef * Time.deltaTime;
        }
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collider){
        //rain damage
        if("Rain".Equals(collider.gameObject.tag)){
            TakeDamage(5);
        }
        else if ("Collectable".Equals(collider.gameObject.tag))
        {
            incrementCoal(1);
            Destroy(collider.gameObject);
        }
        else if ("Checkpoint".Equals(collider.gameObject.tag))
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            lastCheckpoint = collider.gameObject.transform;
            collider.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    //Checks if player has already double jumped
    public void refreshJump() { hasRejump = false; }

    //Checks if player is on an explosive panel
    private bool onExplosive() {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(cc.bounds.center, cc.radius, Vector2.down, .01f, explosionMask);
        return raycastHit2D.collider != null;
    }

    //Checks if player is on ground
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(cc.bounds.center, cc.radius, Vector2.down, .01f, groundMask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ("CanJumpIn".Equals(col.gameObject.tag))
        {
            canjumpin = true;
            intObj = col.gameObject;

        }
        if("CanJumpInMV".Equals(col.gameObject.tag)) {
            canjumpinmv = true;
            intObj = col.gameObject;
        }
        //checkpoint
         if ("Kill".Equals(col.gameObject.tag))
        {
            BeginKillPlayer();
        }
        //PlayerTutorial
        if ("MovementText".Equals(col.gameObject.tag))
        {
            message.text = "Use W, A, S, D to move and SPACE to jump.";
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("movement triggered");
        }else if ("CheckpointText".Equals(col.gameObject.tag))
        {
            message.text = "The Orange Triangles are checkpoints to respawn and/or restore health. Be sure to jump on them to set checkpoint.";
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }else if ("CoalText".Equals(col.gameObject.tag))
        {
            message.text = "Collect Coal for Points.";
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }else if ("WaterText".Equals(col.gameObject.tag))
        {
            message.text = "Your health depletes over time so be careful of Rain and Water Puddles which add more damage. You can also press 'R' to respawn.";
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }else if ("JumpInOutText".Equals(col.gameObject.tag))
        {
            message.text = "You can restore health by jumping in and out of the rectangle Torch. Use the UP and Down arrows.";
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }else if ("RopeText".Equals(col.gameObject.tag))
        {
            message.text = "Burn through the rope to get to the other side.";
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    //Kill player
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
        refreshCoal();
        transform.position = lastCheckpoint.position;
    }

    void OnCollisionExit2D(Collision2D col) {
        if(canjumpin) 
        {
            canjumpin = false;
        }
        if(canjumpinmv) 
        {
            canjumpinmv = false;
        }
    }

    //Health depletion
    void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0){
            BeginKillPlayer();
        }
    }

    //Increase coal by i amt
    private void incrementCoal(int i)
    {
        coalCount += i;
        coalText.text = coalCount.ToString();
    }

    //Decrease coal by i amt
    private void decrementCoal(int i)
    {
        coalCount -= i;
        coalText.text = coalCount.ToString();
    }

    //Set coalcount to zero
    private void refreshCoal()
    {
        coalCount = 0;
    }
}
