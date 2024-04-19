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
    public GameObject PlayerLider;
    public GameObject Aqua;
    public GameObject Capitan;
    public GameObject Golden;
    public GameObject Golden2;
    public GameObject caniche;
    public GameObject caniche2;
    public GameObject Vientos;
    public GameObject Pome;
    public GameObject Chusky;
    public GameObject Salchicha;
    public List<GameObject> cards2 = new List<GameObject>();

    void Start()
    {

        cards2.Add(Rastafari);
        cards2.Add(Aguacero);
        cards2.Add(Poodle);
        cards2.Add(Poodle2);
        cards2.Add(Poodle3);
        cards2.Add(Vito);
        cards2.Add(Cocky);
        cards2.Add(Sequia);
        cards2.Add(Robin);
        cards2.Add(Golden);
        cards2.Add(Golden2);
        cards2.Add(caniche);
        cards2.Add(caniche2);
        cards2.Add(Aqua);
        cards2.Add(Capitan);
        cards2.Add(Pome);
        cards2.Add(Vientos);
        cards2.Add(Chusky);
        cards2.Add(Salchicha);



        Hand2();
        GameObject lider2 = Instantiate(Rey, new Vector3(0, 0, 0), Quaternion.identity);
        lider2.transform.SetParent(PlayerLider.transform, false);
    }

    void OnClick2()
    {

        if (cards2.Count > 0)
        {
            int randomIndex2 = Random.Range(0, cards2.Count);
            GameObject enemyCard = Instantiate(cards2[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(EnemyArea.transform, false);
            cards2.RemoveAt(randomIndex2);
        }
        else
        {
            Debug.Log("No hay m�s cartas en el mazo.");
        }



    }




    public void Hand2()
    {
        for (var i = 0; i < 10; i++)
        {
            int randomIndex2 = Random.Range(0, cards2.Count);
            GameObject enemyCard1 = Instantiate(cards2[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard1.transform.SetParent(EnemyArea.transform, false);
            cards2.RemoveAt(randomIndex2);
            enemyCard1.SetActive(false);




        }
    }
    public void Hand3()
    {
        for (var i = 0; i < 2; i++)
        {
            int randomIndex2 = Random.Range(0, cards2.Count);
            GameObject enemyCard1 = Instantiate(cards2[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard1.transform.SetParent(EnemyArea.transform, false);
            cards2.RemoveAt(randomIndex2);
            enemyCard1.SetActive(true);




        }
    }
    public void Hand4()
    {
        for (var i = 0; i < 2; i++)
        {
            int randomIndex2 = Random.Range(0, cards2.Count);
            GameObject enemyCard1 = Instantiate(cards2[randomIndex2], new Vector3(0, 0, 0), Quaternion.identity);
            enemyCard1.transform.SetParent(EnemyArea.transform, false);
            cards2.RemoveAt(randomIndex2);
            enemyCard1.SetActive(false);




        }
    }
}

