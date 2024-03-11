using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> deck = new List<GameObject>();

    //deck.Add(AssetDatabase.LoadAssetAtPath<GameObject>(""));

    void Awake ()
    {
        for (int i = 0; i < 10; i++)
            deck.Add(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Data/CardsGameObject/Rey.prefab"));
            
            Debug.Log($"la cantidqad de cartas en el deck es de: {deck.Count}");
    }
    
}
