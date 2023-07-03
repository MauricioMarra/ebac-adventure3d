using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseUtil : MonoBehaviour
{
    [SerializeField] private float _scaleFactor;
    [SerializeField] private float _duration;
    
    private float _originalScale;

    // Start is called before the first frame update
    void Start()
    {
        _originalScale = transform.localScale.x;
        Pulse();
    }

    private void Pulse()
    {
        transform.DOScale(_scaleFactor, _duration).SetLoops(-1, LoopType.Yoyo);
    }
}
