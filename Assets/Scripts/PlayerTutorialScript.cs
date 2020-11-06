using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTutorialScript : MonoBehaviour
{
    public Text message;
    bool display = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if ("MovementText".Equals(col.gameObject.tag))
        {
            message.text = "Use W, A, S, D to move and SPACe to jump.";
            display = true;
            Debug.Log("movement triggered");
        }else if ("CheckpointText".Equals(col.gameObject.tag))
        {
            message.text = "The Orange Triangles are checkpoints to respawn and/or restore health.";
            display = true;
        }else if ("CoalText".Equals(col.gameObject.tag))
        {
            message.text = "Collect Coal for Points.";
            display = true;
        }else if ("WaterText".Equals(col.gameObject.tag))
        {
            message.text = "Be careful of Rain and Water Puddles.";
            display = true;
        }else if ("JumpInOut".Equals(col.gameObject.tag))
        {
            message.text = "You can restore health by jumping in and out of the rectangle Torch. Use the UP and Down arrows.";
            display = true;
        }else if ("RopeText".Equals(col.gameObject.tag))
        {
            message.text = "Burn through the rope to get to the other side.";
            display = true;
        }else{
            display = false;
        }
    }

    
    
}
