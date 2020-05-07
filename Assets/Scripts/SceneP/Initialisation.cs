using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initialisation : MonoBehaviour
{

    public Text zoneText;

    // Start is called before the first frame update
    void Start()
    {
        zoneText.text = "Objectif : " + PlayerPrefs.GetInt("Ballons") + " Ballons";
    }
}
