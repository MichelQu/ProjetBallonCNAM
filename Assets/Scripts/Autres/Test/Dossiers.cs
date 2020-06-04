using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Dossiers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/Texte/profond/Donnée";
        Directory.CreateDirectory(path);
        Debug.Log("Création");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
