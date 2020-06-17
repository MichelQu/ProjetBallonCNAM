using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script gérant l'initialisation des UI : objectif, timer 
// Et lançant le chrono de la scèneP

public class Initialisation : MonoBehaviour
{
    // On récupère les objets nécessaires
    public Text zoneText;
    public Text Timer;
    // On instancie les variables voulues
    public float temps;
    private float tempsInt;

    // Start is called before the first frame update
    void Start()
    {
        // On définit les varibles ici
        temps = 0f;
        zoneText.text = "Objectif : " + PlayerPrefs.GetInt("Ballons") + " Ballons";
        Timer.text = temps + "s";
        // Une variable permettant de décider s'il l'on doit réaliser une variable profonde
        PlayerPrefs.SetInt("AEnregitrerP", 1);
    }

    private void Update()
    {
        // On incrémente les temps avec le temps dans l'application
        temps += Time.deltaTime;
        tempsInt = Mathf.RoundToInt(temps);
        // On affiche le temps dans l'interface voulue
        Timer.text = tempsInt + "s";
    }
}
