using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DestructionBallon : MonoBehaviour
{
    Camera cam;
    public int score;
    public int ballons;

    public Text zoneText1;
    public Text zoneText2;

    void Start()
    {
        cam = GetComponent<Camera>();
        score = 0;
        ballons = 0;
        zoneText1.text = "Score : " + score;
        zoneText2.text = "Ballons détruits : " + ballons;

        // Initialisation texte
        string path = Application.dataPath + "/Texte/dataBallonDestruction.txt";
        File.WriteAllText(path, "Liste regroupant le temps, le nom et la position du ballon détruit" + System.Environment.NewLine);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // debug Ray
        // Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (hit.transform.tag == "Ballon")
                {
                    AudioClip son = hit.transform.gameObject.GetComponent<AudioSource>().clip;
                    hit.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(son, 0.5f);

                    // Destroy(hit.transform.gameObject);
                    // Debug.Log("Destruction d'un ballon");

                    hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    savedate(hit);

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

                    zoneText1.text = "Score : " + score;
                    zoneText2.text = "Ballons détruits : " + ballons;
                }
            }
        }


        if (PlayerPrefs.GetInt("Ballons") == ballons)
        {
            Debug.Log("Vous avez réussi le niveau");
            SceneManager.LoadScene("Transition");
        }
    }


    void savedate(RaycastHit hit)
    {
        string path = Application.dataPath + "/Texte/dataBallonDestruction.txt";

        string temps = "#" + cam.GetComponent<Initialisation>().temps.ToString("G");
        string pos = hit.transform.position.ToString("G");
        string nom = hit.transform.gameObject.name;

        string[] content =
        {
            temps,
            nom,
            pos
        };

        string saveString = string.Join("%", content) + System.Environment.NewLine;

        File.AppendAllText(path, saveString);

        // Enregistrement Destruction
        Debug.Log("Destruction");
    }

}
