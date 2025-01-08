using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] GameObject iconPrefab;

    bool inventoryEnabled;
    GameObject[,] inventoryIcons = new GameObject[9, 4];
    int[,] inventoryItems = new int[9,4];
    int[,] inventoryAmount = new int[9, 4];


    private void Start()
    {
        for (int y = 0; y < inventoryIcons.GetLength(1); y++)
        {
            for (int x = 0; x < inventoryIcons.GetLength(0); x++)
            {
                inventoryIcons[x,y] = Instantiate(iconPrefab, transform);
                inventoryIcons[x,y].transform.position = new Vector3(x * 110 + 520, y * 110 + 375, 0);
            }
        }
    }

    public void OnClick()
    {
        if (inventoryEnabled)
        {

        }
    }

}
