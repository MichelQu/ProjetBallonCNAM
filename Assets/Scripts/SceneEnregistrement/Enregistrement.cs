using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class Enregistrement : MonoBehaviour
{


    #region Les variables
    // Les gameObjects
    public GameObject ballon1;
    public GameObject ballon2;
    public GameObject canvas;

    // Les listes des objets 
    private List<Quaternion> PointPos;
    public List<Vector3> ListBallonR;
    public List<Vector3> ListBallonD;

    // Les variables du scripts
    private float temps;
    private int nbr;
    private int tampon;
    private float echanti = 0.001f;
    private float tempsEcoule1;
    private float tempsEcoule2;
    private int nbrR;
    private int nbrD;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("FinEnregistrement");
        PlayerPrefs.SetInt("FinEnregistrement", 0);
        // On initialise les variables pour le tableau
        nbr = 0;
        tampon = 0;
        tempsEcoule1 = 0;
        tempsEcoule2 = 0;
        nbrR = 0;
        nbrD = 0;
        // On initialise la liste des quaternions et les listes de ballons
        PointPos = new List<Quaternion>();
        ListBallonR = new List<Vector3>();
        ListBallonD = new List<Vector3>();

        // On récupère toutes les infos des coordonnées qu'on a enregistré
        LoadDataBrut();
        LoadDataBallon();
    }

    // Update is called once per frame
    void Update()
    {
        // On incrémente le temps
        tempsEcoule1 += Time.deltaTime;
        tempsEcoule2 += Time.deltaTime;
        temps += Time.deltaTime;

        // On vérifie qu'il y a un truc dans l'enregistrement
        if (tampon < nbr)
        {
            if (temps > echanti)
            {
                // Debug.Log("En cours");
                transform.rotation = Quaternion.RotateTowards(transform.rotation, PointPos[tampon], 1f);
                tampon += 1;
                temps = 0f;
            }
            // On ajoute les ballons
            if (tempsEcoule1 >= 1)
            {
                if (nbrR < ListBallonR.Count)
                {
                    // Debug.Log("BallonR");
                    Vector3 coord = ListBallonR[nbrR];
                    nbrR += 1;
                    Instantiate(ballon1, coord, Quaternion.identity);
                    // On réinitialise le temps
                    tempsEcoule1 = 0f;
                }
            }

            if (tempsEcoule2 >= 5)
            {
                if (nbrD < ListBallonD.Count)
                {
                    // Debug.Log("BallonD");
                    Vector3 coord = ListBallonR[nbrD];
                    nbrD += 1;
                    Instantiate(ballon2, coord, Quaternion.identity);
                    // On réinitialise le temps
                    tempsEcoule2 = 0f;
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("FinEnregistrement", 1);
            // Debug.Log("Fin");
        }
    }


    void LoadDataBallon()
    {
        // Initialisation
        string[] textArray;
        ListBallonR.Clear();
        ListBallonD.Clear();
        Vector3 pos;
        // Les variables
        string path = Application.dataPath + "/Texte/dataBallonCreation.txt";

        // On récupère le fichier texte
        string readText = File.ReadAllText(path);
        // On le transforme un peu
        readText = readText.Replace("(", "");
        readText = readText.Replace(")", "");
        readText = readText.Replace(", ", "%");

        // string path2 = Application.dataPath + "/Texte/dataBallon2.txt";
        // File.WriteAllText(path2, readText);

        // On le mets dans un liste
        textArray = readText.Split(new[] { "%" }, System.StringSplitOptions.None);

        for (int i=0; i < (textArray.Length/4); i++)
        {
            if (textArray[4*i] == "rouge")
            {
                pos = new Vector3( float.Parse(textArray[i * 4 + 1], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 2], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 3], CultureInfo.InvariantCulture));
                ListBallonR.Add(pos);
            }
            if (textArray[4 * i] == "or")
            {
                pos = new Vector3(float.Parse(textArray[i * 4 + 1], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 2], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 3], CultureInfo.InvariantCulture));
                ListBallonD.Add(pos);
            }
        }

        // Debug.Log("Chargement effectué des ballons");
    }

    void LoadDataBrut()
    {
        string[] textArray;
        PointPos.Clear();

        string path = Application.dataPath + "/Texte/dataBrut.txt";
        // string saveString = File.ReadAllText(path);
        // string[] content = saveString.Split(new[] { saveSeparator }, System.StringSplitOptions.None);

        // On récupère le fichier texte
        string readText = File.ReadAllText(path);

        // On traite les information du fichier texte 
        readText = readText.Replace("Résultat expérience" + System.Environment.NewLine, "");
        readText = readText.Replace("(", "");
        readText = readText.Replace(")", " ");
        readText = readText.Replace(System.Environment.NewLine, "");
        readText = readText.Replace(",", "");

        //// On crée une liste grâce au fichier texte, chaque case de la liste est déterminé par les espaces dans le fichier texte
        textArray = readText.Split(new[] { " " }, System.StringSplitOptions.None);

        // Debug.Log("Longueur : " + textArray.Length);
        // Debug.Log("Longueur/3 : " + textArray.Length/3);

        //// On remplit la liste des vecteurs
        for (int i = 0; i < (textArray.Length / 4); i++)
        {
            Quaternion kwa = new Quaternion(float.Parse(textArray[i * 4], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 1], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 2], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 3], CultureInfo.InvariantCulture));
            // Vector3 kwa = new Vector3(float.Parse(textArray[i*3], CultureInfo.InvariantCulture), float.Parse(textArray[i*3+1], CultureInfo.InvariantCulture), float.Parse(textArray[i*3+2], CultureInfo.InvariantCulture));
            PointPos.Add(kwa);
            nbr += 1;
        }
        // string path2 = Application.dataPath + "/Texte/data.txt";
        // File.WriteAllText(path2, readText);

        // Debug.Log("Chargement effectué des rotations");
    }
}
