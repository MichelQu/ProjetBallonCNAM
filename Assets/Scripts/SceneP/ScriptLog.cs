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

    string saveSeparator = "%";
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        time = this.GetComponent<Initialisation>().temps;

        // Initialisation des Data de l'Orientation de la Caméra
        SaveManager.si.Clear(true);
        SaveManager.si.SaveTransformRotPos(transform, time);

        // Initialisation des Data de la Direction Caméra
        string path2 = Application.dataPath + "/Texte/dataVisualisationCamera.txt";
        File.WriteAllText(path2, "Visualisation de la direction de la Caméra : " + System.Environment.NewLine);
    }

    // Update is called once per frame
    void Update()
    {
        // Récupération du temps
        time = this.GetComponent<Initialisation>().temps;

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
