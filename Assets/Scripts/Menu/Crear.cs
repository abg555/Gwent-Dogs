using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Crear : MonoBehaviour
{

    public Button submitButton;
    public TMP_InputField inputField;
    public GameObject cardss;
    public GameObject cesped;


    void Start()
    {
        submitButton.onClick.AddListener(SubmitText);
    }

    void SubmitText()
    {
        if (inputField != null)
        {
            string submittedText = inputField.text;
            ProcessSubmittedText(submittedText);
        }
        else
        {
            Debug.LogError("InputField no está asignado en el Inspector");
        }
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
        // if (ast is Program program)
        // {
        //     foreach (Card card in program.card)
        //     {
        //         GameObject lider = Instantiate(cardss, new Vector3(0, 0, 0), Quaternion.identity);
        //         lider.transform.SetParent(cesped.transform, false);
        //         string cardType = card.Type.type.Evaluate().ToString();
        //         string cardName = card.Name.ToString();
        //         string cardFaction = card.Faction.ToString();
        //         string cardPower = card.Power.ToString();
        //         TextMeshProUGUI[] allTexts = lider.GetComponentsInChildren<TextMeshProUGUI>();
        //         if (allTexts.Length >= 4)
        //         {
        //             allTexts[0].text = cardType;
        //             allTexts[1].text = cardName;
        //             allTexts[2].text = cardFaction;
        //             allTexts[3].text = cardPower;
        //         }

        //     }
        // }
    }



}
