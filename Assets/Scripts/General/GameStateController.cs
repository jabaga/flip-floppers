using System.Collections;
using UnityEngine;

public class GameStateController : MonoBehaviour {
    public static GameStateController Instance;

    public GameObject thePlayer;
    public bool isGameOver = false;

    [SerializeField] private GameObject LosingUI;
    [SerializeField] private GameObject WinningUI;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void GameOver(bool hasWon) {
        isGameOver = true;
        PlayerController.Instance.canMove = false;
        if (hasWon) StartCoroutine(WinGame());
        else StartCoroutine(LoseGame());
    }

    private IEnumerator LoseGame() {
        //TODO: animate
        //thePlayer.GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(0.01f); //the duration of the death animation
        Destroy(thePlayer);
        LosingUI.SetActive(true);
    }

    private IEnumerator WinGame() {
        //TODO: animate
        //thePlayer.GetComponent<Animator>().SetTrigger("Won");
        yield return new WaitForSeconds(0.01f); //the duration of the win animation
        WinningUI.SetActive(true);
    }
}
