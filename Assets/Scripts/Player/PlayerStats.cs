using UnityEngine;

public class PlayerStats : MonoBehaviour{

    public static PlayerStats Instance;
    public ParticleSystem genderSwitchParticle;

    private PlayerController plCntr;
    
    private Gender playerGender = Gender.Male;

    public void SwitchGender(Gender gender) {
        if(gender == playerGender)
            return;

        playerGender = playerGender.SwitchGender();

        genderSwitchParticle.Play();

        GetComponent<Animator>().SetBool("Female", playerGender == Gender.Female);
    }

    public Gender GetGender() {
        return playerGender;
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
}
