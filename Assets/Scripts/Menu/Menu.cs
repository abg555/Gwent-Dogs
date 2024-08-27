using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    public GameObject scroll;
    public GameObject board;
    public bool isScroll = false;
    void Start()
    {
        scroll.gameObject.SetActive(false);
    }

    public void Jugar()
    {
        SceneManager.UnloadSceneAsync("Error");
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

        foreach (AudioListener listener in listeners)
        {
            listener.enabled = true;


        }

    }


    public void Salir()
    {
        Application.Quit();
    }


    public void Crear()
    {
        SceneManager.LoadScene("Juego");
    }
    public void Menus()
    {
        SceneManager.LoadScene("Inicio");
    }
    public void ScrollView()
    {
        if (!isScroll)
        {
            // GameObject scrollView = Instantiate(scroll, new Vector3(0, 0, 0), Quaternion.identity);
            // scrollView.transform.SetParent(board.transform, false);
            isScroll = true;
            scroll.gameObject.SetActive(true);
        }

    }
}
