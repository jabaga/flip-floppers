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
