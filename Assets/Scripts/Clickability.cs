using UnityEngine;

public class Clickability : MonoBehaviour
{
    public event System.Action OnObjectClicked;

    private void OnMouseDown()
    {
        OnObjectClicked?.Invoke();
    }
}
