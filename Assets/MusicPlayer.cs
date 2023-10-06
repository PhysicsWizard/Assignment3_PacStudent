using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip play;
    [SerializeField] private AudioSource audio;
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
        audio.clip = start;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = play;
        audio.loop = true;
        audio.Play();
    }
}
