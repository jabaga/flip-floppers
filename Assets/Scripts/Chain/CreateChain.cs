using System.Collections.Generic;
using UnityEngine;

public class CreateChain : MonoBehaviour {
    
    [SerializeField] private float segmentSize = 0.2f;
    [SerializeField] private GameObject chainPartPrefab;
    [SerializeField] private GameObject chainContainerPrefab;
    [SerializeField] private Transform waypointContainerObject;

    [ContextMenu("MakeChain")]
    private void CreateTheChain() {
        GameObject par = Instantiate(chainContainerPrefab, Vector3.zero, Quaternion.identity);

        for (int i = 0; i < waypointContainerObject.transform.childCount; i++) {
            int j = (i != waypointContainerObject.transform.childCount - 1) ? i + 1 : 0;
            Vector3 childPos1 = waypointContainerObject.transform.GetChild(i).position;
            Vector3 childPos2 = waypointContainerObject.transform.GetChild(j).position;
            float dist, k, rest;
            int totcnt, cnt;
            
            dist = k = (childPos1 - childPos2).magnitude; //dist between the two positions

            rest = ((dist + 0.1f) % (segmentSize * 2)); //get empty leftover space
            totcnt = (int) ((dist + 0.1f) / (segmentSize * 2)); //get number of segments

            float offset = (rest + segmentSize * 2) / (float)totcnt;

            k -= offset;
            cnt = 0;

            while (cnt < totcnt - 1) {

                GameObject go = Instantiate(chainPartPrefab, Vector3.Lerp(childPos1, childPos2, 1 - k / dist), Quaternion.identity, par.transform);
                go.transform.localScale = new Vector3(segmentSize, segmentSize, 1f);

                Vector3 line = childPos2 - childPos1;
                Vector3 dir = new Vector3(-line.y, line.x, 0f);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                go.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                par.GetComponent<ChainMover>().AddToList(go.transform);
                k -= (segmentSize * 2 + (rest + segmentSize * 2) / (float)totcnt);
                cnt++;
            }
        }
    }

    void OnDrawGizmosSelected() {
        for (int i = 0; i < waypointContainerObject.transform.childCount; i++) {
            int j = (i != waypointContainerObject.transform.childCount - 1) ? i + 1 : 0;
            Gizmos.color = Color.blue;
            Vector3 childPos1 = waypointContainerObject.transform.GetChild(i).position;
            Vector3 childPos2 = waypointContainerObject.transform.GetChild(j).position;
            Vector3 line = childPos2 - childPos1;
            Gizmos.DrawLine(childPos1, childPos2);
            Gizmos.color = Color.red;
            Gizmos.DrawLine((childPos1 + childPos2) / 2, (childPos1 + childPos2) / 2 + new Vector3(-line.y, line.x, 0f));
        }
    }
}
