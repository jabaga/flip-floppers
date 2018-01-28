using UnityEngine;

public class BodyPartsSpawner : MonoBehaviour {
    
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.parent = null;
    }
	
	public void SpawnParts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (PlayerStats.Instance.GetGender() == Gender.Male && transform.GetChild(i).gameObject.name.Contains("Woman"))
                continue;

            if (PlayerStats.Instance.GetGender() == Gender.Female && transform.GetChild(i).gameObject.name.Contains("Man"))
                continue;

            transform.GetChild(i).gameObject.SetActive(true);

            Rigidbody2D body = transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
            body.bodyType = RigidbodyType2D.Kinematic;
            body.velocity = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-0.8f, 0.8f));
        }
    }
}
