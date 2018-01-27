using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMover : MonoBehaviour {

    public bool reverse;
    public float speed;

    void FixedUpdate()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        if(reverse == true)
            rotation.z -= speed / 10;
        else
            rotation.z += speed / 10;

        transform.rotation = Quaternion.Euler(rotation);
    }
}
