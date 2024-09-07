using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class OK : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject cardss;
    public GameObject cesped;
    public GameObject enemy;
    public TurnButton turnButton;
    public Menu menu;
    public Scope scope;
    public bool isBool = false;
    private bool isBool2 = false;
    public GameObject c;
    public GameObject c2;
    public GameObject ar;
    public GameObject ar2;
    public GameObject as1;
    public GameObject as2;



    void Update()
    {


    }
    public void OnButtonClick()
    {
        string inputText = inputField.text;
        ProcessSubmittedText(inputText);
    }
    public void ProcessSubmittedText(string text)
    {
        Debug.Log("Texto enviado: " + text);
        Scanner a = new Scanner(text);
        List<Token> tokens = a.ScanToken();
        Parser parser = new Parser(tokens);
        Node ast = parser.Parse();
        Semantic semantic = new Semantic();
        semantic.CheckNode(ast);
        if (ast is Program program)
        {
            foreach (Effect effect in program.effects)
            {
                scope.PushEffect(effect.Name.name.Evaluate(scope).ToString(), effect);
            }
            foreach (Card card in program.card)
            {
                int k = 60;
                if (turnButton.isPlayerTurn)
                {
                    GameObject lider = Instantiate(cardss, new Vector3(0, 0, 0), Quaternion.identity);
                    lider.transform.SetParent(cesped.transform, false);
                    Cardview cardview = lider.GetComponent<Cardview>();
                    scope.PushCard(card.Name.name.Evaluate(scope).ToString(), cardview.cardview);
                    cardview.cardview.gameObject = lider;
                    cardview.cardview.cardPower = (int)card.Power.power.Evaluate(scope);
                    cardview.cardview.cardName = card.Name.name.Evaluate(scope).ToString();
                    cardview.cardview.cardHability = card.OnActivation.Elements[0].oae.name;
                    cardview.cardview.onActivation = card.OnActivation;
                    cardview.cardview.Owner = 0;
                    cardview.cardview.cardNumber = k;
                    k++;
                    cardview.cardview.cardKind = 1;
                    string cardType = card.Type.type.Evaluate(scope).ToString();
                    string cardFaction = card.Faction.faction.Evaluate(scope).ToString();
                    cardview.cardview.faction = cardFaction;
                    cardview.cardview.type = cardType;
                    var rangeValues = new List<string>();
                    for (int i = 0; i < card.Range.expressionsRange.Length; i++)
                    {
                        if (card.Range.expressionsRange[i] != null)
                        {
                            rangeValues.Add(card.Range.expressionsRange[i].Evaluate(scope).ToString());
                        }
                    }
                    string cardRange = string.Join(", ", rangeValues);
                    int j = 0;
                    foreach (var range in card.Range.expressionsRange)
                    {
                        cardview.cardview.cardRange[j++] = range.Evaluate(scope) as string;
                    }
                    TextMeshProUGUI[] allTexts = lider.GetComponentsInChildren<TextMeshProUGUI>();
                    if (allTexts.Length >= 3)
                    {
                        allTexts[1].text = cardType;
                        allTexts[2].text = cardFaction;
                        allTexts[3].text = cardRange;

                    }
                    if(cardType == @"""Clima"""){
cardview.cardview.zone = "cl";
                    }
                    else if(cardType == @"""Aumento"""){
                    cardview.cardview.zone = "au";
                    
                    }

                    else if (allTexts[3].text == @"""Melee""")
                    {
                        cardview.cardview.zone = "c";
                    }
                    else if (allTexts[3].text == @"""Ranged""")
                    {
                        cardview.cardview.zone = "ar";
                    }
                    else if (allTexts[3].text == @"""Siege""")
                    {
                        cardview.cardview.zone = "as1";
                    }
                    else if (allTexts[3].text == @"""Melee"", ""Ranged""" || allTexts[3].text == @"""Ranged"", ""Melee""")
                    {
                        cardview.cardview.zone = "c";
                        cardview.cardview.zone2 = "ar";
                    }
                    else if (allTexts[3].text == @"""Melee"", ""Siege""" || allTexts[3].text == @"""Siege"", ""Melee""")
                    {
                        cardview.cardview.zone = "c";
                        cardview.cardview.zone2 = "as1";
                    }
                    else if (allTexts[3].text == @"""Ranged"", ""Siege""" || allTexts[3].text == @"""Siege"", ""Ranged""")
                    {
                        cardview.cardview.zone = "ar";
                        cardview.cardview.zone2 = "as";
                    }
                    else if (allTexts[3].text == @"""Melee"", ""Ranged"", ""Siege""" || allTexts[3].text == @"""Ranged"", ""Melee"", ""Siege""" || allTexts[3].text == @"""Siege"", ""Melee"", ""Ranged""" || allTexts[3].text == @"""Melee"", ""Siege"", ""Ranged""" || allTexts[3].text == @"""Ranged"", ""Siege"", ""Melee""" || allTexts[3].text == @"""Siege"", ""Ranged"", ""Melee""")
                    {
                        cardview.cardview.zone = "c";
                        cardview.cardview.zone2 = "ar";
                        cardview.cardview.zone3 = "as1";
                    }
                    isBool = false;


                }
                else
                {
                    GameObject lider = Instantiate(cardss, new Vector3(0, 0, 0), Quaternion.identity);
                    lider.transform.SetParent(enemy.transform, false);
                    Cardview cardview = lider.GetComponent<Cardview>();
                    cardview.cardview.cardPower = (int)card.Power.power.Evaluate(scope);
                    cardview.cardview.cardName = card.Name.name.Evaluate(scope).ToString();
                    cardview.cardview.cardHability = card.OnActivation.Elements[0].oae.name;
                    cardview.cardview.onActivation = card.OnActivation;
                    cardview.cardview.Owner = 1;
                    cardview.cardview.cardKind = 1;
                    cardview.cardview.cardNumber = k;
                    k++;
                    cardview.cardview.gameObject = lider;
                    string cardType = card.Type.type.Evaluate(scope).ToString();
                    string cardFaction = card.Faction.faction.Evaluate(scope).ToString();
                    cardview.cardview.faction = cardFaction;
                    cardview.cardview.type = cardType;
                    var rangeValues = new List<string>();
                    for (int i = 0; i < card.Range.expressionsRange.Length; i++)
                    {
                        if (card.Range.expressionsRange[i] != null)
                        {
                            rangeValues.Add(card.Range.expressionsRange[i].Evaluate(scope).ToString());
                        }
                    }
                    string cardRange = string.Join(", ", rangeValues);
                    int j = 0;
                    foreach (var range in card.Range.expressionsRange)
                    {
                        cardview.cardview.cardRange[j++] = range.Evaluate(scope) as string;
                    }
                    TextMeshProUGUI[] allTexts = lider.GetComponentsInChildren<TextMeshProUGUI>();
                    if (allTexts.Length >= 3)
                    {
                        allTexts[1].text = cardType;
                        allTexts[2].text = cardFaction;
                        allTexts[3].text = cardRange;

                    }
                    if(cardType == @"""Clima"""){
cardview.cardview.zone = "cl";
                    }
                    else if(cardType == @"""Aumento"""){
                    cardview.cardview.zone = "au2";
                    
                    }

                    else if (allTexts[3].text == @"""Melee""")
                    {
                        cardview.cardview.zone = "c2";
                    }
                    else if (allTexts[3].text == @"""Ranged""")
                    {
                        cardview.cardview.zone = "ar2";
                    }
                    else if (allTexts[3].text == @"""Siege""")
                    {
                        cardview.cardview.zone = "as2";
                    }
                    else if (allTexts[3].text == @"""Melee"",""Ranged""" || allTexts[3].text == @"""Ranged"",""Melee""")
                    {
                        cardview.cardview.zone = "c2";
                        cardview.cardview.zone2 = "ar2";
                    }
                    else if (allTexts[3].text == @"""Melee"",""Siege""" || allTexts[3].text == @"""Siege"",""Melee""")
                    {
                        cardview.cardview.zone = "c2";
                        cardview.cardview.zone2 = "as2";
                    }
                    else if (allTexts[3].text == @"""Ranged"",""Siege""" || allTexts[3].text == @"""Siege"",""Ranged""")
                    {
                        cardview.cardview.zone = "ar2";
                        cardview.cardview.zone2 = "as2";
                    }
                    else if (allTexts[3].text == @"""Melee"",""Ranged"",""Siege""" || allTexts[3].text == @"""Ranged"",""Melee"",""Siege""" || allTexts[3].text == @"""Siege"",""Melee"",""Ranged""" || allTexts[3].text == @"""Melee"",""Siege"",""Ranged""" || allTexts[3].text == @"""Ranged"",""Siege"",""Melee""" || allTexts[3].text == @"""Siege"",""Ranged"",""Melee""")
                    {
                        cardview.cardview.zone = "c2";
                        cardview.cardview.zone2 = "ar2";
                        cardview.cardview.zone3 = "as2";
                    }
                    isBool2 = false;

                }



            }
            menu.scroll.gameObject.SetActive(false);
            menu.isScroll = false;
        }
        menu.scroll.gameObject.SetActive(false);
        menu.isScroll = false;
    }
    public void Prueba(Scope scope)
    {

        foreach (Transform child in c.transform)
        {

            Cardview cardView = child.GetComponent<Cardview>();
            if (!isBool && cardView != null && cardView.cardNumber >= 60)
            {
                cardView.cardNumber = 0;
                Evaluator evaluato = new Evaluator(scope, cardView.cardview);
                evaluato.eEffect();
                Debug.Log($"Efecto de la carta evaluado");
                isBool = true;
                break;
            }

        }
        foreach (Transform child in ar.transform)
        {

            Cardview cardView = child.GetComponent<Cardview>();
            if (!isBool && cardView != null && cardView.cardNumber >= 60)
            {
                cardView.cardNumber = 0;
                Evaluator evaluato = new Evaluator(scope, cardView.cardview);
                evaluato.eEffect();
                Debug.Log($"Efecto de la carta evaluado");
                isBool = true;
                break;
            }

        }
        foreach (Transform child in as1.transform)
        {

            Cardview cardView = child.GetComponent<Cardview>();
            if (!isBool && cardView != null && cardView.cardNumber >= 60)
            {
                cardView.cardNumber = 0;
                Evaluator evaluato = new Evaluator(scope, cardView.cardview);
                evaluato.eEffect();
                Debug.Log($"Efecto de la carta evaluado");
                isBool = true;
                break;
            }

        }
    }
    public void Prueba2(Scope scope)
    {
        foreach (Transform child in c2.transform)
        {

            Cardview cardView = child.GetComponent<Cardview>();
            if (!isBool && cardView != null && cardView.cardNumber >= 60)
            {
                cardView.cardNumber = 0;
                Evaluator evaluato = new Evaluator(scope, cardView.cardview);
                evaluato.eEffect();
                Debug.Log($"Efecto de la carta evaluado");
                isBool2 = true;
                break;
            }

        }
        foreach (Transform child in ar2.transform)
        {

            Cardview cardView = child.GetComponent<Cardview>();
            if (!isBool && cardView != null && cardView.cardNumber >= 60)
            {
                cardView.cardNumber = 0;
                Evaluator evaluato = new Evaluator(scope, cardView.cardview);
                evaluato.eEffect();
                Debug.Log($"Efecto de la carta evaluado");
                isBool2 = true;
                break;
            }

        }
        foreach (Transform child in as2.transform)
        {

            Cardview cardView = child.GetComponent<Cardview>();
            if (!isBool && cardView != null && cardView.cardNumber >= 60)
            {
                cardView.cardNumber = 0;
                Evaluator evaluato = new Evaluator(scope, cardView.cardview);
                evaluato.eEffect();
                Debug.Log($"Efecto de la carta evaluado");
                isBool2 = true;
                break;
            }

        }
    }
}
