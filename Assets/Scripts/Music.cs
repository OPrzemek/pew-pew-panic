using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance;
    public List<AudioClip> clips;
    public AudioSource source;
    public int chosen;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void Initialize()
    {
        chosen = Random.Range(0, clips.Count);
        source.clip = clips[chosen];
        source.Play();
        StartCoroutine(StartPlaying());
    }

    public IEnumerator StartPlaying()
    {
        if(!source.isPlaying)
        {
            chosen = ++chosen % clips.Count;
            source.clip = clips[chosen];
            source.Play();
        }
        yield return new WaitForEndOfFrame();
    }
}
