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
    public bool isPaused = false;

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
        Cursor.visible = true;
        pauseMenuUI.SetActive(false);
        resumeMenuUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        resumeMenuUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void RetryBut()
    {
        SceneManager.LoadScene(4);
        // Debug.Log("Retry");
    }

    public void MenuBut()
    {
        SceneManager.LoadScene("Menu");
        // Debug.Log("Chargement menu");
    }

    public void RetourBut()
    {
        SceneManager.LoadScene("Transition");
    }

    public void VisualisationBut()
    {
        SceneManager.LoadScene("Visualisation");
    }

    public void VisualisationBut2()
    {
        SceneManager.LoadScene("VisualisationLibre");
    }

    public void PauseBut()
    {
        Pause();
    }
}