using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Start Walking");
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("Stop Walking");
        }
    }
}
