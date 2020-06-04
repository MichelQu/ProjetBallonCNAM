using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

// Création de l'interface de transition à la fin d'un niveau

public class TransitionButtons : MonoBehaviour
{

    private void Start()
    {
        // On rend visible le curseur
        Cursor.visible = true;

        // On crée un nouveau dossier pour stocker les informations :
        string path = Application.dataPath + "/Texte/profond/Données" + PlayerPrefs.GetInt("NuméroSave");
        Directory.CreateDirectory(path);

        // On réalise une sauvegarde Profonde
        PlayerPrefs.SetInt("NuméroSave", PlayerPrefs.GetInt("NuméroSave") + 1);
        // Orientation de la caméra
        string path1 = path + "/DataOrientationCamera.txt";
        string readText = File.ReadAllText(Application.dataPath + "/Texte/dataOrientationCamera.txt");
        File.WriteAllText(path1, readText);
        // Création des ballons
        string path2 = path + "/DataBallonCreation.txt";
        string readText2 = File.ReadAllText(Application.dataPath + "/Texte/dataBallonCreation.txt");
        File.WriteAllText(path2, readText2);
        // Destruction des ballons
        string path3 = path + "/DataBallonDestruction.txt";
        string readText3 = File.ReadAllText(Application.dataPath + "/Texte/dataBallonDestruction.txt");
        File.WriteAllText(path3, readText3);
        // Data Visualisation Caméra
        string path4 = path + "/DataVisualisationCamera.txt";
        string readText4 = File.ReadAllText(Application.dataPath + "/Texte/dataVisualisationCamera.txt");
        File.WriteAllText(path4, readText4);
        // Data Diverses
        string path5 = path + "/DataDiverses.txt";
        string readText5 = File.ReadAllText(Application.dataPath + "/Texte/dataDiverses.txt");
        File.WriteAllText(path5, readText5);

        // Debug.Log("SauvegardeProfonde");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NiveauButton()
    {
        SceneManager.LoadScene("Niveaux");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("SceneP");
    }

    public void QuitBut()
    {
        Application.Quit();
    }

    public void PlaySaveBut()
    {
        SceneManager.LoadScene("Enregistrement");
    }
}
