using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Globalization;
using UnityEngine.SceneManagement;

public class ScriptLog : MonoBehaviour
{

    private float time = 0;
    private float echanti = 0.1f;
    int tamponScore = 0;
    List<float> listTemps = new List<float>();

    string saveSeparator = "%";
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        // On récupère les infos utiles aux starts
        listTemps.Clear();
        time = this.GetComponent<Initialisation>().temps;
        tamponScore = this.GetComponent<DestructionBallon>().score;

        // Initialisation des Data de l'Orientation de la Caméra
        SaveManager.si.Clear(true);
        SaveManager.si.SaveTransformRotPos(transform, time);

        // Initialisation des Data de la Direction Caméra
        string path1 = Application.dataPath + "/Texte/dataVisualisationCamera.txt";
        File.WriteAllText(path1, "Visualisation de la direction de la Caméra : " + System.Environment.NewLine);

        // Initialisation des Data diverse
        string path2 = Application.dataPath + "/Texte/dataDiverses.txt";
        File.WriteAllText(path2, "Quelques données variées sur l'expérience : " + System.Environment.NewLine);
    }

    // Update is called once per frame
    void Update()
    {
        // Récupération du temps
        time = this.GetComponent<Initialisation>().temps;
        // Récupération du score
        int score = this.GetComponent<DestructionBallon>().score;

        // Si le temps est supérieur à la fréquence d'échantillonage
        if ( (time - (i*echanti)) >= echanti)
        {
            // On incrémente pour la prochaine frame
            i += 1;

            // On sauvegarde la rotation de la caméra et le temps associé
            SaveManager.si.SaveTransformRotPos(transform, time);
            // On sauvegarde la direction de la caméra
            SaveCamera(time);
        }

        // Si le score a évolué donc 
        if (score != tamponScore)
        {
            Debug.Log("On ajoute un temps");
            // On modifie le score 
            tamponScore = score;
            // On note le temps dans un liste
            listTemps.Add(time);
        }

        if (score >= 5)
        {
            SauvegardeDonnee(time);
        }
    }

    void SauvegardeDonnee(float timer)
    {
        string path = Application.dataPath + "/Texte/dataDiverses.txt";
        File.AppendAllText(path, "- Nombre de ballons détruits : " + PlayerPrefs.GetInt("Ballons") + " ballons" + System.Environment.NewLine);
        File.AppendAllText(path, "- Durée totale de l'expérience : " + timer + "s" + System.Environment.NewLine);
        File.AppendAllText(path, "- Durée moyenne d'explosion des ballons : " + timer/PlayerPrefs.GetInt("Ballons") + "s" + System.Environment.NewLine);

        File.AppendAllText(path, System.Environment.NewLine + "Le temps de destruction des ballons : " + System.Environment.NewLine);

        int i = 1;
        float tamponTemps = 0f;

        foreach (float item in listTemps)
        {
            // On calcule la différence de temps 
            float differenceTemps = item - tamponTemps;
            // On écrit ce qu'on va mettre dans le fichier texte
            string debutMot = "- Temps destruction Ballon " + i + " : ";
            string finMot = "s (+ " + differenceTemps + "s)";
            // On l'ajoute dans le fichier texte
            File.AppendAllText(path,debutMot + item + finMot + System.Environment.NewLine);

            // On redéfinit les variables pour la suite
            i += 1;
            tamponTemps = item;
        }
    }

    void SaveCamera(float timer)
    {
        var Regard = this.transform.forward*30 + transform.position;
        string path = Application.dataPath + "/Texte/dataVisualisationCamera.txt";

        string timeMod = "#" + timer.ToString();

        string[] content =
        {
            timeMod,
            Regard.ToString("G") + "%"
        };

        string saveString = string.Join(saveSeparator, content) + System.Environment.NewLine;
        File.AppendAllText(path, saveString);
    }

}
