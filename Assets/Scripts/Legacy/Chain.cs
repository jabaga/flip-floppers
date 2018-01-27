using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {
    
    public float chainGravityScale = 0;
    public float speed = 100;
    
    Vector2 anchor = new Vector2(0, 0);

    void Start () {
        GameObject wheelsParent = gameObject.transform.Find("Wheels").gameObject;
        GameObject chainParent = gameObject.transform.Find("Chain").gameObject;

        if (wheelsParent == null || chainParent == null)
            Debug.LogError("No wheels or chain found!");

        /**
         * CHAIN CONFIGURATION
         */
        Rigidbody2D[] chainBodies = chainParent.GetComponentsInChildren<Rigidbody2D>();
        for(int i=0; i<chainBodies.Length; i++)
        {
            Rigidbody2D bodyToConnect = null;
            if(i == 0)
            {
                bodyToConnect = chainBodies[chainBodies.Length - 1];
            } else
            {
                bodyToConnect = chainBodies[i - 1];
            }
            
            // joint body
            HingeJoint2D joint = chainBodies[i].gameObject.AddComponent<HingeJoint2D>();
            joint.connectedBody = bodyToConnect;
            joint.anchor = anchor;

            // make sure it is solid
            chainBodies[i].GetComponent<Collider2D>().isTrigger = false;
            
            // make sure it is solid
            chainBodies[i].GetComponent<Rigidbody2D>().gravityScale = chainGravityScale;
        }


        /**
         * WHEELS CONFIGURATION
         */
        for(int i=0; i < wheelsParent.transform.childCount; i++)
        {
            WheelMover wheelComponent = wheelsParent.transform.GetChild(i).GetComponent<WheelMover>();
            if(wheelComponent == null)
            {
                wheelComponent = wheelsParent.transform.GetChild(i).gameObject.AddComponent<WheelMover>();
            }

            float velocity = speed;
            if (wheelComponent.reverse == true)
                velocity = -speed;

            //wheelComponent.SetVelocity(velocity);
        }
    }
}
