using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Color _flashColor;
    [SerializeField] private float _duration;

    private Tween _currentTween;
    private Color _defaultColor;

    private void Start()
    {
        _defaultColor = _mesh.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void FlashObject()
    {
        if (_mesh != null && !_currentTween.IsActive())
            _currentTween = _mesh.material.DOColor(_flashColor, "_EmissionColor", _duration).SetLoops(4, LoopType.Yoyo);
    }
}
