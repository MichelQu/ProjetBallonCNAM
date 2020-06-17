using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script gérant l'interface dans la sceneP
// Pourvoir mettre le jeu en arret et reprendre, quitter, etc...
// Et gérer l'apparition des UI.

public class MenuChangementVisu : MonoBehaviour
{
    // Déclaration des variables
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject resumeMenuUI;
    public GameObject heatMapUI;

    Camera cam;

    private void Start()
    {
        // On commence par mettre la scène en jeu
        Resume();
        cam = Camera.main;
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
        // On réactive le curseur
        Cursor.visible = false;
        // On active ou déactive les canvas voulus
        pauseMenuUI.SetActive(false);
        heatMapUI.SetActive(false);
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
        heatMapUI.SetActive(false);
        // On désactive le temps pour la physique
        Time.timeScale = 0f;
        // On change la variable pour les contrôles avec touches
        isPaused = true;
    }

    void HeatMap()
    {
        // On réactive le curseur
        Cursor.visible = true;
        // On active ou déactive les canvas voulus
        pauseMenuUI.SetActive(false);
        resumeMenuUI.SetActive(false);
        heatMapUI.SetActive(true);
        // On désactive le temps pour la physique
        Time.timeScale = 0f;
    }

    public void heatMapBut()
    {
        // Pour afficher les données de HeatMap
        HeatMap();
    }

    public void heatMap2()
    {
        // On crée une HeatMap pour les données d'acuité visuelle
        cam.GetComponent<ExploSpatialeV>().VisualisationAcuite();
        Resume();
    }

    public void heatMap10()
    {
        // On crée une HeatMap pour les données d'acuité visuelle
        cam.GetComponent<ExploSpatialeV>().VisualisationLecture();
        // On revient dans le mode Resume
        Resume();
    }

    public void heatMap20()
    {
        // On crée une HeatMap pour les données d'acuité visuelle
        cam.GetComponent<ExploSpatialeV>().VisualisationSymbole();
        // On revient dans le mode Resume
        Resume();
    }

    public void finHeatMap()
    {
        // On supprime les données de HeatMap s'il y en a
        cam.GetComponent<ExploSpatialeV>().HeatMapFermeture();
        // On revient dans le mode Resume
        Resume();
    }

    public void ResumeBut()
    {
        // Bouton pour revenir dans la scène en cours 
        Resume();
    }

    public void QuitBut()
    {
        // Bouton pour quitter l'application
        Application.Quit();
    }

    public void RetourBut()
    {
        // Bouton pour revenir dans la scène d'enregistrement
        SceneManager.LoadScene("Enregistrement");
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
}