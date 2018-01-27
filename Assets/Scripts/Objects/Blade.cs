using System.Collections;
using UnityEngine;

public class Blade : MonoBehaviour {

    public bool activeBlade = true;

    [SerializeField] private Vector2 movement = new Vector2(1f,0f);
    [SerializeField, Range(0.01f, 0.5f)] private float speed = 0.01f;
    [SerializeField, Range(0.01f, 0.2f)] private float rigidness = 0.01f;

    private Vector2 initPos;
    private Vector2 endPos;

    private void Start() {
        initPos = transform.position;
        endPos = (Vector2) transform.position + movement;
        StartCoroutine(MoveAround());
    }

    private IEnumerator MoveAround() {
        float moveProgress;
        while (activeBlade) {
            moveProgress = 0;
            while (moveProgress < 1f) {
                if (!activeBlade) break;
                transform.position = Vector2.Lerp(initPos, endPos, moveProgress);
                yield return new WaitForSeconds(rigidness);
                moveProgress += (speed);
            }
            moveProgress = 1;
            while (moveProgress > 0f) {
                if (!activeBlade) break;
                transform.position = Vector2.Lerp(initPos, endPos, moveProgress);
                yield return new WaitForSeconds(rigidness);
                moveProgress -= (speed);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
            rotation.z -= 10;

        transform.rotation = Quaternion.Euler(rotation);
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }
}
