using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

// Ce scrit permet de donner des scores de visualisation

public class ExplorationSpatiale : MonoBehaviour
{

    private int nbrBallon;
    private List<float> ListTempsVisu;
    private List<Vector3> ListPosVisu;

    public GameObject[] listGO;
    private GameObject[] listGO1;
    private GameObject[] listGO2;

    public GameObject totem;
    // private GameObject container;
    private GameObject container2;

    private void Start()
    {
        ListTempsVisu = new List<float>();
        ListPosVisu = new List<Vector3>();
    }

    public void DestructionFin()
    {
        // Cette fonction détruit les ballons restant sur la scène
        listGO1 = GameObject.FindGameObjectsWithTag("Ballon");
        listGO2 = GameObject.FindGameObjectsWithTag("BallonTest");
        foreach (GameObject item in listGO1)
        {
            Destroy(item);
        }

        foreach (GameObject item in listGO2)
        {
            Destroy(item);
        }
    }

    public List<float> Scores()
    {
        // On définit les listes voulues
        List<float> listScore = new List<float>();
        // On lit les données de visualisation
        LoadDataVisualisationES();
        // On ajoute les scores dans la liste qui sera retournée
        listScore.Add(ScoreAcuite());
        listScore.Add(ScoreLecture());
        listScore.Add(ScoreSymbole());
        return (listScore);
    }

    float ScoreAcuite()
    {
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(0.78f, 0.78f, 0.78f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVisu)
        {       
            Instantiate(go1, loca, Quaternion.identity);
        }

        // On retourne le score
        float scoreExploration = calculScore();
        // On supprime les gameObjects crées
        listGO = GameObject.FindGameObjectsWithTag("Totem");
        foreach(GameObject item in listGO)
        {
            Destroy(item);
        }
        return (scoreExploration);
    }

    float ScoreLecture()
    {
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(5.28f, 5.28f, 5.28f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVisu)
        {
            Instantiate(go1, loca, Quaternion.identity);
        }
        // On retourne le score
        float scoreExploration = calculScore();
        // On supprime les gameObjects crées
        listGO = GameObject.FindGameObjectsWithTag("Totem");
        foreach (GameObject item in listGO)
        {
            Destroy(item);
        }
        return (scoreExploration);
    }

    float ScoreSymbole()
    {
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(10.89f, 10.89f, 10.89f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVisu)
        {
            Instantiate(go1, loca, Quaternion.identity);
        }

        // On retourne le score
        float scoreExploration = calculScore();
        // On supprime les gameObjects crées
        listGO = GameObject.FindGameObjectsWithTag("Totem");
        foreach (GameObject item in listGO)
        {
            Destroy(item);
        }
        return (scoreExploration);
    }

    float calculScore()
    {
        // On crée une boîte pour réaliser la tâche
        container2 = new GameObject("Identification");
        container2.transform.position = Vector3.zero;
        container2.transform.rotation = Quaternion.identity;
        float nbrDetection = 0;
        // On regarde dans toutes les directions pour voir s'il y a des totems
        for (int z = 0; z < 480; z += 1)
        {
            for (int x = 0; x < 480; x += 1)
            {
                // Création des rayons qui seront tirés
                Vector3 vision = transform.forward;
                Ray ray = new Ray(container2.transform.position, vision);
                RaycastHit hit;
                // On tire un rayon dans la direction choisi
                if (Physics.Raycast(ray,out hit, 100f))
                {
                    if(hit.transform.tag == "Totem")
                    {
                        nbrDetection += 1f;
                    }
                }
                this.transform.Rotate(new Vector3(-0.75f, 0, 0));
            }
            this.transform.Rotate(new Vector3(0, 0.75f, 0));
        }
        // Calcul du score
        float pixelUtile = 480 * 480 * 0.5f; // Exploration de la demi sphère supérieure
        float score = (nbrDetection / pixelUtile) * 100f;
        // On réinitialise et retourne le score
        Destroy(container2);
        return score;
    }

    public void LoadDataVisualisationES()
    {
        // Initialisation des variables
        string[] textArray;
        ListTempsVisu.Clear();
        ListPosVisu.Clear();

        // Le chemin associé aux datas de visualisation
        string path = Application.dataPath + "/Texte/dataVisualisationCamera.txt";

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

        for (int i = 0; i < (textArray.Length / 4); i++)
        {
            // On ajoute la position de la visualisation
            Vector3 pos = new Vector3(float.Parse(textArray[4 * i + 1], CultureInfo.InvariantCulture), float.Parse(textArray[4 * i + 2], CultureInfo.InvariantCulture), float.Parse(textArray[4 * i + 3], CultureInfo.InvariantCulture));

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
}
