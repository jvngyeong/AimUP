using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject[] musics;

    void Awake()
    {
        audioSource.Play();
        DontDestroyOnLoad(transform.gameObject);
    }
}