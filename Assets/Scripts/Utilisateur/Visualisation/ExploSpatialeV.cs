﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploSpatialeV : MonoBehaviour
{
    // Déclaration des variables
    private List<Vector3> ListPosVis;
    private GameObject[] listGO;
    public GameObject totem;

    private bool isDone;

    private void Start()
    {
        // Instanciation des variables
        ListPosVis = new List<Vector3>();
        isDone = false;
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
        ListPosVis = this.GetComponent<VisualisationVisu>().ListPosVisu;
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(0.78f, 0.78f, 0.78f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVis)
        {
            Instantiate(go1, loca, Quaternion.identity);
        }
        // On cherche tous les totems dans la scène et on change leur taille et couleur
        StartCoroutine(HeatMap());
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
        ListPosVis = this.GetComponent<VisualisationVisu>().ListPosVisu;
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(5.28f, 5.28f, 5.28f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVis)
        {
            Instantiate(go1, loca, Quaternion.identity);
        }
        // On cherche tous les totems dans la scène et on change leur taille et couleur
        StartCoroutine(HeatMap());
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
        ListPosVis = this.GetComponent<VisualisationVisu>().ListPosVisu;
        // On crée un gameObject parent sur lequel on va mettre les ballons
        GameObject go1 = totem;
        go1.transform.localScale = new Vector3(10.89f, 10.89f, 10.89f);
        go1.transform.tag = totem.transform.tag;
        // Pour chaque localisation, on instancie une sphère qui reproduit la zone d'exploration de l'acuité visuelle (3°)
        foreach (Vector3 loca in ListPosVis)
        {
            Instantiate(go1, loca, Quaternion.identity);
        }
        // On cherche tous les totems dans la scène et on change leur taille et couleur
        StartCoroutine(HeatMap());
    }


    private int rechercheMax(GameObject[] listTotem)
    {
        int Max = listTotem[0].GetComponent<Totem>().intersection;
        foreach (GameObject tot in listTotem)
        {
            int tampon = tot.GetComponent<Totem>().intersection;
            if (tampon > Max)
            {
                Max = tampon;
            }
        }
        return Max;
    }


    IEnumerator HeatMap()
    {
        // On laisse le temps pour que les totems puissent se mettre à jour
        yield return new WaitForSeconds(1);
        // On regroupe tous les totems dans une liste de totem
        GameObject[] listTotem = GameObject.FindGameObjectsWithTag("Totem");
        // On recherche le max
        int max = rechercheMax(listTotem);
        // On calcule les parts pour séparer en 5 groupes d'intersection
        int part1, part2, part3, part4;
        part4 = Mathf.RoundToInt(max * 0.8f);
        part3 = Mathf.RoundToInt(max * 0.6f);
        part2 = Mathf.RoundToInt(max * 0.4f);
        part1 = Mathf.RoundToInt(max * 0.2f);
        // Pour chaque totem si le totem possède des interactions on change les couleurs
        foreach (GameObject item in listTotem)
        {
            int inter = item.GetComponent<Totem>().intersection;
            if (inter > part4) // Rouge
            {
                item.GetComponent<Renderer>().material.color = new Color(1.0f, 0f, 0f, 1f);
            }
            if (inter > part3 && inter < (part4+1)) // Orange
            {
                item.GetComponent<Renderer>().material.color = new Color(1.0f, 0.5f, 0f, 1f);
            }
            if (inter > part2 && inter < (part3+1)) // Jaune
            {
                item.GetComponent<Renderer>().material.color = new Color(1.0f, 0.8f, 0f, 0.7f);
            }
            if (inter > part1 && inter < (part2+1)) // Vert
            {
                item.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 0.7f);
            }
            if (inter < part1 + 1) // Bleu
            {
                item.GetComponent<Renderer>().material.color = new Color(0f, 0f, 1f, 0.7f);
            }
        }
    }
}
