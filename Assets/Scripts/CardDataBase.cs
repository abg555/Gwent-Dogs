using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
public static List<Cards> list = new List<Cards>();


void Awake(){
    list.Add(new Cards(0, "Rey Chihuahua Loco", 0, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen7"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(1, "Aguacero Interminable", 0, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen8"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(2, "Temporada de sequia", 0, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen9"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(3, "Vientos del Este", 0, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen10"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(4, "AquaDogO", 0, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen11"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(5, "Capitan Rottweiler", 0, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen12"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(6, "Premios Golden Retriever", 0, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen13"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(7, "Pomerania x Pomerania", 0, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen14"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(8, "Caniche Maliche", 2, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen15"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(9, "Robin Pug", 4, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen16"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(10, "Perro Salchicha Gordo Bachicha", 0, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen17"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(11, "Rastafari", 8, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen18"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(12, "Vito Corgione", 10, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen19"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(13, "Poodle Coquette", 5, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen20"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(14, "CHusky el perro diabolico", 7, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen21"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(15, "Chau Chau pescau", 6, "description", Resources.Load<Sprite>(""),  Resources.Load<Sprite>("Imaegen22"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(16, "Los quince de Cocky", 15, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen23"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(17, "El Duke de Mar", 11, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen24"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
    list.Add(new Cards(18, "Sabueso al cubo", 12, "description", Resources.Load<Sprite>(""), Resources.Load<Sprite>("Imaegen25"), Resources.Load<Sprite>("h"), Resources.Load<Sprite>(""), Resources.Load<Sprite>("")));
}




}
