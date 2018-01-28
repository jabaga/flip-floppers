using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainMover : MonoBehaviour {
    public bool isActive = true;

    [SerializeField, Range(0.1f, 0.4f)] private float chainSpeed = 0.1f;
    [SerializeField] private bool reverse = false;
    [SerializeField, Range(0.01f, 0.5f)] private float chainMoveDelay = 0.01f;
    [Space(10), SerializeField] private List<Transform> segments = new List<Transform>();
    [SerializeField] private List<Vector3> initPosits = new List<Vector3>();

    private int segmentProgress = 0;

    private void Start() {
        if (!(segments.Count > 0)) return;
        if (reverse) chainSpeed = -chainSpeed;
        StartCoroutine(MoveChain());
    }

    public void AddToList(Transform seg) {
        segments.Add(seg);
        initPosits.Add(seg.localPosition);
    }
    
    public void Reverse() {
        chainSpeed = -chainSpeed;
        reverse = !reverse;
    }

    public bool IsReversed() {
        return reverse;
    }


    private void Move(float progress) {
        for (int i = 0; i < segments.Count; i++) {
            int k = (segmentProgress + i) % segments.Count;
            int j = (k != segments.Count - 1) ? k + 1 : 0;
            segments[i].localPosition = Vector3.Lerp(initPosits[k], initPosits[j], progress);


            Vector3 line = initPosits[j] - initPosits[k];
            Vector3 dir = new Vector3(-line.y, line.x, 0f);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            segments[i].rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private IEnumerator MoveChain() {
        float moveProgress = 0;
        while (isActive) {
            yield return new WaitForSeconds(chainMoveDelay);
            moveProgress += chainSpeed;

            if (moveProgress > 1) {
                moveProgress = 0;
                segmentProgress++;
            } else if (moveProgress < 0) {
                moveProgress = 1;
                segmentProgress--;
            }

            if (segmentProgress > segments.Count - 1) segmentProgress = 0;
            else if (segmentProgress < 0) segmentProgress = segments.Count - 1;

            Move(moveProgress);
        }
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }

}
