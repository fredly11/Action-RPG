using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    [SerializeField] AudioClip selectCharacter;
    [SerializeField] AudioClip deselect;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SelectCharacter()
    {
        audioSource.clip = selectCharacter;
        audioSource.Play();
    }

    public void Deselect()
    {
        audioSource.clip = deselect;
        audioSource.Play();
    }

}
