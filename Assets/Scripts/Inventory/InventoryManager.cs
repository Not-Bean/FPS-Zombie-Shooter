using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] GameObject dropPrefab;
    


    bool inventoryEnabled;
    GameObject[,] inventoryIcons = new GameObject[4, 9];
    int[,] inventoryItems = new int[4, 9];
    int[,] inventoryAmount = new int[4, 9];


    private void Start()
    {
        for (int y = 0; y < inventoryIcons.GetLength(1); y++)
        {
            for (int x = 0; x < inventoryIcons.GetLength(0); x++)
            {
                inventoryIcons[x,y] = Instantiate(dropPrefab, transform);
                inventoryIcons[x,y].transform.position = new Vector3(x * 110 + 520, y * 110 + 110, 0);
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
