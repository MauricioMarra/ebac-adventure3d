using Ebac.Core.Singleton;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SfxSetup> sfxSetups;

    // Start is called before the first frame update
    void Start()
    {
        musicSetups = new List<MusicSetup>();
        sfxSetups = new List<SfxSetup>();
    }

    public class MusicSetup
    {
        public AudioClip audioClip;

    }

    public class SfxSetup
    {
        public AudioClip audioClip;
    }

    public enum SoundType
    {
        Type01,
        Type02,
        Type03,

    }
}
