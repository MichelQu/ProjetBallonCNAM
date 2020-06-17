using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script gérant l'initialisation des UI : objectif, timer 
// Et lançant le chrono de la scène Enregistrement

public class InitialisationSE : MonoBehaviour
{
    // Déclaration des variables
    public Text objectif;
    public Text Timer;
    public float temps;
    private float tempsInt;

    // Start is called before the first frame update
    void Start()
    {
        // On définit les varibles ici
        temps = 0f;
        objectif.text = "Objectif : " + PlayerPrefs.GetInt("Ballons") + " Ballons";
        Timer.text = temps + "s";
    }

    private void Update()
    {
        // Selon les scènes en cours 
        if (SceneManager.GetActiveScene().name == "Enregistrement")
        {
            // On incrémente les temps avec le temps dans l'application
            temps += Time.deltaTime;
            tempsInt = Mathf.RoundToInt(temps);
            // On affiche le temps dans l'interface voulue
            Timer.text = tempsInt + "s";
        }
        if (SceneManager.GetActiveScene().name == "VisualisationLibre")
        {
            // Si ce n'est pas la fin de l'enregistrement
            if (PlayerPrefs.GetInt("VisualisationLibre") == 0)
            {
                // On incrémente les temps avec le temps dans l'application
                temps += Time.deltaTime;
                tempsInt = Mathf.RoundToInt(temps);
                // On affiche le temps dans l'interface voulue
                Timer.text = tempsInt + "s";
            }
        }
    }
}
