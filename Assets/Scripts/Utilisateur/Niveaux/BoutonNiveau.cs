using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Création de l'interface pour le choix des niveaux

public class BoutonNiveau : MonoBehaviour
{
    // On récupère les informations
    public int Ballons;

    public void nbrBouton()
    {
        // On définit la variable elle sera transmise pour les prochaines scènes, c'est l'objectif
        PlayerPrefs.DeleteKey("Ballons");
        PlayerPrefs.SetInt("Ballons", Ballons);
        // On lance la scène principale de jeu
        SceneManager.LoadScene("SceneP");
    }

    public void MenuBut()
    {
        // On revient à la scène Menu
        SceneManager.LoadScene("Menu");
    }
}
