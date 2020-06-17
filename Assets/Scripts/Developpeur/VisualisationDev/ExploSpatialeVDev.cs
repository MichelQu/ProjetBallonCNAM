using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploSpatialeVDev : MonoBehaviour
{
    // On instancie les variables
    private List<Vector3> ListPosVis;
    private GameObject[] listGO;
    public GameObject totem;
    // On instancie le booléen qui indique s'il y a des totems/HeatMap dans la scène
    private bool isDone;
    private Camera cam;

    private void Start()
    {
        ListPosVis = new List<Vector3>();
        isDone = false;
        cam = Camera.main;
    }

    public void HeatMapFermeture()
    {
        // Si des données sont chargées
        if (isDone)
        {
            // On supprime les gameObjects crées pour totem
            listGO = GameObject.FindGameObjectsWithTag("Totem");
            foreach (GameObject item in listGO)
            {
                Destroy(item);
            }
            // Les données ne sont plus chargées donc on change la variable
            isDone = false;
        }
    }

    public void VisualisationAcuite()
    {
        // Si des données d'exploration spatiale sont déjà présents dans la scène
        if (isDone)
        {
            // On supprime tous les totems qui sont présents dans la scène qu'il y en ait ou pas
            HeatMapFermeture();
        }
        // On change la variable
        isDone = true;
        // On charge les données de visualisation
        ListPosVis = cam.GetComponent<VisualisationVisu>().ListPosVisu;
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(0.78f, 0.78f, 0.78f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVis)
        {
            Instantiate(go1, loca, Quaternion.identity);
        }
        // On cherche tous les totems dans la scène et on change leur couleur
        listGO = GameObject.FindGameObjectsWithTag("Totem");
        foreach (GameObject item in listGO)
        {
            item.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f);
        }
    }

    public void VisualisationLecture()
    {
        // Si des données d'exploration spatiale sont déjà présents dans la scène
        if (isDone)
        {
            // On supprime tous les totems qui sont présents dans la scène qu'il y en ait ou pas
            HeatMapFermeture();
        }
        // On change la variable
        isDone = true;
        // On charge les données de visualisation
        ListPosVis = cam.GetComponent<VisualisationVisu>().ListPosVisu;
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(5.28f, 5.28f, 5.28f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVis)
        {
            Instantiate(go1, loca, Quaternion.identity);
        }
        // On cherche tous les totems dans la scène et on change leur couleur
        listGO = GameObject.FindGameObjectsWithTag("Totem");
        foreach (GameObject item in listGO)
        {
            item.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f);
        }
    }

    public void VisualisationSymbole()
    {
        // Si des données d'exploration spatiale sont déjà présents dans la scène
        if (isDone)
        {
            // On supprime tous les totems qui sont présents dans la scène qu'il y en ait ou pas
            HeatMapFermeture();
        }
        // On change la variable
        isDone = true;
        // On charge les données de visualisation
        ListPosVis = cam.GetComponent<VisualisationVisu>().ListPosVisu;
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(10.89f, 10.89f, 10.89f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVis)
        {
            Instantiate(go1, loca, Quaternion.identity);
        }
        // On cherche tous les totems dans la scène et on change leur couleur
        listGO = GameObject.FindGameObjectsWithTag("Totem");
        foreach (GameObject item in listGO)
        {
            item.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f);
        }
    }
}
