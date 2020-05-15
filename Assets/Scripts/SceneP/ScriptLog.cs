using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Globalization;
using UnityEngine.SceneManagement;

public class ScriptLog : MonoBehaviour
{

    private float temps;
    private bool saving;
    private float echanti = 0.001f;

    private string saveSeparator = "%";

    // Start is called before the first frame update
    void Start()
    {
        saving = false;
        // Data Orientation caméra
        string saveString = "Résultat expérience Orientation de la caméra (Quaternion)" + System.Environment.NewLine;
        string path = Application.dataPath + "/Texte/dataBrut.txt";
        File.WriteAllText(path, saveString);

        // Data Direction Caméra
        path = Application.dataPath + "/Texte/dataBrutCamera.txt";
        File.WriteAllText(path, "");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            saving = !saving;
        }

        temps += Time.deltaTime;
        if (temps > echanti) //&& saving)
        {
            SaveData();
            SaveCamera();
            temps = 0f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Enregistrement");
            // Debug.Log("Début de l'enregistrement");
        }

    }

    void SaveData()
    {
        var Regard = this.transform.rotation;
        // var Regard = this.transform.forward;

        string path = Application.dataPath + "/Texte/dataBrut.txt";

        float timer1 = this.GetComponent<Initialisation>().temps; // Le temps dans le jeu
        string timer2 = "#" + timer1.ToString();

        string[] content =
        {
            timer2,
            Regard.ToString("G")
        };

        string saveString = string.Join(saveSeparator, content) + System.Environment.NewLine;
        

        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            File.WriteAllText(path, saveString);
        }
        else
        {
            File.AppendAllText(path, saveString);
        }

        // Debug.Log("Sauvegarde effectuée");
    }

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
