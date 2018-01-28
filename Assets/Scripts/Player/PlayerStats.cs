﻿using UnityEngine;
using AudioHelper;

public class PlayerStats : MonoBehaviour{

    public static PlayerStats Instance;
    public ParticleSystem genderSwitchParticle;

    private PlayerController plCntr;
    
    private Gender playerGender = Gender.Male;

    public void SwitchGender(Gender gender, AudioClip sound) {
        if(gender == playerGender)
            return;

        playerGender = playerGender.SwitchGender();
        int gn = (playerGender == Gender.Female) ? 2 : 1;
        PlayerController.Instance.SetActiveCollider(gn);

        genderSwitchParticle.Play();

        AudioManager.instance.RandomizeMiscSfx(sound);

        GetComponent<Animator>().SetBool("Female", playerGender == Gender.Female);
    }

    public Gender GetGender() {
        return playerGender;
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
}
