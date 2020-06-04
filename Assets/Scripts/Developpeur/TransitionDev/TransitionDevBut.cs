using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionDevBut : MonoBehaviour
{
    public Text zoneText;

    // Start is called before the first frame update
    void Start()
    {
        int num = PlayerPrefs.GetInt("NumDos");
        zoneText.text = "Dossier n°" + num + " choisi";
    }

    public void But1()
    {
        SceneManager.LoadScene("EnregistrementDev");
    }

    public void But2()
    {
        SceneManager.LoadScene("VisualisationDev");
    }

    public void But3()
    {
        SceneManager.LoadScene("VisualisationLibreDev");
    }

    public void But4()
    {
        SceneManager.LoadScene("Selection");
    }

    public void But5()
    {
        SceneManager.LoadScene("Menu");
    }

    public void But6()
    {
        Application.Quit();
    }
}
