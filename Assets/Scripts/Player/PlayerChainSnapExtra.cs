using UnityEngine;

public class PlayerChainSnapExtra : MonoBehaviour {

    public static PlayerChainSnapExtra Instance;

    public bool tempStop = false;

    private GameObject archivedChain;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public GameObject GetNewParent() {
        return archivedChain;
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Segment" && tempStop) {
            archivedChain = coll.gameObject;
        }
    }
}
