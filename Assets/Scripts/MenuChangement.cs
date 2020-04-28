using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChangement : MonoBehaviour
{
    public static bool isPaused = false;

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
    }

    void Resume()
    {
        Cursor.visible = false;
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

    public void ResumeBut()
    {
        Resume();
        Debug.Log("Resume");
    }

    public void QuitBut()
    {
        Application.Quit();
        // Debug.Log("Quit");
    }
}