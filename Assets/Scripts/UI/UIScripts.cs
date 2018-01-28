using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScripts : MonoBehaviour {

	public void RestartLVL() {
        GameManager.Instance.ReloadScene();
    }
}
