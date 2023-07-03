using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUtil : MonoBehaviour
{
    private void Update()
    {
        //this.transform.DORotate(this.transform.up + Vector3.up * 5f, .5f);
        this.transform.Rotate(this.transform.up + Vector3.up * .5f);
    }
}
