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
    // private List<Vector3> PointPos;

    private List<Vector3> ListBallonR;
    private List<Vector3> ListBallonD;
    private List<float> ListTempsR;
    private List<float> ListTempsD;

    // Les variables du scripts
    private int nbr;
    private int tampon;
    private float echanti = 0.001f;
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
        nbrR = 0;
        nbrD = 0;
        // On initialise la liste des quaternions et les listes de ballons
        PointPos = new List<Quaternion>();
        // PointPos = new List<Vector3>();
        ListBallonR = new List<Vector3>();
        ListBallonD = new List<Vector3>();
        ListTempsR = new List<float>();
        ListTempsD = new List<float>();

        // On récupère toutes les infos des coordonnées qu'on a enregistré
        LoadDataBrut();
        LoadDataBallon();
        // LoadDataCamera();
    }

    // Update is called once per frame
    void Update()
    {
        // On incrémente le temps
        float temps = this.GetComponent<InitialisationSE>().temps;

        // On vérifie qu'il y a un truc dans l'enregistrement
        if (tampon < nbr)
        {
            // On réalise la rotation
            if ((temps - tampon*echanti) > echanti)
            {
                // Debug.Log("En cours");
                // TODO la rapidité est à changer car il dépend du temps (autour des 1.2f à 1.3f)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, PointPos[tampon], 1.25f);

                // Quaternion rotation1 = Quaternion.LookRotation(PointPos[tampon], Vector3.up);
                // transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation1, 1f);

                tampon += 1;
            }

            // On ajoute les ballons Rouges et Dorés selon le temps
            if (Mathf.Abs(temps - ListTempsR[nbrR]) < 0.01f )
            {
                if (nbrR < ListBallonR.Count)
                {
                    Debug.Log("BallonR");
                    Vector3 coord = ListBallonR[nbrR];
                    nbrR += 1;
                    Instantiate(ballon1, coord, Quaternion.identity);
                }
            }

            if (Mathf.Abs(temps - ListTempsD[nbrD]) < 0.01f)
            {
                if (nbrD < ListBallonD.Count)
                {
                    Debug.Log("BallonD");
                    Vector3 coord = ListBallonR[nbrD];
                    nbrD += 1;
                    Instantiate(ballon2, coord, Quaternion.identity);
                }
            }
        }
        else
        {
            // Variable Environnement qui annonce la fin de l'enregistrement
            PlayerPrefs.SetInt("FinEnregistrement", 1);
            // Debug.Log("Fin");
        }
    }

    //void LoadDataCamera()
    //{
    //    // Initialisation
    //    string[] textArray;
    //    ListPosition.Clear();
    //    Vector3 pos;
    //    // Les variables
    //    string path = Application.dataPath + "/Texte/dataBrutCamera.txt";

    //    // On récupère le fichier texte
    //    string readText = File.ReadAllText(path);
    //    // On le transforme un peu
    //    readText = readText.Replace("(", "");
    //    readText = readText.Replace(")", "");
    //    readText = readText.Replace(", ", "%");
    //    readText = readText.Replace(System.Environment.NewLine, "%");

    //    // string path2 = Application.dataPath + "/Texte/dataBrutCamera2.txt";
    //    // File.WriteAllText(path2, readText);

    //    // On le mets dans un liste
    //    textArray = readText.Split(new[] { "%" }, System.StringSplitOptions.None);

    //    Debug.Log("Longueur : " + textArray.Length);
    //    for (int i = 0; i < (textArray.Length / 4); i++)
    //    {
            
    //    }

    //    // Debug.Log("Chargement effectué des ballons");
    //}

    void LoadDataBallon()
    {
        // Initialisation
        string[] textArray;
        ListBallonR.Clear();
        ListBallonD.Clear();
        ListTempsR.Clear();
        ListTempsD.Clear();
        // Les variables
        string path = Application.dataPath + "/Texte/dataBallonCreation.txt";

        // On récupère le fichier texte
        string readText = File.ReadAllText(path);
        // On le transforme un peu
        readText = readText.Replace("Date de création des ballons dans le jeu, leurs coordonnées et spécificités :", "");
        readText = readText.Replace("(", "");
        readText = readText.Replace(")", "");
        readText = readText.Replace(", ", "%");
        readText = readText.Replace("#", "");
        readText = readText.Replace(System.Environment.NewLine, "");

        string path2 = Application.dataPath + "/Texte/dataBallon2.txt";
        File.WriteAllText(path2, readText);

        // On le mets dans un liste
        textArray = readText.Split(new[] { "%" }, System.StringSplitOptions.None);

        for (int i=0; i < (textArray.Length/5); i++)
        {
            if (textArray[5*i+1] == "rouge")
            {
                ListTempsR.Add(float.Parse(textArray[5 * i], CultureInfo.InvariantCulture));
                Vector3 pos = new Vector3(float.Parse(textArray[5 * i + 2], CultureInfo.InvariantCulture), float.Parse(textArray[5 * i + 3], CultureInfo.InvariantCulture), float.Parse(textArray[5 * i + 4], CultureInfo.InvariantCulture));
                ListBallonR.Add(pos);
            }
            if (textArray[5*i+1] == "or")
            {
                ListTempsD.Add(float.Parse(textArray[5 * i], CultureInfo.InvariantCulture));
                Vector3 pos = new Vector3(float.Parse(textArray[5 * i + 2], CultureInfo.InvariantCulture), float.Parse(textArray[5 * i + 3], CultureInfo.InvariantCulture), float.Parse(textArray[5 * i + 4], CultureInfo.InvariantCulture));
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
        readText = readText.Replace("Résultat expérience Orientation de la caméra (Quaternion)" + System.Environment.NewLine, "");
        readText = readText.Replace("(", "");
        readText = readText.Replace(")", " ");
        readText = readText.Replace(System.Environment.NewLine, "");
        readText = readText.Replace(",", "");
        readText = readText.Replace("%", " ");

        //// On crée une liste grâce au fichier texte, chaque case de la liste est déterminé par les espaces dans le fichier texte
        textArray = readText.Split(new[] { " " }, System.StringSplitOptions.None);

        // Debug.Log("Longueur : " + textArray.Length);
        // Debug.Log("Longueur/3 : " + textArray.Length/3);

        // On remplit la liste des vecteurs
        for (int i = 0; i < (textArray.Length / 5); i++)
        {
            Quaternion kwa = new Quaternion(float.Parse(textArray[i*5+1], CultureInfo.InvariantCulture), float.Parse(textArray[i * 5 + 2], CultureInfo.InvariantCulture), float.Parse(textArray[i * 5 + 3], CultureInfo.InvariantCulture), float.Parse(textArray[i * 5 + 4], CultureInfo.InvariantCulture));
            // Vector3 kwa = new Vector3(float.Parse(textArray[i*4+1], CultureInfo.InvariantCulture), float.Parse(textArray[i*4+2], CultureInfo.InvariantCulture), float.Parse(textArray[i*4+3], CultureInfo.InvariantCulture));
            PointPos.Add(kwa);
            nbr += 1;
        }

        // string path2 = Application.dataPath + "/Texte/dataExpe.txt";
        // File.WriteAllText(path2, readText);

        // Debug.Log("Chargement effectué des rotations");
    }
}
