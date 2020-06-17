using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionBut : MonoBehaviour
{
    // On récupère les zones de texte
    public Text input;
    public Text bouton;
    // Booléen qui indique si on a rentré un numéro dans la zone de texte
    private bool entered = false;
    private bool choised = false;

    // Update is called once per frame
    void Update()
    {
        // Si on a rentré un numéro alors
        if (entered)
        {
            // On modifie les zones de texte et les variables
            bouton.text = "Aller vers la scène liée au dossier n°" + input.text;
            int num = int.Parse(input.text);
            PlayerPrefs.SetInt("NumDos", num);
            entered = false;
        }
    }

    public void enterBut()
    {
        // On modifie la variable
        entered = true;
        choised = true;
    }

    public void transiBut()
    {
        // On va vers la scène de transitionDev si un numéro a été rentré grâce à la varible choised
        if (choised)
        {
            SceneManager.LoadScene("TransitionDev");
        }
    }

    public void MenuBut()
    {
        // On va vers la scène de Menu
        SceneManager.LoadScene("Menu");
    }
}
