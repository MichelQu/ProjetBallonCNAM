using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class VisualisationVisu : MonoBehaviour
{
    // Déclaration des variables
    public List<Vector3> ListPosVisu;
    LineRenderer Line;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        // On initialise
        ListPosVisu = new List<Vector3>();
        // Le chemin associé aux datas de visualisation
        path = Application.persistentDataPath + Path.DirectorySeparatorChar + "dataVisualisationCamera.txt";
        // On charge les données de visualisation
        LoadDataVisualisation();
        // On dessine les traits
        Line = GetComponent<LineRenderer>();
        DessineLigne(Line);
    }

    void LoadDataVisualisation()
    {
        // Initialisation des variables
        string[] textArray;
        ListPosVisu.Clear();

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

        int longueur = textArray.Length / 4;

        for (int i = 0; i <longueur; i++)
        {
            // On ajoute la position de la visualisation
            Vector3 pos = new Vector3(float.Parse(textArray[4 * i + 1], CultureInfo.InvariantCulture), float.Parse(textArray[4 * i + 2], CultureInfo.InvariantCulture), float.Parse(textArray[4 * i + 3], CultureInfo.InvariantCulture));
            if (i > 0)
            {
                // Si la caméra ne bouge pas trop, pas besoin de dessiner
                Vector3 posComparaison = ListPosVisu[ListPosVisu.Count - 1];
                if (Mathf.Abs(posComparaison.x - pos.x) > 0.01f)
                {
                    if (Mathf.Abs(posComparaison.y - pos.y) > 0.01f)
                    {
                        if (Mathf.Abs(posComparaison.z - pos.z) > 0.01f)
                        {
                            ListPosVisu.Add(pos);
                        }
                    }
                }
            }
            else
            {
                // On ajoute la première position ici
                ListPosVisu.Add(pos);
            }
        }

    }

    void DessineLigne(LineRenderer Trait)
    {
        // On définit le nbr de points
        Trait.positionCount = ListPosVisu.Count;

        // On crée les couleurs
        Color c1 = Color.green;
        Color c2 = Color.blue;

        // On définit les couleurs des traits
        Trait.material = new Material(Shader.Find("Sprites/Default"));
        Trait.SetColors(c1, c2);

        // On définit les positions
        for (int i = 0; i < ListPosVisu.Count; i++)
        {
            // Debug.Log("Ça dessine");
            Trait.SetPosition(i, ListPosVisu[i]);
        }
    }
}
