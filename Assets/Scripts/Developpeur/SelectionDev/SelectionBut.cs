using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionBut : MonoBehaviour
{

    public Text input;
    public Text bouton;

    private bool entered = false;

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            bouton.text = "Aller vers la scène liée au dossier n°" + input.text;
            int num = int.Parse(input.text);
            PlayerPrefs.SetInt("NumDos", num);
            entered = false;
        }
    }

    public void enterBut()
    {
        entered = true;
    }

    public void transiBut()
    {
        SceneManager.LoadScene("TransitionDev");
    }
}
