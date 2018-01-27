using UnityEngine;

public class PlayerStats : MonoBehaviour{

    public static PlayerStats Instance;

    [SerializeField] private Animator anim;

    private PlayerController plCntr;
    
    private Gender playerGender = Gender.Male;

    public void SwitchGender() {
        playerGender = playerGender.SwitchGender();
        //TODO: Play animation for transformation
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
