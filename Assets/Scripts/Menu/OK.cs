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
    public Menu menu;



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
            foreach (Card card in program.card)
            {
                GameObject lider = Instantiate(cardss, new Vector3(0, 0, 0), Quaternion.identity);
                lider.transform.SetParent(cesped.transform, false);
                Cardview cardview = lider.GetComponent<Cardview>();
                cardview.cardview.cardPower = (int)card.Power.power.Evaluate();
                cardview.cardview.cardName = card.Name.name.Evaluate().ToString();
                cardview.cardview.cardHability = card.OnActivation.Elements[0].oae.name;
                string cardType = card.Type.type.Evaluate().ToString();
                string cardFaction = card.Faction.faction.Evaluate().ToString();
                var rangeValues = new List<string>();
                for (int i = 0; i < card.Range.expressionsRange.Length; i++)
                {
                    if (card.Range.expressionsRange[i] != null)
                    {
                        rangeValues.Add(card.Range.expressionsRange[i].Evaluate().ToString());
                    }
                }
                string cardRange = string.Join(", ", rangeValues);
                TextMeshProUGUI[] allTexts = lider.GetComponentsInChildren<TextMeshProUGUI>();
                if (allTexts.Length >= 3)
                {
                    allTexts[1].text = cardType;
                    allTexts[2].text = cardFaction;
                    allTexts[3].text = cardRange;

                }

            }
            menu.scroll.gameObject.SetActive(false);
            menu.isScroll = false;
        }
        menu.scroll.gameObject.SetActive(false);
        menu.isScroll = false;
    }

}
