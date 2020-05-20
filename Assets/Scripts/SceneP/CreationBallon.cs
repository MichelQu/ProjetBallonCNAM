using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreationBallon : MonoBehaviour
{
    float tempsEcoule1,tempsEcoule2; // Temps écoulé depuis le dernier ajout de ballon associé

    float seuilBR, seuilBD; // Date à laquelle, on ajoute des ballons

    public GameObject ballon1; // Préfab Ballon Rouge
    public GameObject ballon2; // Préfab Ballon Doré

    public bool aleatoire = true; // Pouvoir choisir si l'apparition des ballons est aléatoire ou non
    int i = 0; // Pour gérer l'apparition des ballons si ce n'est pas aléatoire


    void Start()
    {
        // On initialise les données 
        tempsEcoule1 = 0f;
        // tempsEcoule2 = 0f;

        // On crée une date random comme seuil pour l'apparition des ballons
        seuilBR = 3.0f + Random.Range(-0.5f, 0.5f);
        seuilBD = 15.0f + Random.Range(-2.5f, 2.5f);

        // Inialisation des Data liées à la création des ballons
        string path = Application.dataPath + "/Texte/dataBallonCreation.txt";
        File.WriteAllText(path,"Date de création des ballons dans le jeu, leurs coordonnées et spécificités :" + System.Environment.NewLine);
    }

    void Update()
    {
        if (aleatoire)
        // Si l'apparition des ballons est aléatoire
        {
            // On incrémente le temps écoulé
            tempsEcoule1 += Time.deltaTime;
            tempsEcoule2 += Time.deltaTime;

            // Si le temps incrémenté atteint le seuil alors on crée un nouveau ballon1 (ballon rouge)
            if (tempsEcoule1 >= seuilBR)
            {
                // On crée un module et un angle aléatoire pour le positionnement du ballon
                float module = 7.5f + Random.Range(-2.5f, 2.5f);
                float angle = Random.Range(-Mathf.PI, Mathf.PI);

                float x = module * Mathf.Cos(angle);
                float z = module * Mathf.Sin(angle);

                // On ajoute le ballon au coordonnée créé
                Vector3 coord = new Vector3(x, 0, z);
                GameObject go = Instantiate(ballon1, coord, Quaternion.identity);

                go.GetComponent<Ballon>().norme = module;

                // On réinitialise le temps écoulé et on crée une nouvelle date pour l'apparition du prochain ballon
                tempsEcoule1 = 0f;
                seuilBR = 3.0f + Random.Range(-0.5f, 0.5f);

                // On sauvegarde les data liées à la création ballon rouge
                Save("rouge", coord);
            }

            // Si le temps incrémenté atteint le seuil alors on crée un nouveau ballon2
            //if (tempsEcoule2 >= seuilBD)
            //{
            //    // On crée un module et un angle aléatoire pour le positionnement du ballon
            //    float module = 7.5f + Random.Range(-2.5f, 2.5f);
            //    float angle = Random.Range(-Mathf.PI, Mathf.PI);

            //    float x = module * Mathf.Cos(angle);
            //    float z = module * Mathf.Sin(angle);

            //    // On ajoute le ballon au coordonnée créé
            //    Vector3 coord = new Vector3(x, 0, z);
            //    Instantiate(ballon2, coord, Quaternion.identity);

            //    // On réinitialise le temps écoulé et on crée une nouvelle date pour l'apparition du prochain ballon
            //    tempsEcoule2 = 0f;
            //    seuilBD = 15.0f + Random.Range(-2.5f, 2.5f);

            //    // On sauvegarde les data liées à la création ballon doré
            //    Save("or", coord);
            //}
        }


        else
        // Si l'apparition des ballons n'est pas aléatoire, on crée des ballons en cercle etc
        {
            tempsEcoule1 += Time.deltaTime;
            if (tempsEcoule1 > 1.0f)
            {
                float module = 5.0f;
                if (i > 12) { i = 0;  }
                float angle = -Mathf.PI + Mathf.PI*i / 6;
                i += 1;

                float x = module * Mathf.Cos(angle);
                float z = module * Mathf.Sin(angle);

                Vector3 coord = new Vector3(x, 0, z);
                Instantiate(ballon1, coord, Quaternion.identity);
                tempsEcoule1 = 0f;

                // On enregistre le lieu du ballon
                Save("rouge", coord);
            }
        }
    }


    private void Save(string nom, Vector3 coord)
    {
        // Lieu de stockage des data liées à la création des ballons
        string path = Application.dataPath + "/Texte/dataBallonCreation.txt";
        string saveSeparator = "%";

        float timer1 = this.GetComponent<Initialisation>().temps; // Le temps dans le jeu
        string timer2 = "#" + timer1.ToString();

        string[] content =
        {
            timer2,
            nom,
            coord.ToString("F4")
        };

        // On en fait un seul string avec saveSeparator comme séparateur
        string saveString = string.Join(saveSeparator, content);
        // On l'ajoute au fichier texte
        File.AppendAllText(path, saveString + "%" + System.Environment.NewLine);
    }

}
