using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
