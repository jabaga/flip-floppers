using System.Collections;
using UnityEngine;

public class TeleporterPart : MonoBehaviour {

    [SerializeField] private Gender teleporterGender = Gender.Male;
    [SerializeField] private float effectSpeed = 0.02f;
    [Space(10)]
    [SerializeField] private Transform otherPart;
    [SerializeField] private Transform transmissionEffect;
    private ParticleSystem particleTransmission;

    private bool isReceiving;
    private ParticleSystem teleporterPartEffect;

    private void Start() {
        isReceiving = false;
        teleporterPartEffect = GetComponent<ParticleSystem>();
        particleTransmission = transmissionEffect.GetComponent<ParticleSystem>();
        SetColor();
        teleporterPartEffect.Play();
    }

    private void SetColor() {
        var main = teleporterPartEffect.main;
        main.startColor = (teleporterGender == Gender.Male) ? GameSettings.Instance.maleParticleColor : GameSettings.Instance.femaleParticleColor;
    }

    private IEnumerator Transfer() {
        transmissionEffect.position = transform.position;
        particleTransmission.Play();
        float moveProgress = 0;
        otherPart.GetComponent<TeleporterPart>().SetReceiving();

        PlayerController.Instance.OnTeleportationStart();

        while (moveProgress < 1f) {
            Vector3 newPos = Vector3.Lerp(transform.position, otherPart.position, moveProgress);
            transmissionEffect.position = newPos;
            PlayerStats.Instance.transform.position = newPos;
            moveProgress += effectSpeed;
            yield return new WaitForSeconds(effectSpeed / 2);
        }

        PlayerController.Instance.OnTeleportationEnd();
        particleTransmission.Stop();
    }

    public void SetAccess(Gender newGender) {
        teleporterPartEffect.Stop();
        teleporterGender = newGender;
        SetColor();
        teleporterPartEffect.Play();
    }

    public void SetReceiving() {
        isReceiving = true;
    }

    [ContextMenu("Activate")]
    private void Activate() {
        if (true) StartCoroutine(Transfer());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"
            && !isReceiving
            && teleporterGender == PlayerStats.Instance.GetGender()) {
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            isReceiving = false;
        }
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }

    private void OnDisable() {
        StopAllCoroutines();
    }
}
