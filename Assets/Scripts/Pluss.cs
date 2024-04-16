using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluss : MonoBehaviour
{

    
    public GameObject arquero;
        public int points = 0; 
        public Vector2Int position; 

        
        public void AddPointsToOtherCards(List<Pluss> allCards)
        {
            foreach (Pluss otherCard in allCards)
            {
               
                {
                    
                        otherCard.points += 1;
                        Debug.Log($"Sumando 2 puntos a la carta en posición {otherCard.position}");
                    
                }
            }
        }
    
}


