using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxPool : Singleton<SfxPool>
{
    private List<AudioSource> _audioSourceList = new List<AudioSource>();

    [SerializeField] private int _poolSize = 10;
    [SerializeField] private AudioMixerGroup _mixer;

    private int _currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < _poolSize; i++)
        {
            CreatePool();
        }
    }

    public void CreatePool()
    {
        var newAudioSource = new GameObject("SfxAudioSource");
        newAudioSource.transform.SetParent(transform);
        newAudioSource.AddComponent<AudioSource>();
        newAudioSource.GetComponent<AudioSource>().outputAudioMixerGroup = _mixer;

        _audioSourceList.Add(newAudioSource.GetComponent<AudioSource>());
    }

    public void Play(SoundManager.SoundType soundType)
    {
        if (soundType == SoundManager.SoundType.None) return;

        _audioSourceList[_currentIndex].GetComponent<AudioSource>().clip = SoundManager.instance.GetSfxByType(soundType).audioClip;
        _audioSourceList[_currentIndex].Play();

        _currentIndex++;
        if (_currentIndex >= _audioSourceList.Count) _currentIndex = 0;
    }
}
