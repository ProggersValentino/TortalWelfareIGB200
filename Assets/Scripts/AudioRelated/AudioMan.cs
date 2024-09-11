using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// the purpose of this script is to manage each audio source when we want to play an audio clip of our choosing
/// </summary>
public class AudioMan : MonoBehaviour
{
    public List<AudioSO> audioToKeepTrack;

    public List<AudioSource> sourcesOfAudio;
    
    public Dictionary<string, Tuple<AudioMixerGroup, AudioClip>> audioKnowledgeBase =
        new Dictionary<string, Tuple<AudioMixerGroup, AudioClip>>();

    public Dictionary<AudioMixerGroup, AudioSource>
        sourceKnowledgeBase = new Dictionary<AudioMixerGroup, AudioSource>();

    private void OnEnable()
    {
        AudioEventSystem.PlayAudio += PlayAudio;
        AudioEventSystem.StopAudio += StopAudio;
    }

    private void Awake()
    {
        InitKnowledgeBase(audioToKeepTrack);
        InitSourceKnowledgeBase(sourcesOfAudio);
    }

    public void InitKnowledgeBase(List<AudioSO> audioList)
    {
        foreach (AudioSO audio in audioList)
        {
            audioKnowledgeBase[audio._lookupKey] = new Tuple<AudioMixerGroup, AudioClip>(audio._mixerOutput, audio._audio);
        }
        
    }

    public void InitSourceKnowledgeBase(List<AudioSource> sources)
    {
        foreach (AudioSource audioS in sources)
        {
            sourceKnowledgeBase.TryAdd(audioS.outputAudioMixerGroup, audioS);
        }
    }
    
    /// <summary>
    /// find and return the audio mixer group and audio clip we want to play
    /// </summary>
    /// <param name="searchKey"></param>
    /// <returns></returns>
    public Tuple<AudioMixerGroup, AudioClip> ExtractWantedAudio(string searchKey)
    {
        audioKnowledgeBase.TryGetValue(searchKey, out Tuple<AudioMixerGroup, AudioClip> value);
        return value;
    }

    /// <summary>
    /// when we want to extract a particular audio source to play the audio we desire
    /// </summary>
    /// <param name="mixerGroup"></param>
    /// <returns></returns>
    public AudioSource ExtractWantedSource(AudioMixerGroup mixerGroup)
    {
        sourceKnowledgeBase.TryGetValue(mixerGroup, out AudioSource source);
        return source;
    }

    public void PlayAudio(string searchKey)
    {
        Tuple<AudioMixerGroup, AudioClip> selectedAudio = ExtractWantedAudio(searchKey);
        AudioSource selectedSource = ExtractWantedSource(selectedAudio.Item1);
        
        selectedSource.PlayOneShot(selectedAudio.Item2);
    }
    
    public void StopAudio(string searchKey)
    {
        Tuple<AudioMixerGroup, AudioClip> selectedAudio = ExtractWantedAudio(searchKey);
        AudioSource selectedSource = ExtractWantedSource(selectedAudio.Item1);
        
        selectedSource.Stop();
    }
    
}
