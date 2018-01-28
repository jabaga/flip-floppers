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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            GameManager.Instance.OpenScene("Tutorial");
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.Quit();
        }
    }
}
