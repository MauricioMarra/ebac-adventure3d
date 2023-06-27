using Cinemachine;
using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    [SerializeField] float _amplitude;
    [SerializeField] float _frequency;
    [SerializeField] float _duration;

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _noise;

    private bool _isShaking;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _timer = _duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isShaking)
            OnShakeCamera(_amplitude, _frequency);
        else
            OnShakeCamera(0, 0);

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _isShaking = false;
            _timer = _duration;
        }
    }

    public void OnShakeCamera(float amplitude, float frequency)
    {
        _noise.m_AmplitudeGain = amplitude;
        _noise.m_FrequencyGain = frequency;
    }

    [NaughtyAttributes.Button]
    public void ShakeCamera()
    {
        _isShaking = true;
    }
}
