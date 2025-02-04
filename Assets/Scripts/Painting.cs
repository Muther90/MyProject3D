using UnityEngine;

public class Painting : MonoBehaviour
{
    void Start()
    {
        Color randomColor = Random.ColorHSV();
        Renderer objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = randomColor;
    }
}
