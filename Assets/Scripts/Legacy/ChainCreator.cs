using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainCreator : MonoBehaviour {

    public GameObject prefab;
    public float distance;

    Vector3 lastSpawnedObjectPos = new Vector3(-1000, -1000, 0);

	void Start () {
		
	}
	
	void FixedUpdate () {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;

            if (Vector3.Distance(lastSpawnedObjectPos, mousePos) >= distance)
            {
                GameObject spawned = GameObject.Instantiate(prefab, mousePos, Quaternion.Euler(0,0,0)) as GameObject;
                spawned.transform.SetParent(gameObject.transform);

                spawned.GetComponent<Collider2D>().isTrigger = true;

                lastSpawnedObjectPos = mousePos;
            }
        }
    }
}
