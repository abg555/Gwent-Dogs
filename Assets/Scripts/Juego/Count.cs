using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Count : MonoBehaviour
{

    public TMP_Text scoreText; /*referencia a un objeto texto de unity*/
    public TurnManager turnManager;     /*referencia un objeto TurnManager*/
    public int TotalPlayerPower { get; private set; } = 0;  /*propiedad publica que empieza en 0*/

    public void Update()
    {

        TotalPlayerPower = 0; /*reinicia su valor a 0*/
        foreach (var zonePower in turnManager.zonePowers.Values)   /*itera sobre cada valor de la coleccion zonePowers.Values*/
        {
            TotalPlayerPower += zonePower; /*suma el valor actual de zonePower*/
        }
        UpdateText(); /*actualiza el texto*/
    }
    public void ResetPower()
    {
        TotalPlayerPower = 0; /**/
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text = "Total de Puntos: " + TotalPlayerPower; /*actua;iza el texto para mostrar el total de puntos del jugador*/

    }
}
