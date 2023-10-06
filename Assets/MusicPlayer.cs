using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip play;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        StartCoroutine(playSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playSound()
    {
        audioSource.clip = start;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = play;
        audioSource.loop = true;
        audioSource.Play();
    }
}
