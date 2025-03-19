using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    public Image crosshair;  // Assign your crosshair UI Image in the Inspector
    public Color defaultColor = Color.white;
    public Color enemyColor = Color.red;
    public float rayDistance = 100f;
    
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        crosshair.color = defaultColor;
    }

    void Update()
    {
        CheckForEnemy();
    }

    void CheckForEnemy()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Zombie"))  
            {
                crosshair.color = enemyColor;
            }
            else
            {
                crosshair.color = defaultColor;
            }
        }
        else
        {
            crosshair.color = defaultColor;
        }
    }
}

