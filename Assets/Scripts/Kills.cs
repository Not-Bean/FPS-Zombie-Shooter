using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Kills : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killText;
    int kills;

    public void AddKill()
    {
        kills++;
    }

    private void FixedUpdate()
    {
        killText.text = kills.ToString();
    }
}
