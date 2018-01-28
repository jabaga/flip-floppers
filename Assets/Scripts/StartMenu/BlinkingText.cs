using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public Text Text;

	void Start ()
    {
        InvokeRepeating("BlinkingFunc", 0.7f, 0.7f);
	}

    void BlinkingFunc()
    {
        if (Text.isActiveAndEnabled)
            Text.gameObject.SetActive(false);
        else
            Text.gameObject.SetActive(true);
    }
	

}
