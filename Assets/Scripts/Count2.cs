using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count2 : MonoBehaviour
{


    public TMP_Text scoreText;
    public turn2 turn2;
    public int TotalPlayerPower2 { get; private set; } = 0;


    public void Update()
    {

        TotalPlayerPower2 = 0;
        foreach (var zonePower in turn2.zonePowers2.Values)
        {
            TotalPlayerPower2 += zonePower;
        }
        scoreText.text = "Total de Puntos: " + TotalPlayerPower2;
    }

}

