using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMover : MonoBehaviour {

    public bool reverse;

    float velocity;
    Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        body.angularVelocity = velocity;
    }

    public void SetVelocity(float velocity)
    {
        this.velocity = velocity;
    }
}
