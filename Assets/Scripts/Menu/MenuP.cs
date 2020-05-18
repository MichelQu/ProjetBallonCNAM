using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Création de l'interface du menu Principale du jeu

public class MenuP : MonoBehaviour
{
    public void PlayBut()
    {
        SceneManager.LoadScene("Niveaux");
        Debug.Log("Play");
    }

    public void QuitBut()
    {
        Application.Quit();
    }
}
