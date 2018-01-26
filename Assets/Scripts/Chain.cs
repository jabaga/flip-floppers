using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {

    //new Vector2(-0.44f, 0)
    public Vector2 anchor;

	void Start () {
        Rigidbody2D[] bodies = GetComponentsInChildren<Rigidbody2D>();

        for(int i=0; i<bodies.Length; i++)
        {
            Rigidbody2D bodyToConnect = null;
            if(i == 0)
            {
                bodyToConnect = bodies[bodies.Length - 1];
            } else
            {
                bodyToConnect = bodies[i - 1];
            }

            //print(bodies[i].gameObject.name +" will joint to "+ bodyToConnect.gameObject.name);

            HingeJoint2D joint = bodies[i].gameObject.AddComponent<HingeJoint2D>();
            joint.connectedBody = bodyToConnect;
            joint.anchor = anchor;

            //print(Vector3.Distance(bodies[i].gameObject.transform.position, bodyToConnect.gameObject.transform.position) +" "+ Helper.DegreesBetweenObjects(bodies[i].gameObject, bodyToConnect.gameObject));
            
        }
	}
	
	void Update () {
		
	}
}
