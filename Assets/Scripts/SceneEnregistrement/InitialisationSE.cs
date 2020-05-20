using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Script gérant l'initialisation des UI : objectif, timer 
// Et lançant le chrono de la scène Enregistrement

public class InitialisationSE : MonoBehaviour
{
    public Text objectif;
    public Text Timer;
    public float temps;
    private float tempsInt;

    // Start is called before the first frame update
    void Start()
    {
        temps = 0f;
        objectif.text = "Objectif : " + PlayerPrefs.GetInt("Ballons") + " Ballons";
        Timer.text = temps + "s";
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            temps += Time.deltaTime;
            tempsInt = Mathf.RoundToInt(temps);
            Timer.text = tempsInt + "s";
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (PlayerPrefs.GetInt("VisualisationLibre") == 0)
            {
                temps += Time.deltaTime;
                tempsInt = Mathf.RoundToInt(temps);
                Timer.text = tempsInt + "s";
            }
        }
    }
}
