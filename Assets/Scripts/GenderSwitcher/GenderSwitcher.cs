using UnityEngine;

public class GenderSwitcher : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            //other.GetComponent<PlayerInfo>().SwitchGender(); -> switches gender and plays animation
        }
    }
}
