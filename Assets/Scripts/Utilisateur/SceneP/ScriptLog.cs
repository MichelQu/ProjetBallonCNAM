using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScriptLog : MonoBehaviour
{
    // Déclaration des variables
    private float time = 0;
    float echanti = 0.1f;
    int i = 0;
    // Les listes qui seront utiles 
    List<float> listTemps = new List<float>();
    List<Vector3> listErreur = new List<Vector3>();
    public List<Vector3> listVisualisation = new List<Vector3>();
    // Calcul de la distance
    Vector3 pos1;
    Vector3 pos2;
    public float angle;
    // Les liens des datas
    string path1, path2;
    string saveSeparator = "%";

    // Start is called before the first frame update
    void Start()
    {
        // On récupère les infos utiles aux starts
        listTemps.Clear();
        time = this.GetComponent<Initialisation>().temps;
        pos1 = transform.forward;
        // Initialisation des Data de la Direction Caméra
        path1 = Application.persistentDataPath + Path.DirectorySeparatorChar + "dataVisualisationCamera.txt";
        File.WriteAllText(path1, "Visualisation de la direction de la Caméra : " + System.Environment.NewLine);
        // Initialisation des Data diverse
        path2 = Application.persistentDataPath + Path.DirectorySeparatorChar + "dataDiverses.txt";
        File.WriteAllText(path2, "Quelques données variées sur l'expérience : " + System.Environment.NewLine);
        // Initialisation des Data de l'Orientation de la Caméra
        SaveManager.si.Clear(false);
        SaveManager.si.SaveTransformRotPos(transform, time);
    }

    // Update is called once per frame
    void Update()
    {
        // Récupération du temps
        time = this.GetComponent<Initialisation>().temps;
        // Si le temps est supérieur à la fréquence d'échantillonage
        if ((time - (i * echanti)) >= echanti)
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
        // On ajoute les vecteurs de visualisation
        listVisualisation.Add(this.transform.forward * 30 + transform.position);
    }

    public void SauvegardeDonnee()
    {
        // On récupère le temps 
        float timer = this.GetComponent<Initialisation>().temps;
        // On récupère les scores dans le script Exploration Spatiale
        List<float> listScore = this.GetComponent<ExplorationSpatiale>().Scores();
        // Information sur l'expérience réalisée
        File.AppendAllText(path2, "- Nombre de ballons détruits : " + PlayerPrefs.GetInt("Ballons") + " ballons" + System.Environment.NewLine);
        File.AppendAllText(path2, "- Durée totale de l'expérience : " + timer + "s" + System.Environment.NewLine);
        File.AppendAllText(path2, "- Durée moyenne d'explosion des ballons : " + timer/PlayerPrefs.GetInt("Ballons") + "s" + System.Environment.NewLine);

        // On récupère la liste des erreurs
        listErreur = this.GetComponent<DestructionBallonV2>().ListErrorMoyenne;
        float ErreurMoy = 0f;

        foreach(Vector3 erreur in listErreur)
        {
            ErreurMoy += Mathf.Sqrt(erreur.x * erreur.x + erreur.y * erreur.y + erreur.y * erreur.y);
        }
        ErreurMoy = ErreurMoy / PlayerPrefs.GetInt("Ballons");
        File.AppendAllText(path2, "- Erreur de précision moyenne dans la destruction des ballons : " + ErreurMoy + System.Environment.NewLine);
        File.AppendAllText(path2, "- Distance angulaire parcourue par ballon : " + angle / PlayerPrefs.GetInt("Ballons") + "°" + System.Environment.NewLine + System.Environment.NewLine);

        File.AppendAllText(path2, "Scores d'exploration spatiale : " + System.Environment.NewLine);
        File.AppendAllText(path2, "- Score d'exploration spatiale lié à l'acuité visuelle (1,5°) : " + listScore[0] + "% de la zone utile." + System.Environment.NewLine);
        File.AppendAllText(path2, "- Score d'exploration spatiale lié à la vision de lecture (10°): " + listScore[1] + "% de la zone utile." + System.Environment.NewLine);
        File.AppendAllText(path2, "- Score d'exploration spatiale lié à la vision de reconnaissance de symbole (20°) : " + listScore[2] + "% de la zone utile." + System.Environment.NewLine + System.Environment.NewLine);

        File.AppendAllText(path2, System.Environment.NewLine + "Informations diverses sur la destruction des ballons : " + System.Environment.NewLine);

        int i = 1;
        float tamponTemps = 0f;

        listTemps = GetComponent<DestructionBallonV2>().ListTemps;
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
            File.AppendAllText(path2,debutMot + item + finMot + System.Environment.NewLine + alinea + erreurS + System.Environment.NewLine);
            // On redéfinit les variables pour la suite
            i += 1;
            tamponTemps = item;
        }

        // On clear les listes nouvellement crées après utilisation
        listScore.Clear();
        listTemps.Clear();
    }

    void SaveCamera(float timer)
    {
        // On note le vector3 qui donne le lieu du regard
        var Regard = this.transform.forward*30 + transform.position;
        // Un string qui donne le temps dans le jeu
        string timeMod = "#" + timer.ToString();
        // On note toutes les informations dans cette liste de string
        string[] content =
        {
            timeMod,
            Regard.ToString("G") + "%"
        };
        // On assemble les données dans un unique string
        string saveString = string.Join(saveSeparator, content) + System.Environment.NewLine;
        // On l'ajoute au fichier lié au path
        File.AppendAllText(path1, saveString);
    }

}
