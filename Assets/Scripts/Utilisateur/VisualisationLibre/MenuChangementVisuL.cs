using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script gérant l'interface dans la sceneP
// Pourvoir mettre le jeu en arret et reprendre, quitter, etc...
// Et gérer l'apparition des UI.

public class MenuChangementVisuL : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject resumeMenuUI;

    private void Start()
    {
        PlayerPrefs.SetInt("VisualisationLibre",0);
        // On met le jeu en route
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        // Pour mettre le jeu en pause ou en route avec la touche escape de l'ordinateur.
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
        // On active ou désactive les canvas voulus
        pauseMenuUI.SetActive(false);
        resumeMenuUI.SetActive(true);
        // On met en route le temps pour la physique
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        // On active le curseur
        Cursor.visible = true;
        // On active ou désactive les canvas voulus
        pauseMenuUI.SetActive(true);
        resumeMenuUI.SetActive(false);
        // On désactive le temps pour la physique
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RetryBut()
    {
        // Pour relancer la scène de visualisation Libre depuis le début
        PlayerPrefs.SetInt("VisualisationLibre", 0);
        SceneManager.LoadScene("VisualisationLibre");
    }

    public void ResumeBut()
    {
        // Pour remettre la scène en jeu
        Resume();
    }

    public void QuitBut()
    {
        // Pour quitter l'application
        Application.Quit();
    }

    public void RetourBut()
    {
        // Pour revenir vers la scène d'enregistrement
        SceneManager.LoadScene("Enregistrement");
    }

    public void MenuBut()
    {
        // Pour lancer la scène Menu
        SceneManager.LoadScene("Menu");
    }

    public void PauseBut()
    {
        // Pour mettre la scène en Pause
        Pause();
    }
}