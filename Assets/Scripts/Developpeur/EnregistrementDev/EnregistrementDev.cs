using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

// Ce script gère l'apparition des ballons dans la scène enregistrement

public class EnregistrementDev : MonoBehaviour
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

    private List<float> ListTempsVisu;
    private List<Vector3> ListPosVisu;

    // Line Renderer
    LineRenderer Trait;

    // Les variables du scripts
    private int nbrR;
    private int nbrD;
    private int nbrVisu;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("FinEnregistrement");
        PlayerPrefs.SetInt("FinEnregistrement", 0);

        // On initialise les variables pour le tableau
        nbrR = 0;
        nbrD = 0;
        nbrVisu = 0;
        // on récupère le lineRenderer de la cam
        Trait= GetComponent<LineRenderer>();

        // On initialise les listes des ballons et des temps d'apparition de ces ballons
        ListBallonR = new List<Vector3>();
        ListBallonD = new List<Vector3>();
        ListTempsR = new List<float>();
        ListTempsD = new List<float>();
        // On initialise les listes de temps et de coordonnées pour faire l'apparition de la visualisation
        ListTempsVisu = new List<float>();
        ListPosVisu = new List<Vector3>();

        // On récupère toutes les infos des coordonnées qu'on a enregistré
        LoadDataBallon();
        LoadDataVisualisation();
    }

    // Update is called once per frame
    void Update()
    {
        // On incrémente le temps
        float temps = this.GetComponent<InitialisationDev>().temps;

        // On ajoute les ballons Rouges et Dorés selon le temps
        if (nbrR < ListBallonR.Count)
        {
            if (Mathf.Abs(temps - ListTempsR[nbrR]) < 0.02f )
            {
                Vector3 coord = ListBallonR[nbrR]; // On récupre les coordonnées 
                nbrR += 1; // On incrémente pour la suite
                Instantiate(ballon1, coord, Quaternion.identity); // On instancie le ballon rouge
            }
        }

        if (nbrD < ListBallonD.Count)
        {
            if (Mathf.Abs(temps - ListTempsD[nbrD]) < 0.02f)
            {
                Vector3 coord = ListBallonR[nbrD]; // On récupre les coordonnées 
                nbrD += 1; // On incrémente pour la suite
                Instantiate(ballon2, coord, Quaternion.identity); // On instancie le ballon doré
            }
        }

        if (nbrVisu < ListPosVisu.Count)
        {
            if (Mathf.Abs(temps - ListTempsVisu[nbrVisu]) < 0.02f)
            {
                DrawLineEnregistrement(nbrVisu); // On dessine les lignes de visualisation
                nbrVisu += 1 ; // On incrémente pour la suite
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
        string path = Application.dataPath + "/Texte/profond/Données" + PlayerPrefs.GetInt("NumDos") + "/DataBallonCreation.txt";

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

    void LoadDataVisualisation()
    {
        // Initialisation des variables
        string[] textArray;
        ListTempsVisu.Clear();
        ListPosVisu.Clear();

        // Le chemin associé aux datas de visualisation
        string path = Application.dataPath + "/Texte/profond/Données" + PlayerPrefs.GetInt("NumDos") + "/DataVisualisationCamera.txt";

        // On récupère le fichier texte
        string readText = File.ReadAllText(path);

        // On le transforme un peu pour pouvoir l'utiliser plus tard
        readText = readText.Replace("Visualisation de la direction de la Caméra : " + System.Environment.NewLine, "");
        readText = readText.Replace("(", "");
        readText = readText.Replace(")", "");
        readText = readText.Replace(", ", "%");
        readText = readText.Replace("#", "");
        readText = readText.Replace(System.Environment.NewLine, "");

        // On le mets dans un liste de String grâce au séparateur (%) qu'on a mis dans le fichier txt
        textArray = readText.Split(new[] { "%" }, System.StringSplitOptions.None);

        for (int i = 0; i < (textArray.Length/4); i++)
        {
            // On ajoute la position de la visualisation
            Vector3 pos = new Vector3(float.Parse(textArray[4 * i + 1], CultureInfo.InvariantCulture), float.Parse(textArray[4 * i+2], CultureInfo.InvariantCulture), float.Parse(textArray[4 * i+3], CultureInfo.InvariantCulture));

            if (i > 0)
            {
                // Si la caméra ne bouge pas trop, pas besoin d'ajouter de points pour dessiner
                Vector3 posComparaison = ListPosVisu[ListPosVisu.Count - 1];
                if (Mathf.Abs(posComparaison.x - pos.x) > 0.01f)
                {
                    if (Mathf.Abs(posComparaison.y - pos.y) > 0.01f)
                    {
                        if (Mathf.Abs(posComparaison.z - pos.z) > 0.01f)
                        {
                            // Les points ne sont pas identiques donc on ajoute
                            ListPosVisu.Add(pos);
                            ListTempsVisu.Add(float.Parse(textArray[4 * i], CultureInfo.InvariantCulture));
                        }
                    }
                }
            }
            else
            {
                // On ajoute la première position et le premier temps ici
                ListTempsVisu.Add(float.Parse(textArray[4 * i], CultureInfo.InvariantCulture));
                ListPosVisu.Add(pos);
            }
        }
    }

    void DrawLineEnregistrement(int nbr)
    {
        Trait.positionCount = nbr; // On dit le nombre de point pour le dessin

        // On crée les couleurs
        Color c1 = Color.green;
        Color c2 = Color.blue;

        // On définit les couleurs des traits
        Trait.material = new Material(Shader.Find("Sprites/Default"));
        Trait.SetColors(c1, c2);

        for (int i =0; i < nbr; i++)
        {
            Trait.SetPosition(i, ListPosVisu[i]); // On rentre les positions
        }
    }
}
