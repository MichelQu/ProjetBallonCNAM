using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Globalization;

// Script gérant l'initialisation des UI : objectif, timer 
// Et lançant le chrono de la scène Enregistrement

public class InitialisationDev : MonoBehaviour
{
    // On récupère les variables voulues
    public Text objectif;
    public Text Timer;
    public float temps;
    float tempsInt;
    string path;
    // On instance les listes qui seront utilisés dans la suite
    string[] textArray;

    // Start is called before the first frame update
    void Start()
    {
        // On prend en premier
        SaveManagerDev.si.Clear(false);
        // Le chemin associé au placement des datas
        path = Application.persistentDataPath + Path.DirectorySeparatorChar + "Données" + PlayerPrefs.GetInt("NumDos") + "/DataDiverses.txt";
        // On modifie les variables dès le début
        temps = 0f;
        int nbrBallons = AvoirBallon();
        PlayerPrefs.SetInt("ObjectifsDev", nbrBallons);
        objectif.text = "Objectif : " + nbrBallons + " Ballons";
        Timer.text = temps + "s";
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "EnregistrementDev")
        {
            temps += Time.deltaTime;
            tempsInt = Mathf.RoundToInt(temps);
            Timer.text = tempsInt + "s";
        }
    }

    int AvoirBallon()
    {
        // On récupère le fichier texte
        string readText = File.ReadAllText(path);
        // On le mets dans un liste de String grâce au séparateur (%) qu'on a mis dans le fichier txt
        textArray = readText.Split(new[] { " " }, System.StringSplitOptions.None);
        // On récupère le numéro voulu
        int numero = int.Parse(textArray[12]);
        return (numero);
    }
}
