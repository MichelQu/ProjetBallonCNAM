using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

// Ce script gère l'apparition des ballons dans la scène enregistrement

public class Enregistrement : MonoBehaviour
{
    #region Les variables
    // Les gameObjects
    public GameObject ballon1;
    public GameObject ballon2;

    // Les listes des objets 
    private List<Vector3> ListBallonR;
    private List<Vector3> ListBallonD;
    private List<float> ListTempsR;
    private List<float> ListTempsD;

    // Les variables du scripts
    private int nbrR;
    private int nbrD;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("FinEnregistrement");
        PlayerPrefs.SetInt("FinEnregistrement", 0);

        // On initialise les variables pour le tableau
        nbrR = 0;
        nbrD = 0;
        // On initialise les listes des ballons et des temps d'apparition de ces ballons
        ListBallonR = new List<Vector3>();
        ListBallonD = new List<Vector3>();
        ListTempsR = new List<float>();
        ListTempsD = new List<float>();

        // On récupère toutes les infos des coordonnées qu'on a enregistré
        LoadDataBallon();
        // LoadDataCamera();
    }

    // Update is called once per frame
    void Update()
    {
        // On incrémente le temps
        float temps = this.GetComponent<InitialisationSE>().temps;

        // On ajoute les ballons Rouges et Dorés selon le temps
        if (nbrR < ListBallonR.Count)
        {
            if (Mathf.Abs(temps - ListTempsR[nbrR]) < 0.01f )
            {
                Vector3 coord = ListBallonR[nbrR]; // On récupre les coordonnées 
                nbrR += 1; // On incrémente pour la suite
                Instantiate(ballon1, coord, Quaternion.identity); // On instancie le ballon rouge
            }
        }

        if (nbrD < ListBallonD.Count)
        {
            if (Mathf.Abs(temps - ListTempsD[nbrD]) < 0.01f)
            {
                Vector3 coord = ListBallonR[nbrD]; // On récupre les coordonnées 
                nbrD += 1; // On incrémente pour la suite
                Instantiate(ballon2, coord, Quaternion.identity); // On instancie le ballon doré
            }
        }
    }

    void LoadDataBallon()
    {
        // Initialisation des variables
        string[] textArray;
        ListBallonR.Clear();
        ListBallonD.Clear();
        ListTempsR.Clear();
        ListTempsD.Clear();
        // Le chemin associé au placement des datas
        string path = Application.dataPath + "/Texte/dataBallonCreation.txt";

        // On récupère le fichier texte
        string readText = File.ReadAllText(path);
        // On le transforme un peu pour pouvoir l'utiliser plus tard
        readText = readText.Replace("Date de création des ballons dans le jeu, leurs coordonnées et spécificités :", "");
        readText = readText.Replace("(", "");
        readText = readText.Replace(")", "");
        readText = readText.Replace(", ", "%");
        readText = readText.Replace("#", "");
        readText = readText.Replace(System.Environment.NewLine, "");

        // On le mets dans un liste de String grâce au séparateur (%) qu'on a mis dans le fichier txt
        textArray = readText.Split(new[] { "%" }, System.StringSplitOptions.None);

        for (int i=0; i < (textArray.Length/5); i++)
        {
            if (textArray[5*i+1] == "rouge")
            {
                // On ajoute le temps dans la liste des Temps des ballons rouges
                ListTempsR.Add(float.Parse(textArray[5 * i], CultureInfo.InvariantCulture));
                // On ajoute la position dans la liste des ballons rouges
                Vector3 pos = new Vector3(float.Parse(textArray[5 * i + 2], CultureInfo.InvariantCulture), float.Parse(textArray[5 * i + 3], CultureInfo.InvariantCulture), float.Parse(textArray[5 * i + 4], CultureInfo.InvariantCulture));
                ListBallonR.Add(pos);
            }
            if (textArray[5*i+1] == "or")
            {
                // On ajoute le temps dans la liste des Temps des ballons dorés
                ListTempsD.Add(float.Parse(textArray[5 * i], CultureInfo.InvariantCulture));
                // On ajoute la position dans la liste des ballons dorés
                Vector3 pos = new Vector3(float.Parse(textArray[5 * i + 2], CultureInfo.InvariantCulture), float.Parse(textArray[5 * i + 3], CultureInfo.InvariantCulture), float.Parse(textArray[5 * i + 4], CultureInfo.InvariantCulture));
                ListBallonD.Add(pos);
            }
        }
    }
}
