using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicPlaylist;

    private AudioSource audioSource;
    private int musicIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextMusic();
    }

    void PlayNextMusic()
    {
        audioSource.clip = musicPlaylist[musicIndex];
        audioSource.Play();
        musicIndex++;

        if (musicIndex >= musicPlaylist.Length)
        {
            musicIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayNextMusic();
        }

        if (!audioSource.isPlaying)
        {
            PlayNextMusic();
        }


    }
}
