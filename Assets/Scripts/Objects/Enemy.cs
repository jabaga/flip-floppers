using UnityEngine;

public class Enemy : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            GameStateController.Instance.GameOver(false);
        }
    }
}
