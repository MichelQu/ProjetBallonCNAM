using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreationBallon : MonoBehaviour
{
    private float tempsEcoule1;
    private float tempsEcoule2;

    private float seuilBR;
    private float seuilBD;

    public GameObject ballon1;
    public GameObject ballon2;

    // Start is called before the first frame update
    void Start()
    {
        tempsEcoule1 = 0f;
        tempsEcoule2 = 0f;

        seuilBR = Random.Range(2.5f, 3.5f);
        seuilBD = Random.Range(12.5f, 17.5f);

        string path = Application.dataPath + "/Texte/dataBallonCreation.txt";
        File.WriteAllText(path,"Date de création des ballons dans le jeu, leurs coordonnées et spécificités :" + System.Environment.NewLine);
    }

    // Update is called once per frame
    void Update()
    {
        // On incrémente le temps
        tempsEcoule1 += Time.deltaTime;
        tempsEcoule2 += Time.deltaTime;

        // Si le temps incrémenté atteint le seuil alors on crée un nouveau ballon1
        if (tempsEcoule1 >= seuilBR)
        {
            float module = Random.Range(5.0f, 10f);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);

            float x = module * Mathf.Cos(angle);
            float z = module * Mathf.Sin(angle);

            Vector3 coord = new Vector3(x, 1, z);
            Instantiate(ballon1, coord, Quaternion.identity);

            // On réinitialise le temps
            tempsEcoule1 = 0f;
            seuilBR = Random.Range(2.5f, 3.5f);

            // On enregistre le lieu du ballon
            Save("rouge", coord);
        }

        // Si le temps incrémenté atteint le seuil alors on crée un nouveau ballon2
        if (tempsEcoule2 >= seuilBD)
        {
            float module = Random.Range(5.0f, 10f);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);

            float x = module * Mathf.Cos(angle);
            float z = module * Mathf.Sin(angle);

            Vector3 coord = new Vector3(x, 1, z);
            Instantiate(ballon2, coord, Quaternion.identity);

            // On réinitialise le temps et le seuil
            tempsEcoule2 = 0f;
            seuilBD = Random.Range(12.5f, 17.5f);

            // On enregistre le lieu du ballon
            Save("or", coord);
        }

    }


    private void Save(string nom, Vector3 coord)
    {
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

        // On en fait un seul string
        string saveString = string.Join(saveSeparator, content);
        // On l'ajoute au fichier texte
        File.AppendAllText(path, saveString + "%" + System.Environment.NewLine);
    }

}
