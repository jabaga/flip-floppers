using UnityEngine;

public class KillsPlayerOnTouch : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Collision with Player");
            Destroy(coll.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Collision with Player");
            Destroy(coll.gameObject);
        }

    }

}
