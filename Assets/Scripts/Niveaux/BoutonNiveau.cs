using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Création de l'interface pour le choix des niveaux

public class BoutonNiveau : MonoBehaviour
{
    public int Ballons;

    public void nbrBouton()
    {
        PlayerPrefs.DeleteKey("Ballons");
        PlayerPrefs.SetInt("Ballons", Ballons);
        SceneManager.LoadScene("SceneP");
    }
}
