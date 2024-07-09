using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void Salir()
    {
        Application.Quit();
    }


    public void Crear()
    {
        SceneManager.LoadScene("Crear");
    }
    public void Menus()
    {
        SceneManager.LoadScene("Inicio");
    }
}
