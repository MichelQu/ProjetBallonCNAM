using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// Script gérant l'interface dans la scène Enregistrement
// Pouvoir rejouer l'enregistrement ou l'arreter 
// Et gérer l'apparition des UI.

public class MenuChangementDev : MonoBehaviour
{
    public bool isPaused = false;

    // On récupère les canvas d'affichage
    public GameObject pauseMenuUI;
    public GameObject resumeMenuUI;

    private void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
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
        Cursor.visible = true;
        // On active ou déactive les canvas voulus
        pauseMenuUI.SetActive(false);
        resumeMenuUI.SetActive(true);
        // On active le temps pour la physique
        Time.timeScale = 1f;
        // On change la variable pour les contrôles avec touche
        isPaused = false;
    }

    void Pause()
    {
        // On désactive le curseur
        Cursor.visible = true;
        // On active ou déactive les canvas voulus
        pauseMenuUI.SetActive(true);
        resumeMenuUI.SetActive(false);
        // On active le temps pour la physique
        Time.timeScale = 0f;
        // On change la variable pour les contrôles avec touche
        isPaused = true;
    }

    public void ResumeBut()
    {
        // On met le jeu en cours
        Resume();
    }

    public void RetryBut()
    {
        // On charge la scène d'EnregistrementDev
        SceneManager.LoadScene("EnregistrementDev");
    }

    public void MenuBut()
    {
        // On charge la scène de Menu
        SceneManager.LoadScene("Menu");
    }

    public void RetourBut()
    {
        // On charge la scène de TransitionDev
        SceneManager.LoadScene("TransitionDev");
    }

    public void VisualisationBut()
    {
        // On charge la scène de VisualisationDev
        SceneManager.LoadScene("VisualisationDev");
    }

    public void VisualisationBut2()
    {
        SceneManager.LoadScene("VisualisationLibreDev");
    }

    public void PauseBut()
    {
        // Pour mettre le jeu en pause
        Pause();
    }
}