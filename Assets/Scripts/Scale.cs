using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxScale;
    private Vector3 _initialScale;
    private bool _isGrowing;

    void Start()
    {
        _initialScale = transform.localScale;
        _isGrowing = true;
    }

    void Update()
    {
        if (_maxScale < transform.localScale.x || transform.localScale.x < _initialScale.x)
        {
            _isGrowing = !_isGrowing;
        }

        if (_isGrowing)
        {
            transform.localScale += new Vector3(_speed * Time.deltaTime, _speed * Time.deltaTime, _speed * Time.deltaTime);
        }
        else
        {
            transform.localScale -= new Vector3(_speed * Time.deltaTime, _speed * Time.deltaTime, _speed * Time.deltaTime);
        }
    }
}
