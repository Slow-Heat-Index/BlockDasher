using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class EnemySoundManager : MonoBehaviour {
    [SerializeField] private AudioClip[] sounds;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(int i) {
        _audioSource.pitch = 1;
        _audioSource.clip = sounds[i];
        _audioSource.PlayOneShot(sounds[i]);
    }
    
    public void PlayRandomPitch(int i) {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.clip = sounds[i];
        _audioSource.PlayOneShot(sounds[i]);
    }
}
