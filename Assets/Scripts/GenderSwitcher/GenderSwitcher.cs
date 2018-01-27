using UnityEngine;

public class GenderSwitcher : MonoBehaviour {

    public Gender gender;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerStats>().SwitchGender(gender);
        }
    }
}
