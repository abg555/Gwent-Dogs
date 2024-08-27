using UnityEngine;
using TMPro;

public class ErrorSceneManager : MonoBehaviour
{
    public TMP_Text errorText;

    void Start()
    {
        string errorMessage = PlayerPrefs.GetString("ErrorMessage", "Error desconocido");
        SetErrorMessage(errorMessage);
    }

    public void SetErrorMessage(string message)
    {
        if (errorText != null)
        {
            errorText.text = message;
        }
    }
}