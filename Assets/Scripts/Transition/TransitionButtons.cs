using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Création de l'interface de transition à la fin d'un niveau

public class TransitionButtons : MonoBehaviour
{

    private void Start()
    {
        Cursor.visible = true;
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NiveauButton()
    {
        SceneManager.LoadScene("Niveaux");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("SceneP");
    }

    public void QuitBut()
    {
        Application.Quit();
    }

    public void PlaySaveBut()
    {
        SceneManager.LoadScene("Enregistrement");
    }
}
