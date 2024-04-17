using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinEnemy : MonoBehaviour
{
    public TMP_Text enemyText;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyText.text = "" + gameManager.winEnemy;
    }
}
