using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère la montée du ballon dans les airs
// Et il détruit le ballon s'il atteint une certaine hauteur

public class Ballon : MonoBehaviour
{
    private float speed = 1f;
    public float norme;

    // Update is called once per frame
    void Update()
    {
        elevationBallon();
    }

    private void elevationBallon()
    {
        if (this.gameObject.name == "BallonRouge(Clone)")
        {
            if (transform.position.y <= 10)
            {
                transform.Translate(transform.up * speed * Time.deltaTime);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        if (this.gameObject.name == "BallonDore(Clone)")
        {
            if (transform.position.y <= 10)
            {
                transform.Translate(transform.up * 2 * speed * Time.deltaTime);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }
}
