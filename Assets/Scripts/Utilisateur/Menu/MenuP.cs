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
        // Debug.Log("Play");
    }

    public void QuitBut()
    {
        Application.Quit();
    }

    public void ResetBut()
    {
        // On reset le numéro de sauvegarde
        PlayerPrefs.SetInt("NuméroSave",0);
        // Debug.Log("Ça reset !");
    }

    public void DvpBut()
    {
        SceneManager.LoadScene("VisualisationLibre");
    }
}
