using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionDevBut : MonoBehaviour
{
    // On instancie la varible (zone de texte)
    public Text zoneText;

    // Start is called before the first frame update
    void Start()
    {
        // On récupère le numéro du dossier choisi et on l'affiche
        int num = PlayerPrefs.GetInt("NumDos");
        zoneText.text = "Dossier n°" + num + " choisi";
    }

    public void But1()
    {
        // On va vers la scène d'enregistrement
        SceneManager.LoadScene("EnregistrementDev");
    }

    public void But2()
    {
        // On va vers la scène de visualisation 
        SceneManager.LoadScene("VisualisationDev");
    }

    public void But3()
    {
        // On va vers la scène de visualisation libre
        SceneManager.LoadScene("VisualisationLibreDev");
    }

    public void But4()
    {
        // On va vers la scène de sélection 
        SceneManager.LoadScene("Selection");
    }

    public void But5()
    {
        // On va vers la scène de menu 
        SceneManager.LoadScene("Menu");
    }

    public void But6()
    {
        // Bouton pour quitter l'application
        Application.Quit();
    }
}
