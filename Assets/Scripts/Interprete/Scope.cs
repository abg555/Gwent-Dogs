using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Scope : MonoBehaviour
{
    public Dictionary<string, Cards> cards = new Dictionary<string, Cards>();

    public GameManager gameManager;
    public Dictionary<string, Effect> effects = new Dictionary<string, Effect>();
    public Dictionary<string, object> value = new Dictionary<string, object>();


    public void MostrarError(string error)
    {
        PlayerPrefs.SetString("ErrorMessage", error);
        SceneManager.LoadScene("Error");
        AudioListener[] listeners = UnityEngine.Object.FindObjectsOfType<AudioListener>();

        // Desactiva todos los Audio Listeners excepto el principal
        foreach (AudioListener listener in listeners)
        {
            if (listener.gameObject.CompareTag("MainAudioListener"))
            {
                listener.enabled = true;
            }
            else
            {
                listener.enabled = false;
            }
        }
    }
    public void PushCard(string value, Cards card)
    {
        if (cards.ContainsKey(value))
        {
            MostrarError($"Ya existe una carta con este nombre:{value}");
        }
        cards[value] = card;
    }
    public void PushEffect(string value, Effect effect)
    {
        if (effects.ContainsKey(value))
        {
            MostrarError($"Ya existe una effecto con este nombre:{value}");
        }
        else
        {
            effects[value] = effect;
        }
    }

    public Effect isEffect(string value)
    {
        if (effects.ContainsKey(value))
        {
            return effects[value];
        }
        else
        {
            MostrarError($"No existe un efecto con este nombre");
            throw new Exception("ddkv");
        }
    }

    public Cards GetCard(string key)
    {
        if (value.TryGetValue(key, out var cardObj) && cardObj is Cards card)
        {
            return card;
        }
        Debug.LogWarning($"No se encontró una carta válida para la clave: {key}");
        return null;
    }
}
