using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//checks for fixedJoint2D on object. if none adds a FixedJoint and a RigidBody2D as It is Required
[RequireComponent(typeof(FixedJoint2D))]

public class DraggableItemController : MonoBehaviour {
    public bool isGrabbed = false;
    private float xPos;

	// Use this for initialization
	void Start () {
        //get starting x position
        xPos = transform.position.x;

        //disable fixed joint 2D
        GetComponent<FixedJoint2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if box is not grabbed
		if (isGrabbed == false)
        {
            //x position of the object stays the same but y can move. ie the box is affected by gravity
            transform.position = new Vector2(xPos, transform.position.y);
        }
        
        //if box is grabbed
        else
        {
            //
            xPos = transform.position.x;
        }
	}
}
