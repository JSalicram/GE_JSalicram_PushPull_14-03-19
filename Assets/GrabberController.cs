using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberController : MonoBehaviour {
    //lowers rayCast from character position
    public float rayOffSetY = 0.5f;

    //distance an object can be grabbed within
    public float grabDist = 2f;

    //variable to store the grabbed game object
    private GameObject grabbedItem;
	
	// Update is called once per frame
	void Update () {
        //if E is pressed trigger the grab item function
        if (Input.GetKeyDown(KeyCode.E))
        {
            GrabItem();
        }
        //if E is released trigger the release the grab item function
        if (Input.GetKeyUp(KeyCode.E))
        {
            ReleaseItem();
        }
    }

    public void GrabItem()
    {
        //create a variable for the ray start that is equal to the position of the game object this is attatched to
        Vector2 rayStartPos = transform.position;
        //move the rayStart down by the rayOffSet
        rayStartPos.y = rayStartPos.y - rayOffSetY;
        //variable that defines the direction of the ray
        //this direction is equivalent to (1,0) multipled by the characters x scale i.e 1 or -1
        Vector2 rayDir = Vector2.right * transform.localScale.x;

        //queriesStartInColliders = false this stops the rayCast from returning the colliders that it is already within
        Physics2D.queriesStartInColliders = false;
        //this casts the ray
        //Physics2D.Raycast requires an origin point, a direction and a distance to cast
        RaycastHit2D castRay = Physics2D.Raycast(rayStartPos, rayDir, grabDist);

        //i.e if the castRay hits a collider
        if(castRay.collider != null)
        {
            //if the collider that is hit has the DraggableItemController
            if(castRay.collider.gameObject.GetComponent<DraggableItemController>() != null)
            {
                //store the item castRay hit as the grabbedItem
                grabbedItem = castRay.collider.gameObject;

                //gets and sets the FixedJoint2D.connectedBody on the grabbedItem to
                //the RigidBody2D attatched to this game object
                grabbedItem.GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
                //gets and enables the FixedJoint2D on the grabbedItem
                grabbedItem.GetComponent<FixedJoint2D>().enabled = true;
                //gets and sets to true the isGrabbed variable on the grabbedItem
                grabbedItem.GetComponent<DraggableItemController>().isGrabbed = true;
            }
        }
    }

    public void ReleaseItem()
    {
        //if the grabbedItem variable isnt null
        if(grabbedItem != null)
        {
            //get and set enable to false on the FixedJoint2D attatched to the grabbedItem
            grabbedItem.GetComponent<FixedJoint2D>().enabled = false;
            //gets and sets to false the isGrabbed variable on the grabbedItem
            grabbedItem.GetComponent<DraggableItemController>().isGrabbed = false;
        }
    }

    private void OnDrawGizmos()
    {
        //set the gizmo color to green
        Gizmos.color = Color.green;

        //this creates new variables that are the same as the ray cast variables

        //create a variable for the gizmo start that is equal to the position of the game object this is attatched to
        Vector2 gizmoStartPos = transform.position;
        //move the gizmoStart down by the rayOffSet
        gizmoStartPos.y = gizmoStartPos.y - rayOffSetY;
        //variable that defines the direction of the gizmo
        //this direction is equivalent to (1,0) multipled by the characters x scale i.e 1 or -1
        Vector2 gizmoDir = Vector2.right * transform.localScale.x;
    }
}
