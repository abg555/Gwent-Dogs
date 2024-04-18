// using UnityEngine;

// public class Sum : MonoBehaviour
// {
//     public GameObject arqueroPosition; 
//     public int newCardPower = 10; 

//     void Start()
//     {

//         if (arqueroPosition == null)
//         {
//             Debug.LogError("La posición Arquero no está asignada.");
//             return;
//         }


//         foreach (Transform child in arqueroPosition.transform)
//         {

//             Cards cardComponent = child.GetComponent<Cards>();
//             if (cardComponent != null)
//             {

//                 cardComponent.cardPower = newCardPower;
//                 Debug.Log("Card Power modificado para: " + child.name);
//             }
//         }
//     }
// }