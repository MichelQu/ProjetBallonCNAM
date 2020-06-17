using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère la montée du ballon dans les airs
// Et il détruit le ballon s'il atteint une certaine hauteur

public class Ballon : MonoBehaviour
{
    // Déclaration des variables
    private float speed = 1f;
    public float norme;

    // Update is called once per frame
    void Update()
    {
        // On lance l'application à chaque frame
        elevationBallon();
    }

    private void elevationBallon()
    {
        // Si c'est un ballon rouge 
        if (this.gameObject.name == "BallonRouge(Clone)")
        {
            // Si le ballon n'est pas trop haut
            if (transform.position.y <= 10)
            {
                // On le fait monter dans l'espace
                transform.Translate(transform.up * speed * Time.deltaTime);
            }
            else
            {
                // S'il est trop haut, on le détruit
                Destroy(this.gameObject);
            }
        }

        // Si c'est un ballon dorée
        if (this.gameObject.name == "BallonDore(Clone)")
        {
            // Si le ballon n'est pas trop haut
            if (transform.position.y <= 10)
            {
                // On le fait monter dans l'espace
                transform.Translate(transform.up * 2 * speed * Time.deltaTime);
            }
            else
            {
                // S'il est trop haut, on le détruit
                Destroy(this.gameObject);
            }
        }

    }
}
