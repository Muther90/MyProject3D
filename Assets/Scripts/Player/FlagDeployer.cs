using System.Collections.Generic;
using UnityEngine;

public class FlagDeployer : MonoBehaviour
{
    [SerializeField] private FlagCreator _flagCreator;

    private Base _selectedBase;
    private Dictionary<Base, Flag> _baseFlags = new();

    public void SelectBase(Base selectedBase)
    {
        _selectedBase = selectedBase;
    }

    public void ClearSelection()
    {
        _selectedBase = null;
    }

    public void DeployFlagAt(Vector3 worldPosition)
    {
        if (_selectedBase != null)
        {
            if (_selectedBase.CanDeployFlag())
            {
                if (_baseFlags.TryGetValue(_selectedBase, out Flag flag))
                {
                    flag.PlaceAt(worldPosition);
                }
                else
                {
                    flag = _flagCreator.Create();
                    flag.PlaceAt(worldPosition);
                    _baseFlags.Add(_selectedBase, flag);
                }

                flag.gameObject.SetActive(true);
                _selectedBase.OnFlagDeployed(worldPosition);
            }
        }
    }

    public void RemoveFlagForBase(Base baseObject)
    {
        if (_baseFlags.TryGetValue(baseObject, out Flag flag))
        {
            flag.Reset();
            _baseFlags.Remove(baseObject);
        }
    }
}