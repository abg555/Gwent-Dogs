using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Count : MonoBehaviour
{

    public TMP_Text scoreText;
    public TurnManager turnManager;
    public int TotalPlayerPower { get; private set; } = 0;

    public void Update()
    {

        TotalPlayerPower = 0;
        foreach (var zonePower in turnManager.zonePowers.Values)
        {
            TotalPlayerPower += zonePower;
        }
        UpdateText();
    }
    public void ResetPower()
    {
        TotalPlayerPower = 0;
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text = "Total de Puntos: " + TotalPlayerPower;

    }
}
