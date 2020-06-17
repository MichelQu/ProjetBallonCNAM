using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// Script gérant l'interface dans la scène Enregistrement
// Pouvoir rejouer l'enregistrement ou l'arreter 
// Et gérer l'apparition des UI.

public class MenuChangementSE : MonoBehaviour
{
    // Déclaration des variables
    public bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject resumeMenuUI;

    private void Start()
    {
        // On commence par mettre la scène en route
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        // Pour mettre en pause ou en jeu, la scène en cours avec la touche escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (PlayerPrefs.GetInt("FinEnregistrement") == 1)
        {
            Pause();
            PlayerPrefs.SetInt("FinEnregistrement", 0);
        }
    }

    void Resume()
    {
        // On réactive le curseur
        Cursor.visible = false;
        // On active ou déactive les canvas voulus
        pauseMenuUI.SetActive(false);
        resumeMenuUI.SetActive(true);
        // On active le temps pour la physique
        Time.timeScale = 1f;
        // On change la variable pour les contrôles avec touches
        isPaused = false;
    }

    void Pause()
    {
        // On réactive le curseur
        Cursor.visible = true;
        // On active ou déactive les canvas voulus
        pauseMenuUI.SetActive(true);
        resumeMenuUI.SetActive(false);
        // On active le temps pour la physique
        Time.timeScale = 0f;
        // On change la variable pour les contrôles avec touches
        isPaused = true;
    }


    public void RetryBut()
    {
        // On relance la scène d'enregistrement
        SceneManager.LoadScene("Enregistrement");
    }

    public void MenuBut()
    {
        // On remet dans le menu
        SceneManager.LoadScene("Menu");
    }

    public void RetourBut()
    {
        // Bouton pour retourner dans la scène de transition
        SceneManager.LoadScene("Transition");
    }

    public void VisualisationBut()
    {
        // Bouton pour aller dans la scène de visualisation
        SceneManager.LoadScene("Visualisation");
    }

    public void VisualisationBut2()
    {
        // Bouton pour aller dans la scène de visualisation libre
        SceneManager.LoadScene("VisualisationLibre");
    }

    public void PauseBut()
    {
        // Bouton pour mettre le jeu en pause
        Pause();
    }
}