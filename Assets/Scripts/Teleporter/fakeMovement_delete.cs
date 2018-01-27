using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakeMovement_delete : MonoBehaviour {
    public float speed = 10f;

	void Update () {
		if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.A)) {
            transform.position -= Vector3.right * speed * Time.deltaTime;
        }
    }
}
