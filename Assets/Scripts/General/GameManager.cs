using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void ReloadScene() {
        OpenScene(SceneManager.GetActiveScene().name);
    }

    public void OpenScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    
    public void Quit() {
        Application.Quit();
    }

    private void OnApplicationFocus(bool focus) {
        if (focus) {
            Time.timeScale = 1;
        } else {
            Time.timeScale = 0;
        }
    }
}

[System.Flags]
public enum Gender {
    Male = (1<<0), Female = (1 << 1)
}

public static class EnumExtensions {
    public static Gender SwitchGender (this Gender type) {
        if (type == Gender.Male) return Gender.Female;
        else return Gender.Male;
    }
}

