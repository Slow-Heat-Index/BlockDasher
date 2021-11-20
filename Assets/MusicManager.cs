using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {
    [SerializeField] private AudioClip[] songs;
   [SerializeField] AudioSource _audioSource;



    public void Play(int i) {
        _audioSource.clip = songs[i];
        _audioSource.Play();
    }
}
