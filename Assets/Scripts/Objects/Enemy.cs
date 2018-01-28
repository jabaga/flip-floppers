using UnityEngine;
using AudioHelper;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour {

    public AudioClip DieSound;
    public AudioClip FailSound;
    public GameObject ParticleDieEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            GameStateController.Instance.GameOver(false);

            Instantiate(ParticleDieEffect, transform.position, transform.rotation);

            AudioManager.instance.RandomizeMiscSfx(DieSound);
            AudioManager.instance.StopMainSound();

            Invoke("FailSoundEffect", 2f);

        }
    }

    void FailSoundEffect()
    {
        AudioManager.instance.RandomizeMiscSfx(FailSound);
    }
}
