using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource m_AudioSource;

    private void Awake()
    {
        instance = this;
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        m_AudioSource.PlayOneShot(_sound);
    }
}
