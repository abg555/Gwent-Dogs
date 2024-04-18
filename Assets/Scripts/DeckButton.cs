using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

public class DeckButton : MonoBehaviour
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
    public GameObject Aqua;
    public GameObject Capitan;
    public GameObject Golden;
    public GameObject Golden2;
    public GameObject Vientos;
    public GameObject Pome;
    public GameObject Chusky;
    public GameObject PlayerArea;
    public GameObject PlayerLider;
    public GameObject specificDropZone;




    public List<GameObject> cards = new List<GameObject>();

    void Start()
    {

        cards.Add(Rastafari);
        cards.Add(Aguacero);
        cards.Add(Poodle);
        cards.Add(Poodle2);
        cards.Add(Poodle3);
        cards.Add(Vito);
        cards.Add(Cocky);
        cards.Add(Sequia);
        cards.Add(Robin);
        cards.Add(Golden);
        cards.Add(Golden2);
        cards.Add(Aqua);
        cards.Add(Capitan);
        cards.Add(Pome);
        cards.Add(Vientos);
        cards.Add(Chusky);

        GameObject lider = Instantiate(Rey, new Vector3(0, 0, 0), Quaternion.identity);
        lider.transform.SetParent(PlayerLider.transform, false);

        Hand();

    }
    void Update()
    {

    }
    void OnClick()
    {

        if (cards.Count > 0)
        {
            int randomIndex = Random.Range(0, cards.Count);
            GameObject playerCard = Instantiate(cards[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);
            cards.RemoveAt(randomIndex);
        }
        else
        {
            Debug.Log("No hay m�s cartas en el mazo.");
        }




    }

    public void Hand()
    {
        for (var i = 0; i < 10; i++)
        {
            int randomIndex = Random.Range(0, cards.Count);
            GameObject playerCard1 = Instantiate(cards[randomIndex], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard1.transform.SetParent(PlayerArea.transform, false);
            cards.RemoveAt(randomIndex);
            playerCard1.SetActive(true);
            Debug.Log("se instanciaron");







        }

    }



}



