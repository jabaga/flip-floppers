using UnityEngine;

[RequireComponent(typeof(PlayerBehavior))]
public class PlayerStats : MonoBehaviour {

    public static PlayerStats Instance;

    [SerializeField] private Animator anim;

    private PlayerBehavior plBeh;

    //TODO: make private
    public Gender playerGender = Gender.Male;

    private void Start() {
        plBeh = GetComponent<PlayerBehavior>();
    }

    public void SwitchGender() {
        playerGender = playerGender.SwitchGender();
        //TODO: Play animation for transformation
    }

    public Gender GetGender() {
        return playerGender;
    }

    public void EnablePlayer(bool isEnabled) {
        if (isEnabled) plBeh.DisableInput();
        else plBeh.EnableInput();
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
}
