using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BallonDore : MonoBehaviour
{
    Camera cam;
    private float tempo = 0f;
    private string tagTempo;
    private bool save;      

    // Start is called before the first frame update
    void Start()
    {
        // On initialise les données 
        cam = GetComponent<Camera>();
        // Initialisation du fichier qui va regrouper la sauvegarde des ballons détruits
        // string path = Application.dataPath + "/Texte/dataFixationBallon.txt";
        // File.WriteAllText(path, "Liste des temporisations sur les ballons dorés :" + System.Environment.NewLine);
    }

    // Update is called once per frame
    void Update()
    {
        // On crée un laser qui part du milieu de l'écran et qui va tirer un laser tout droit
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            // Si on détecte un ballon Test
            if(hit.transform.tag == "BallonTest")
            {
                tempo += Time.deltaTime;
                save = true;
            }
            // Si on ne détecte pas un ballonTest
            else
            {
                tempo = 0f;
            }
        }
        // Si on ne détecte pas d'objet (en sorti de BallonTest ici avec la 2e condition)
        else
        {
            if (save)
            {
                // On note dans une liste
                if (tempo > 0.1f)
                {
                    // Pour retirer les erreurs
                    SaveTemporisation(tempo);
                }
                save = false;
            }
            // On réinitialise et on change la tempo
            tempo = 0f;
        }
    }

    void SaveTemporisation(float temporisation)
    {
        string path = Application.dataPath + "/Texte/dataFixationBallon.txt"; // Le lieu de sauvegarde

        string[] content =
        {
            temporisation.ToString()
        };

        string saveString = string.Join("%", content) + System.Environment.NewLine; // On joint les différents éléments de content avec le saveString pour créer un unique string

        File.AppendAllText(path, saveString); // on l'ajoute au fichier lié au path
    }
}
