using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        temps += Time.deltaTime;
        tempsInt = Mathf.RoundToInt(temps);
        Timer.text = tempsInt + "s";
    }
}
