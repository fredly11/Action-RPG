using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] AudioClip hit1;
    [SerializeField] AudioClip hit2;
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Start() {
        audioSource = transform.GetComponentsInChildren<AudioSource>()[0];
    }

    void Hit(){
        Debug.Log(("Hit!"));
        float random = UnityEngine.Random.value;
        if(random < .5){
            audioSource.clip = hit1;
        } else
        {
            audioSource.clip = hit2;    
        }
        audioSource.Play();
    }
}
