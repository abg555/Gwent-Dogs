using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WinPlayer : MonoBehaviour
{

    public TMP_Text playerText;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerText.text = "" + gameManager.winPlayer;
    }
}
