using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonNiveau : MonoBehaviour
{
    public int Ballons;

    public void nbrBouton()
    {
        PlayerPrefs.DeleteKey("Ballons");
        PlayerPrefs.SetInt("Ballons", Ballons);
        // Debug.Log(PlayerPrefs.GetInt("Ballons"));
        SceneManager.LoadScene("SceneP");
    }
}
