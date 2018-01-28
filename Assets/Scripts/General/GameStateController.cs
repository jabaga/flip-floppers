using System.Collections;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using AudioHelper;

public class GameStateController : MonoBehaviour {
    public static GameStateController Instance;

    public bool isGameOver = false;

    [SerializeField] private GameObject LosingUI;
    [SerializeField] private GameObject WinningUI;
    [SerializeField] private ProCamera2D procam;

    [Space(10), SerializeField] private AudioClip DieSound;
    [SerializeField] private AudioClip FailSound;
    [SerializeField] private GameObject ParticleDieEffect;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void GameOver(bool hasWon) {
        if (!isGameOver) {
            isGameOver = true;
            PlayerController.Instance.canMove = false;
            procam.enabled = false;
            if (hasWon) StartCoroutine(WinGame());
            else StartCoroutine(LoseGame());
        }
    }

    private IEnumerator LoseGame() {
        Instantiate(ParticleDieEffect, transform.position, transform.rotation);

        AudioManager.instance.RandomizeMiscSfx(DieSound);
        AudioManager.instance.StopMainSound();
        yield return new WaitForSeconds(2f);
        AudioManager.instance.RandomizeMiscSfx(FailSound);
        yield return new WaitForSeconds(0.01f); 
        Destroy(PlayerController.Instance.gameObject);
        LosingUI.SetActive(true);
    }

    private IEnumerator WinGame() {

        yield return new WaitForSeconds(0.01f); //the duration of the win animation
        WinningUI.SetActive(true);
    }
}
