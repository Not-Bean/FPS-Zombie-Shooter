using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public float offsetMultiplier = 50f; // Increase effect strength
    public float smoothTime = 0.2f;

    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Vector3 velocity;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition; // Get UI element’s start position
    }

    void Update()
    {
        // Get mouse position relative to screen center (-1 to 1 range)
        Vector2 mouseOffset = new Vector2(
            (Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2),
            (Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2)
        );

        // Apply parallax movement
        Vector2 targetPosition = startPosition + (mouseOffset * offsetMultiplier);
        rectTransform.anchoredPosition = Vector3.SmoothDamp(rectTransform.anchoredPosition, targetPosition, ref velocity, smoothTime);
    }
}
