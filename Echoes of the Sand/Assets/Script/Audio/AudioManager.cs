using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region
    public static AudioManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    //[SerializeField] private AudioClip[] audioClips;
    [SerializeField] private SoundEffect[] soundEffects;

    private List<AudioSource> audioSources = new List<AudioSource>();

    private AudioSource audioSource;

    private void Start()
    {
        CreateAudioSourceObject();
    }

    private AudioSource CreateAudioSourceObject()
    {
        //Création d'un nouvel objet vide
        GameObject newAudioSource = new GameObject();
        newAudioSource.name = "AudioSource";        //changement du nom
        newAudioSource.transform.parent = this.transform;   //changement de la hiérarchie

        //ajout du composant audioSource
        AudioSource audio = newAudioSource.AddComponent<AudioSource>();

        //Ajouter au list
        audioSources.Add(audio);

        return audio;
    }

    public void PlayAudio(AudioClip audioClip)
    {
        //audioSource.clip = audioClip;
        //audioSource.Play();

        //Iterate in the loop to find an audio that's not playing
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                return;
            }
        }

        //If there isn't one, create a new one
        AudioSource newSource = CreateAudioSourceObject();
        newSource.clip = audioClip;
        newSource.Play();
    }

    public void PlayAudio(string audioClipName)     //l'utilisation de string et de switch est moins favorable, à changer éventuellement
    {
        Debug.Log("Playing audio: " + audioClipName);
        foreach (SoundEffect sfx in soundEffects)
        {
            if (sfx.soundName == audioClipName)
            {
                PlayAudio(sfx.audioclip);
                return;
            }
        }
    }

    //public void StopAudio(string audioClipName)
    //{
    //    foreach (AudioSource audioSource in audioSources)
    //    {
    //        if (audioSource.isPlaying)
    //        {
    //            audioSource.Stop();
    //            return;
    //        }
    //    }
    //}
}

[Serializable]      //de la library system
public struct SoundEffect
{
    public string soundName;
    public AudioClip audioclip;
}

