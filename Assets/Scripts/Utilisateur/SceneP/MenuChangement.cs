using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script gérant l'interface dans la sceneP
// Pourvoir mettre le jeu en arret et reprendre, quitter, etc...
// Et gérer l'apparition des UI.

public class MenuChangement : MonoBehaviour
{
    // Déclaration des variables
    public static bool isPaused = false;

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
    }

    void Resume()
    {
        // On désactive le curseur
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
        // On active ou déactive les canvas vouluss
        pauseMenuUI.SetActive(true);
        resumeMenuUI.SetActive(false);
        // On désactive le temps pour la physique
        Time.timeScale = 0f;
        // On change la variable pour les contrôles avec touches
        isPaused = true;
    }

    public void ResumeBut()
    {
        // Bouton pour revenir dans la scène en route
        Resume();
    }

    public void QuitBut()
    {
        // Bouton pour quitter l'application
        Application.Quit();
    }

    public void MenuBut()
    {
        // Bouton pour aller dans la scène de Menu
        SceneManager.LoadScene("Menu");
    }

    public void PauseBut()
    {
        // Bouton pour mettre en pause la scène
        Pause();
    }

    public void EnregistrementBut()
    {
        // Bouton pour aller vers la scène d'enregistrement
        SceneManager.LoadScene("Enregistrement");
    }
}