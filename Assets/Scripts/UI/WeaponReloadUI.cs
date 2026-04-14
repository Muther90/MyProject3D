using UnityEngine;

public class WeaponReloadUI : WeaponUI
{
    [SerializeField] private GameObject _reloadIndicator;

    private WeaponReloadInfo _reloadInfo;

    protected override void OnWeaponChanged(Weapon weapon)
    {
        OnUnsubscribe();

        if (weapon != null && weapon.TryGetComponent<WeaponReloadInfo>(out WeaponReloadInfo reloadInfo))
        {
            _reloadInfo = reloadInfo;
            _reloadInfo.ReloadingStateChanged += UpdateReloadState;
            UpdateReloadState(_reloadInfo.IsReloading);
        }
        else
        {
            _reloadIndicator.SetActive(false);
            _reloadInfo = null;
        }
    }

    protected override void OnUnsubscribe()
    {
        if (_reloadInfo != null)
        {
            _reloadInfo.ReloadingStateChanged -= UpdateReloadState;
            _reloadInfo = null;
        }
    }

    private void UpdateReloadState(bool isReloading)
    {
        _reloadIndicator.SetActive(isReloading);
    }
}