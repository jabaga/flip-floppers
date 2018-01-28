using UnityEngine;
using AudioHelper;

public class GenderSwitcher : MonoBehaviour {

    public Gender gender;
    public AudioClip FemaleChangeSound;
    public AudioClip MaleChangeSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            AudioClip ac = (gender == Gender.Male) ? MaleChangeSound : FemaleChangeSound;

            other.GetComponent<PlayerStats>().SwitchGender(gender, ac);
        }
    }
}
