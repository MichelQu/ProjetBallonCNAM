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
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "Données" + PlayerPrefs.GetInt("NuméroSave");
        Directory.CreateDirectory(path);

        // On réalise une sauvegarde Profonde la première fois sinon on le fait pas
        if (PlayerPrefs.GetInt("AEnregitrerP") == 1) // Cette variable voit si on a déjà sauvegardé les fichiers ou non.
        {
            // On réalise une sauvegarde profonde
            PlayerPrefs.SetInt("NuméroSave", PlayerPrefs.GetInt("NuméroSave") + 1);
            // Orientation de la caméra
            string path1 = path + Path.DirectorySeparatorChar + "DataOrientationCamera.txt";
            string readText = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "dataOrientationCamera.txt");
            File.WriteAllText(path1, readText);
            // Création des ballons
            string path2 = path + Path.DirectorySeparatorChar + "DataBallonCreation.txt";
            string readText2 = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "dataBallonCreation.txt");
            File.WriteAllText(path2, readText2);
            // Destruction des ballons
            string path3 = path + Path.DirectorySeparatorChar + "DataBallonDestruction.txt";
            string readText3 = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "dataBallonDestruction.txt");
            File.WriteAllText(path3, readText3);
            // Data Visualisation Caméra
            string path4 = path + Path.DirectorySeparatorChar + "DataVisualisationCamera.txt";
            string readText4 = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "dataVisualisationCamera.txt");
            File.WriteAllText(path4, readText4);
            // Data Diverses
            string path5 = path + Path.DirectorySeparatorChar + "DataDiverses.txt";
            string readText5 = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "dataDiverses.txt");
            File.WriteAllText(path5, readText5);
            // On modifie la variable pour éviter de sauvegarder plusieurs les fichiers
            PlayerPrefs.SetInt("AEnregitrerP", 0);
        }
    }

    public void MenuButton()
    {
        // Pour lancer la scène Menu
        SceneManager.LoadScene("Menu");
    }

    public void NiveauButton()
    {
        // Pour lancer la scène Niveaux
        SceneManager.LoadScene("Niveaux");
    }

    public void RetryButton()
    {
        // Pour lancer la scène Principale et refaire le niveau choisi
        SceneManager.LoadScene("SceneP");
    }

    public void QuitBut()
    {
        // Pour quitter l'application
        Application.Quit();
    }

    public void PlaySaveBut()
    {
        // Pour lancer la scène d'enregistrement
        SceneManager.LoadScene("Enregistrement");
    }
}
