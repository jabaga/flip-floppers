using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheat : MonoBehaviour
{
    public Transform EndPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.position = EndPosition.position;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

}
