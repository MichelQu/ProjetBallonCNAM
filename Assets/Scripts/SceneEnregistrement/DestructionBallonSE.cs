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

    void Start()
    {
        cam = GetComponent<Camera>();
        score = 0;
        ballons = 0;
        tampon = 0;
        ListTemps = new List<float>();
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
                tampon += 1;
                Destruction();
            }
        }


    }

    void Destruction()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Ballon")
            {
                AudioClip son = hit.transform.gameObject.GetComponent<AudioSource>().clip;
                hit.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(son, 0.5f);

                // Destroy(hit.transform.gameObject);
                // Debug.Log("Destruction d'un ballon");

                hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;

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

                textScore.text = "Score : " + score;
                textBallon.text = "Ballons détruits : " + ballons;
            }
        }
    }

    void LoadDestructionBallon()
    {
        string[] textArray;
        ListTemps.Clear();
        string path = Application.dataPath + "/Texte/dataBallonDestruction.txt";

        // On récupère le fichier texte
        string readText = File.ReadAllText(path);
        // On le mets dans la liste
        textArray = readText.Split(new[] { "%" }, System.StringSplitOptions.None);

        // Debug.Log("Longueur : " + textArray.Length);

        for(int i=0; i<textArray.Length - 1; i++)
        {
            ListTemps.Add(float.Parse(textArray[i], CultureInfo.InvariantCulture));
        }

        // Debug.Log("Chargement Destruction Ballon");
    }
}
