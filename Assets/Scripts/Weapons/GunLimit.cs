using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLimit : GunBase
{
    private bool _isRecharging = false;

    private int _currentBullets = 0;
    private int _maxBullets = 5;

    [SerializeField] private float _rechargeDelay = 3f;
    private float _rechargeCounter = 0f;

    protected override IEnumerator ShootCoroutine()
    {
        if (_isRecharging) yield break;

        while (_currentBullets < _maxBullets)
        {
            Shoot();
            _currentBullets++;

            UIManager.instance.UpdateBulletCount((float)_currentBullets / (float)_maxBullets);

            if (_currentBullets < _maxBullets)
                yield return new WaitForSeconds(shootDelay);
        }

        Recharge();
    }

    private void Recharge()
    {
        _isRecharging = true;
        StartCoroutine(StartRecharging());
    }

    private IEnumerator StartRecharging()
    {
        while (_rechargeCounter < _rechargeDelay)
        {
            _rechargeCounter += Time.deltaTime;

            UIManager.instance.UpdateBulletCount(_rechargeDelay, _rechargeCounter);

            yield return new WaitForEndOfFrame();
        }

        _rechargeCounter = 0f;
        _currentBullets = 0;
        _isRecharging = false;
    }
}
