using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count2 : MonoBehaviour
{


    public TMP_Text scoreText; // Referencia al componente Text
    public turn2 turn2;// Referencia al ZoneManager

    void Update()
    {
        // Actualiza el texto del objeto UI para mostrar el total de puntos
        int totalPlayerPower = 0;
        foreach (var zonePower in turn2.zonePowers2.Values)
        {
            totalPlayerPower += zonePower;
        }
        scoreText.text = "Total de Puntos: " + totalPlayerPower;
    }


}
