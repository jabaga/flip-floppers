﻿using UnityEngine;
using UnityEngine.UI;
using AudioHelper;

public class EndManager : MonoBehaviour
{
    public GameObject EndCanvas;
    public AudioClip EndSound;
    public GameObject Player;

    private void Awake()
    {
        EndCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            EndCanvas.gameObject.SetActive(true);
            AudioManager.instance.RandomizeMiscSfx(EndSound);
            transform.gameObject.SetActive(false);
            Player.SetActive(false);
        }
    }


}
