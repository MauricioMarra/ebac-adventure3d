using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string auxioMixerParameter;


    public void ChangeSliderValue(float value)
    {
        if (audioMixer != null)
        {
            if (value <= -20)
                value = -80;

            audioMixer.SetFloat(auxioMixerParameter, value);
        }
    }
}
