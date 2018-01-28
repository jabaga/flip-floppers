using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {
    public static TutorialUI Instance;

    [TextArea(3, 10), SerializeField] private string allText;
    [SerializeField] private GameObject textContainer;
    [SerializeField] private Text tutText;
    private string[] txtArray;
    private bool isStopped;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        string[] separator = new string[1];
        separator[0] = Environment.NewLine;
        txtArray = allText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        for(int i = 0; i < txtArray.Length; i++) {
            txtArray[i] = txtArray[i].Replace("\\n", "\n");
        }
        textContainer.SetActive(false);
    }

    public void ShowText(int numb) {
        textContainer.SetActive(true);
        if (numb < txtArray.Length) {
            tutText.text = txtArray[numb];
            isStopped = true;
            Time.timeScale = 0;
        } else {
            HideText();
        }
    }

    public void HideText() {
        textContainer.SetActive(false);
        tutText.text = "";
    }

    public void Update() {
        if (isStopped && Input.GetKeyDown(KeyCode.KeypadEnter)) {
            isStopped = false;
            Time.timeScale = 1;
            HideText();
        }
    }
}
