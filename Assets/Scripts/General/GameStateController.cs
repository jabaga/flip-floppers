using System.Collections;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using AudioHelper;

public class GameStateController : MonoBehaviour {
    public static GameStateController Instance;

    public bool isGameOver = false;
    public BodyPartsSpawner bodyPartsSpawner;

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
            bodyPartsSpawner.SpawnParts();
            bodyPartsSpawner.gameObject.transform.SetParent(null);
            if (hasWon) StartCoroutine(WinGame());
            else StartCoroutine(LoseGame());
        }
    }

    private IEnumerator LoseGame() {
        Instantiate(ParticleDieEffect, PlayerController.Instance.gameObject.transform.position, Quaternion.Euler(0,0,0));

        AudioManager.instance.RandomizeMiscSfx(DieSound);
        AudioManager.instance.StopMainSound();
        Destroy(PlayerController.Instance.gameObject);
        yield return new WaitForSeconds(2f);
        LosingUI.SetActive(true);
        AudioManager.instance.RandomizeMiscSfx(FailSound);
    }

    private IEnumerator WinGame() {

        yield return new WaitForSeconds(0.01f); //the duration of the win animation
        WinningUI.SetActive(true);
    }
}
