using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Ce script gère la destruction des ballons (sons), les UI l'indiquant (score)
// Le fichier regroupant la sauvegarde de la position du ballon détruit
// Et la transition lors de la fin d'un niveau

public class DestructionBallon : MonoBehaviour
{
    Camera cam;
    public int score = 0;
    private int ballons = 0;

    public Text zoneText1;
    public Text zoneText2;

    public AudioClip sonBallon;

    void Start()
    {
        // On initialise les données 
        cam = GetComponent<Camera>();
        zoneText1.text = "Score : " + score;
        zoneText2.text = "Ballons détruits : " + ballons;

        // Initialisation du fichier qui va regrouper la sauvegarde des ballons détruits
        string path = Application.dataPath + "/Texte/dataBallonDestruction.txt";
        File.WriteAllText(path, "Liste regroupant le temps, le nom et la position du ballon détruit" + System.Environment.NewLine);
    }

    // Update is called once per frame
    void Update()
    {
        // On crée un laser qui part du milieu de l'écran et qui va tirer un laser tout droit
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        // Si le rayon détecte un objet
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            // Si on appuie sur la barre espace ou le clic droit de la souris
            {
                if (hit.transform.tag == "Ballon")
                {
                    // On lance l'audio de destruction d'un ballon
                    AudioClip son = hit.transform.gameObject.GetComponent<AudioSource>().clip;
                    hit.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(son, 0.5f);
                    hit.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(sonBallon, 0.7f);

                    // On désactive son mesh renderer pour ne plus pouvoir interagir avec. On est obligé de faire ça pour pouvoir garder l'audio qui est joué juste au-dessus
                    // Un autre script s'occupe de détruire définitivement l'objet.
                    hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;

                    // On sauvegarde les données liées à la destruction du ballon
                    savedate(hit);

                    // On différencie les cas d'un ballon rouge ou doré pour le score
                    if (hit.transform.gameObject.name == "BallonRouge(Clone)")
                    {
                        ballons += 1;
                        score += 1;
                    }
                    if (hit.transform.gameObject.name == "BallonDore(Clone)")
                    {
                        ballons += 1;
                        score += 3;
                    }

                    // On actualise les UI pour le score
                    zoneText1.text = "Score : " + score;
                    zoneText2.text = "Ballons détruits : " + ballons;
                }
            }
        }

        // Test permettant de voir si l'objectif du niveau est atteint
        // Si oui, on change de scène
        if (PlayerPrefs.GetInt("Ballons") == ballons)
        {
            // Debug.Log("Vous avez réussi le niveau");
            SceneManager.LoadScene("Transition");
        }
    }

    // Fonction qui permet de sauvegarder les données liées aux ballons détruits 
    void savedate(RaycastHit hit)
    {
        string path = Application.dataPath + "/Texte/dataBallonDestruction.txt"; // Le lieu de sauvegarde

        string temps = "#" + cam.GetComponent<Initialisation>().temps.ToString("G"); 
        string pos = hit.transform.position.ToString("G");
        string nom = hit.transform.gameObject.name;

        string[] content =
        {
            temps,
            nom,
            pos
        };

        string saveString = string.Join("%", content) + System.Environment.NewLine; // On joint les différents éléments de content avec le saveString pour créer un unique string

        File.AppendAllText(path, saveString); // on l'ajoute au fichier lié au path
    }

}
