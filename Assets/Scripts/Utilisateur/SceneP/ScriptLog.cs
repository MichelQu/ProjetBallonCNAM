using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Globalization;
using UnityEngine.SceneManagement;

public class ScriptLog : MonoBehaviour
{
    // Quelques Variables 
    private float time = 0;
    private float echanti = 0.1f;
    int tamponScore = 0;
    // Les listes qui seront utiles 
    List<float> listTemps = new List<float>();
    List<Vector3> listErreur = new List<Vector3>();
    // Calcul de la distance
    Vector3 pos1;
    Vector3 pos2;
    public float angle;

    string saveSeparator = "%";
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        // On récupère les infos utiles aux starts
        listTemps.Clear();
        time = this.GetComponent<Initialisation>().temps;
        tamponScore = this.GetComponent<DestructionBallonV2>().ballons;
        pos1 = transform.forward;

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
        int nbrBallons = this.GetComponent<DestructionBallonV2>().ballons;

        // Si le temps est supérieur à la fréquence d'échantillonage
        if ( (time - (i*echanti)) >= echanti)
        {
            // On incrémente pour la prochaine frame
            i += 1;

            // On sauvegarde la rotation de la caméra et le temps associé
            SaveManager.si.SaveTransformRotPos(transform, time);
            // On sauvegarde la direction de la caméra
            SaveCamera(time);
            // On ajoute les distances angulaires
            pos2 = transform.forward;
            angle += Vector3.Angle(pos1, pos2);
            pos1 = pos2;
        }

        // Si le score a évolué donc 
        if (nbrBallons != tamponScore)
        {
            // Debug.Log("On ajoute un temps");
            // On modifie le score 
            tamponScore = nbrBallons;
            // On note le temps dans un liste
            listTemps.Add(time);
        }

        //if (nbrBallons == PlayerPrefs.GetInt("Ballons"))
        //{
        //    SauvegardeDonnee(time, angle);
        //    Debug.Log("Sauvegarde");
        //}
    }

    public void SauvegardeDonnee(float timer, float angleD)
    {
        string path = Application.dataPath + "/Texte/dataDiverses.txt";
        // Information sur l'expérience réalisée
        File.AppendAllText(path, "- Nombre de ballons détruits : " + PlayerPrefs.GetInt("Ballons") + " ballons" + System.Environment.NewLine);
        File.AppendAllText(path, "- Durée totale de l'expérience : " + timer + "s" + System.Environment.NewLine);
        File.AppendAllText(path, "- Durée moyenne d'explosion des ballons : " + timer/PlayerPrefs.GetInt("Ballons") + "s" + System.Environment.NewLine);

        // On récupère la liste des erreurs
        listErreur = this.GetComponent<DestructionBallonV2>().ListErrorMoyenne;
        float ErreurMoy = 0f;

        foreach(Vector3 erreur in listErreur)
        {
            ErreurMoy += Mathf.Sqrt(erreur.x * erreur.x + erreur.y * erreur.y + erreur.y * erreur.y);
        }
        ErreurMoy = ErreurMoy / PlayerPrefs.GetInt("Ballons");
        File.AppendAllText(path, "- Erreur de précision moyenne dans la destruction des ballons : " + ErreurMoy + System.Environment.NewLine);
        File.AppendAllText(path, "- Distance angulaire parcourue par ballon : " + angleD / PlayerPrefs.GetInt("Ballons") + "°" + System.Environment.NewLine);
        File.AppendAllText(path, System.Environment.NewLine + "Informations diverses sur la destruction des ballons : " + System.Environment.NewLine);

        int i = 1;
        float tamponTemps = 0f;

        foreach (float item in listTemps)
        {
            // On calcule la différence de temps 
            float differenceTemps = item - tamponTemps;
            Vector3 erreur = listErreur[i - 1];
            float echelle = Mathf.Sqrt(erreur.x * erreur.x + erreur.y * erreur.y + erreur.y * erreur.y);
            // On écrit ce qu'on va mettre dans le fichier texte
            string debutMot = "- Temps destruction Ballon " + i + " : ";
            string finMot = "s (+ " + differenceTemps + "s)";
            string alinea = "       ";
            string erreurS = "Précision de la visée : " + echelle;
            // On l'ajoute dans le fichier texte
            File.AppendAllText(path,debutMot + item + finMot + System.Environment.NewLine + alinea + erreurS + System.Environment.NewLine);
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
