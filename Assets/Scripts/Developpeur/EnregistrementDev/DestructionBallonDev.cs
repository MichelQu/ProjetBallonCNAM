using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

// Ce script gère la destruction des ballons dans la scène Enregistrement grâce aux données enregistrées
// dans le fichier texte : dataBallonDestruction.txt et qui sont récupérées dans ce script

public class DestructionBallonDev : MonoBehaviour
{
    // On charge les variables 
    Camera cam;
    int score = 0;
    int ballons= 0;
    int tampon = 0;
    // On récupère les zones de texte
    public Text textScore;
    public Text textBallon;
    // On instancie les listes
    List<float> ListTemps;
    List<Vector3> ListPos;

    void Start()
    {
        // On instancie les variables
        ListTemps = new List<float>();
        ListPos = new List<Vector3>();
        // Recherche de la caméra
        cam = GetComponent<Camera>();
        // Initialisation des UI
        textScore.text = "Score : " + score;
        textBallon.text = "Ballons détruits : " + ballons;
        // Récupération des informations du fichier texte
        LoadDestructionBallon();
    }

    void Update()
    {
        // On récupère le temps de la scène Enregistrement
        float temps = cam.GetComponent<InitialisationDev>().temps;
        
        // Si on a encore des ballons à détruire (vérifié avec le test dans le if)
        if (tampon < ListTemps.Count)
        {
            // Lorsqu'on est arrivé au temps de déstruction d'un ballon
            if ( Mathf.Abs(temps-ListTemps[tampon]) < 0.01f)
            {
                // On itendifie tous les ballons dans la scène
                GameObject[] listGO = GameObject.FindGameObjectsWithTag("Ballon");
                // On lance la fonction pour en détruire un
                Destruction(listGO);
                // On incrémente la variable tampon pour passer au prochain ballon à détruire
                tampon += 1;
            }
        }

        // Si le score est au-dessus de l'objectif alors on annonce la fin de l'enregistrement
        if(SceneManager.GetActiveScene().name == "EnregistrementDev")
        {
            if (score >= PlayerPrefs.GetInt("ObjectifsDev"))
            {
                // Variable Environnement qui annonce la fin de l'enregistrement
                PlayerPrefs.SetInt("FinEnregistrement", 1);
            }
        }
    }

    void Destruction(GameObject[] listGO)
    {
        // Pour tous les ballons dans la listGO
        foreach (GameObject item in listGO)
        {
            // Si la coordonnée Z (la hauteur) du ballon est la même que celle indiquée dans les datas
            // Alors il s'agit du ballon à détruire
            if ( Mathf.Abs(item.transform.position.z - ListPos[tampon][2]) < 0.0001f)
            {
                GameObject cible = item;    // On a le ballon a détruire
                
                // On joue le son de disparition
                AudioClip son = cible.transform.gameObject.GetComponent<AudioSource>().clip;
                cible.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(son, 0.5f);
                // On le fait disparaitre
                cible.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                // On incrémente le score en fonction du type de ballon
                score += 1;
                ballons += 1;
                // Actualisation des variables UI
                textScore.text = "Score : " + score;
                textBallon.text = "Ballons détruits : " + ballons;

            }
        }
    }

    void LoadDestructionBallon()
    {
        // On initialise les variables
        string[] textArray;
        ListTemps.Clear();
        // Le chemi où l'on récupère les données des ballons détruits
        string path = Application.persistentDataPath + Path.DirectorySeparatorChar + "Données" + PlayerPrefs.GetInt("NumDos") + "/DataBallonDestruction.txt";
        // On récupère le fichier texte
        string readText = File.ReadAllText(path);
        // On le traite un peu pour extraire les informations utiles
        readText = readText.Replace("Liste regroupant le temps, le nom et la position du ballon détruit" + System.Environment.NewLine, "");
        readText = readText.Replace("BallonRouge(Clone)%", "");
        readText = readText.Replace("BallonDore(Clone)%", "");
        readText = readText.Replace("#", "");
        readText = readText.Replace("(", "");
        readText = readText.Replace(", ", "%");
        readText = readText.Replace(")", "%");
        readText = readText.Replace(System.Environment.NewLine, "");
        // On le mets dans la liste suivante grâce aux séparateurs ('%')
        textArray = readText.Split(new[] { "%" }, System.StringSplitOptions.None);

        for(int i=0; i<textArray.Length/4; i++)
        {
            // On ajoute le temps de destruction du solo
            ListTemps.Add(float.Parse(textArray[4*i], CultureInfo.InvariantCulture));
            // On ajoute la position associée à la destruction du ballon
            Vector3 kwa = new Vector3(float.Parse(textArray[i * 4 + 1], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 2], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 3], CultureInfo.InvariantCulture));
            ListPos.Add(kwa);
        }
    }
}
