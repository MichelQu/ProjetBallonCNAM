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
        string path2 = Application.dataPath + "/Texte/dataBrutCamera.txt";
        File.WriteAllText(path2, "");
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
        }

    }

    // TODO
    // À Rajouter

    void SaveCamera()
    {
        var Regard = this.transform.forward*30 + transform.position;
        string path = Application.dataPath + "/Texte/dataBrutCamera.txt";
        string[] content =
        {
            Regard.ToString("G")
        };

        string saveString = string.Join(saveSeparator, content) + System.Environment.NewLine;
        File.AppendAllText(path, saveString);

        // Debug.Log("Sauvegarde Caméra");
    }

}
