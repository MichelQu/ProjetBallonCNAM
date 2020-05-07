using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
