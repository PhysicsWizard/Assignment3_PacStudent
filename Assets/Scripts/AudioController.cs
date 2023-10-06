using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip mainClip;
    void Start()
    {
        StartCoroutine(playMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playMusic()
    {
        source.clip = startClip;
        source.Play();
        yield return new WaitForSeconds(startClip.length);
        source.clip = mainClip;
        source.loop = true;
        source.Play();
    }
}
