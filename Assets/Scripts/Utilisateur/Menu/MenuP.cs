using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// Création de l'interface du menu Principale du jeu

public class MenuP : MonoBehaviour
{
    public void PlayBut()
    {
        // Pour aller vers la scène pour choisir les niveaux
        SceneManager.LoadScene("Niveaux");
    }

    public void QuitBut()
    {
        // Pour quitter l'application
        Application.Quit();
    }

    public void ResetBut()
    {
        // On reset le numéro de sauvegarde
        PlayerPrefs.SetInt("NuméroSave",0);
    }

    public void DvpBut()
    {
        // On va vers la scène de sélection
        SceneManager.LoadScene("Selection");
    }
}
