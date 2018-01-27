using UnityEngine;

public class GameSettings : MonoBehaviour {

    public static GameSettings Instance;

    public Color maleParticleColor;
    public Color femaleParticleColor;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
}
