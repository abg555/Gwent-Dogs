using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<Cardview> handCards = new List<Cardview>();
    public GameObject handArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCard(Cardview card)
    {
        handCards.Add(card);
        card.transform.SetParent(handArea.transform, false);
    }

    public bool ExistCardWithName(string name)
    {
        return handCards.Exists(card => card.cardName == name);
    }
}
