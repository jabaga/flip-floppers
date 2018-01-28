using UnityEngine;

public class TextTrigger : MonoBehaviour {

    [SerializeField] private int step;

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player") {
            TutorialUI.Instance.ShowText(step);
            gameObject.SetActive(false);
        }
    }
}
