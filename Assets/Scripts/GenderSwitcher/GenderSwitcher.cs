using UnityEngine;
using AudioHelper;

public class GenderSwitcher : MonoBehaviour {

    public Gender gender;
    public AudioClip FemaleChangeSound;
    public AudioClip MaleChangeSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerStats>().SwitchGender(gender);

            if (gender == Gender.Female)
            {
                AudioManager.instance.RandomizeMiscSfx(FemaleChangeSound);
            }
            else if(gender == Gender.Male)
            {
                AudioManager.instance.RandomizeMiscSfx(MaleChangeSound);
            }
        }
    }
}
