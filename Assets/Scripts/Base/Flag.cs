using UnityEngine;

public class Flag : MonoBehaviour
{
    public void PlaceAt(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }
}