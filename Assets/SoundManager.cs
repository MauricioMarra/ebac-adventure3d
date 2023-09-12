using Ebac.Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SfxSetup> sfxSetups;

    public AudioSource _audioSource;
    private MusicSetup _currentMusicSetup;

    // Start is called before the first frame update
    void Start()
    {

    }

    [Serializable]
    public class MusicSetup
    {
        public SoundType soundType;
        public AudioClip audioClip;

    }

    [Serializable]
    public class SfxSetup
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }

    [Serializable]
    public enum SoundType
    {
        None,
        Type01,
        Type02,
        Type03,
        Jump,
        ColletctCoin,
        Shoot
    }

    public MusicSetup GetMusicByType(SoundType type)
    {
        return musicSetups.Where(x => x.soundType == type).FirstOrDefault();
    }

    public void PlayMusicByType(SoundType type)
    {
        _currentMusicSetup = GetMusicByType(type);
        _audioSource.clip = _currentMusicSetup.audioClip;
        _audioSource.Play();
    }

    public SfxSetup GetSfxByType(SoundType type)
    {
        return sfxSetups.Where(x => x.soundType == type).FirstOrDefault();
    }
}
