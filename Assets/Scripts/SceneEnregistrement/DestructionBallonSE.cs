using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

public class DestructionBallonSE : MonoBehaviour
{
    Camera cam;
    private int score;
    private int ballons;
    private int tampon;

    public Text textScore;
    public Text textBallon;

    private List<float> ListTemps;
    private List<Vector3> ListPos;
    // private List<string> ListBallon;


    void Start()
    {
        cam = GetComponent<Camera>();
        // Initialisation des variables
        score = 0;
        ballons = 0;
        tampon = 0;
        ListTemps = new List<float>();
        ListPos = new List<Vector3>();
        // ListBallon = new List<string>();

        // Initialisation des UI
        textScore.text = "Score : " + score;
        textBallon.text = "Ballons détruits : " + ballons;

        // Récupération des informations du fichier texte
        LoadDestructionBallon();
    }

    // Update is called once per frame
    void Update()
    {
        float temps = cam.GetComponent<InitialisationSE>().temps;
        

        if (tampon < ListTemps.Count)
        {
            if ( Mathf.Abs(temps-ListTemps[tampon]) < 0.01f)
            {
                Debug.Log("Correspondance Temps pour destruction");
                // On itendifie tous les ballons dans la scène
                GameObject[] listGO = GameObject.FindGameObjectsWithTag("Ballon");
                // On lance la fonction pour en détruire un
                Destruction(listGO);
                // On incrémente la variable tampon pour passer au prochain ballon à détruire
                tampon += 1;
            }
        }
    }

    void Destruction(GameObject[] listGO)
    {
        // On identifie le ballon qui va être détruit avec sa coordonnée Z
        // TODO et le nom peut être dans un plus large cas
        // GameObject target;

        foreach (GameObject item in listGO)
        {
            if ( Mathf.Abs(item.transform.position.z - ListPos[tampon][2]) < 0.01f)
            {
                Debug.Log("Ballon trouvé");

                GameObject cible = item;

                // On a trouvé le ballon a détruire
                // Différentiation des cas si ballon rouge ou ballon dorée

                // On joue le son de disparition
                AudioClip son = cible.transform.gameObject.GetComponent<AudioSource>().clip;
                cible.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(son, 0.5f);
                // On le fait disparaitre
                cible.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;

                if (cible.name == "BallonRouge(Clone)")
                {
                    score += 1;
                    ballons += 1;
                }

                if (cible.name == "BallonDore(Clone)")
                {
                    ballons += 1;
                    score += 3;
                }

                // Actualisation des variables UI
                textScore.text = "Score : " + score;
                textBallon.text = "Ballons détruits : " + ballons;

            }
        }

    
        #region Ancien Code
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (hit.transform.tag == "Ballon")
        //    {
        //        AudioClip son = hit.transform.gameObject.GetComponent<AudioSource>().clip;
        //        hit.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(son, 0.5f);

        //        // Destroy(hit.transform.gameObject);
        //        // Debug.Log("Destruction d'un ballon");

        //        hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;

        //        if (hit.transform.gameObject.name == "BallonRouge(Clone)")
        //        {
        //            ballons += 1;
        //            score += 1;
        //        }
        //        if (hit.transform.gameObject.name == "BallonDore(Clone)")
        //        {
        //            ballons += 1;
        //            score += 3;
        //        }

        //        textScore.text = "Score : " + score;
        //        textBallon.text = "Ballons détruits : " + ballons;
        //    }
        //}
        #endregion
    }

    void LoadDestructionBallon()
    {
        string[] textArray;
        ListTemps.Clear();
        string path = Application.dataPath + "/Texte/dataBallonDestruction.txt";

        // On récupère le fichier texte
        string readText = File.ReadAllText(path);
        // On le traite un peu
        readText = readText.Replace("Liste regroupant le temps, le nom et la position du ballon détruit" + System.Environment.NewLine, "");
        readText = readText.Replace("BallonRouge(Clone)%", "");
        readText = readText.Replace("BallonDore(Clone)%", "");
        readText = readText.Replace("#", "");
        readText = readText.Replace("(", "");
        readText = readText.Replace(", ", "%");
        readText = readText.Replace(")", "%");
        readText = readText.Replace(System.Environment.NewLine, "");

        File.WriteAllText(Application.dataPath + "/Texte/dataBallonDestruction2.txt", readText);

        // On le mets dans la liste
        textArray = readText.Split(new[] { "%" }, System.StringSplitOptions.None);

        // Debug.Log("Longueur : " + textArray.Length);

        for(int i=0; i<textArray.Length/4; i++)
        {
            ListTemps.Add(float.Parse(textArray[4*i], CultureInfo.InvariantCulture));
            // ListBallon.Add(textArray[5*i+1]);
            Vector3 kwa = new Vector3(float.Parse(textArray[i * 4 + 1], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 2], CultureInfo.InvariantCulture), float.Parse(textArray[i * 4 + 3], CultureInfo.InvariantCulture));
            ListPos.Add(kwa);
        }

        // Debug.Log("Chargement Destruction Ballon");
    }
}
