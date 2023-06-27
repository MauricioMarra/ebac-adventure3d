using Ebac.Core.Singleton;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : Singleton<PostProcessManager>
{
    [SerializeField] Vignette _vignette;
    [SerializeField] PostProcessVolume _postProcessVolume;
    [SerializeField] float _flashDuration;

    private float _timer;
    private bool _flash = false;

    public override void Awake()
    {
        base.Awake();
        _postProcessVolume.profile.TryGetSettings<Vignette>(out _vignette);
    }

    private void Start()
    {
        _timer = _flashDuration;
    }

    private void Update()
    {
        if (_timer > 0f && _flash)
        {
            OnFlashVignette();
            _timer -= Time.deltaTime;
        }

        if (_timer < 0f)
        {
            _timer = _flashDuration;
            _flash = false;
        }
    }

    private void OnFlashVignette()
    {
        var res = Mathf.Lerp(0f, 0.6f, _timer/_flashDuration);
        _vignette.intensity.value = res;
    }

    [NaughtyAttributes.Button]
    public void FlashVignette()
    {
        _flash = true;
    }
}

