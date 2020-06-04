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
    public Text objectif;
    public Text Timer;
    public float temps;
    private float tempsInt;

    public string[] textArray;

    // Start is called before the first frame update
    void Start()
    {
        // On prend en premier
        SaveManagerDev.si.Clear(false);

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
        if (SceneManager.GetActiveScene().name == "VisualisationLibreDev")
        {
            if (PlayerPrefs.GetInt("VisualisationLibre") == 0)
            {
                temps += Time.deltaTime;
                tempsInt = Mathf.RoundToInt(temps);
                Timer.text = tempsInt + "s";
            }
        }
    }

    int AvoirBallon()
    {
        // string[] textArray;
        // Le chemin associé au placement des datas
        string path = Application.dataPath + "/Texte/profond/Données" + PlayerPrefs.GetInt("NumDos") + "/DataDiverses.txt";
        // On récupère le fichier texte
        string readText = File.ReadAllText(path);
        // On le mets dans un liste de String grâce au séparateur (%) qu'on a mis dans le fichier txt
        textArray = readText.Split(new[] { " " }, System.StringSplitOptions.None);
        // On récupère le numéro voulu
        int numero = int.Parse(textArray[12]);
        return (numero);
    }
}
