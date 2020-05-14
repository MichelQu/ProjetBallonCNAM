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
        string saveString = "Résultat expérience" + System.Environment.NewLine;
        string path = Application.dataPath + "/Texte/dataBrut.txt";
        File.WriteAllText(path, saveString);
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
            Save();
            temps = 0f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Enregistrement");
            // Debug.Log("Début de l'enregistrement");
        }

    }

    void Save()
    {
        var Regard = this.transform.rotation;
        string path = Application.dataPath + "/Texte/dataBrut.txt";

        string[] content =
        {
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

}
