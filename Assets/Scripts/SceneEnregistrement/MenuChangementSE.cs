using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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

    public void SaveBut()
    {
        PlayerPrefs.SetInt("NuméroSave", PlayerPrefs.GetInt("NuméroSave") + 1);
        string path1 = Application.dataPath + "/Texte/profond/DataBrut" + PlayerPrefs.GetInt("NuméroSave") + ".txt";
        string readText = File.ReadAllText(Application.dataPath + "/Texte/dataBrut.txt");
        File.WriteAllText(path1, readText);

        string path2 = Application.dataPath + "/Texte/profond/DataBallonCreation" + PlayerPrefs.GetInt("NuméroSave") + ".txt";
        string readText2 = File.ReadAllText(Application.dataPath + "/Texte/dataBallonCreation.txt");
        File.WriteAllText(path2, readText2);

        string path3 = Application.dataPath + "/Texte/profond/DataBallonDestruction" + PlayerPrefs.GetInt("NuméroSave") + ".txt";
        string readText3 = File.ReadAllText(Application.dataPath + "/Texte/dataBallonDestruction.txt");
        File.WriteAllText(path3, readText3);

        Debug.Log("SauvegardeProfonde");
    }
}