using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class DeckButton : MonoBehaviour
{
    public Cardview Rey;
    public Cardview Aguacero;
    public Cardview Sequia;
    public Cardview Robin;

    public PlayerController player;
    List<Cardview> cards = new List<Cardview>();
    void Start()
    {
        cards.Add(Rey);
        cards.Add(Aguacero);
        cards.Add(Sequia);
        cards.Add(Robin);

        CreateCards(10);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        CreateCards(1);
    }

    private void CreateCards(int cardsAmount)
    {
        for (int i = 0; i < cardsAmount; i++)
        {
            Cardview card = cards[Random.Range(0, cards.Count)];
            if (!player.ExistCardWithName(card.cardName))
            {
                Cardview playerCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                player.SetCard(playerCard);
            }

        }
    }
}

