using UnityEngine;

[RequireComponent(typeof(Alarm))]
public class HouseSecurity : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private Trigger _trigger;

    private void Awake()
    {
        if (_alarm == null)
        {
            throw new System.Exception("Alarm is not assigned!");
        }

        if (_trigger == null)
        {
            throw new System.Exception("Trigger is not assigned!");
        }

        _trigger.SomeoneEntered += TriggerEntered;
        _trigger.SomeoneExited += TriggerExited;
    }

    private void OnDisable()
    {
        _trigger.SomeoneEntered -= TriggerEntered;
        _trigger.SomeoneExited -= TriggerExited;
    }

    private void TriggerEntered(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Thief thief))
        {
            _alarm.Activation();
        }
    }

    private void TriggerExited(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Thief thief))
        {
            _alarm.Deactivation();
        }
    }
}
