using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

    public Vector2 velocity;
    public float reverseAfter;

    float time = 0;
    float lastTimeReversed = 0;
    Rigidbody2D body;

	void Start () {
        body = GetComponent<Rigidbody2D>();

        body.velocity = velocity;
	}
	
	void FixedUpdate () {
        time += Time.fixedDeltaTime;

        if(time - lastTimeReversed >= reverseAfter)
        {
            velocity = new Vector2(-velocity.x, -velocity.y);

            body.velocity = velocity;

            lastTimeReversed = time;
        }


	}
}
