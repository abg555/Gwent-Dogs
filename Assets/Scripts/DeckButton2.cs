using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckButton2 : MonoBehaviour
{
    public GameObject Rey;
    public GameObject Aguacero;
    public GameObject Sequia;
    public GameObject Robin;
    public GameObject Poodle;
    public GameObject Poodle2;
    public GameObject Poodle3;
    public GameObject Vito;
    public GameObject Rastafari;
    public GameObject Cocky;
    public GameObject EnemyArea;

    List<GameObject> cards = new List<GameObject>();

    void Start()
    {
        cards.Add(Rey);
        cards.Add(Rastafari);
        cards.Add(Aguacero);
        cards.Add(Poodle);
        cards.Add(Poodle2);
        cards.Add(Poodle3);
        cards.Add(Vito);
        cards.Add(Cocky);
        cards.Add(Sequia);
        cards.Add(Robin);

        for (var i = 0; i< 10; i++)
        {
            int randomIndex2 = Random.Range(0, cards.Count);
            GameObject enemyCard1 = Instantiate(cards[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard1.transform.SetParent(EnemyArea.transform, false);
            cards.RemoveAt(randomIndex2);


        }
}

    public void OnClick2()
    {

        if (cards.Count > 0)
        {
            int randomIndex2 = Random.Range(0, cards.Count);
            GameObject enemyCard = Instantiate(cards[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(EnemyArea.transform, false);
            cards.RemoveAt(randomIndex2);
        }
        else
        {
            Debug.Log("No hay más cartas en el mazo.");
        }



    }
    void Update()
    {
        
    }
}
