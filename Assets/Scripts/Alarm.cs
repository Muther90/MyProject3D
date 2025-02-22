using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, Range(0f, 1f)] private float _audioRateVolume = 0.05f;
    [SerializeField, Range(0f, 1f)] private float _audioMaxVolume = 1f;

    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        if (_audioSource == null)
        {
            throw new System.Exception("Audio Source is not assigned!");
        }

        _audioSource.volume = 0;
    }

    public void Activation()
    {
        if (_volumeCoroutine is not null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        if (_audioSource.time > 0)
        {
            _audioSource.UnPause();
        }
        else
        {
            _audioSource.Play();
        }

        _volumeCoroutine = StartCoroutine(UpdateVolume(_audioMaxVolume));
    }

    public void Deactivation()
    {
        if (_volumeCoroutine is not null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(UpdateVolume(0));
    }

    private IEnumerator UpdateVolume(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _audioRateVolume * Time.deltaTime);

            yield return null;
        }

        if (Mathf.Approximately(_audioSource.volume, 0))
        {
            _audioSource.Stop();
        }
    }
}
