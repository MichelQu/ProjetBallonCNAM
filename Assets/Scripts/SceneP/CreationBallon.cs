using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationBallon : MonoBehaviour
{
    private float tempsEcoule1;
    private float tempsEcoule2;
    public GameObject ballon1;
    public GameObject ballon2;

    // Start is called before the first frame update
    void Start()
    {
        tempsEcoule1 = 0f;
        tempsEcoule2 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // On incrémente le temps
        tempsEcoule1 += Time.deltaTime;
        tempsEcoule2 += Time.deltaTime;

        // Si le temps incrémenté atteint le seuil alors on crée un nouveau ballon1
        if (tempsEcoule1 >= 1)
        {
            float module = Random.Range(5.0f, 30.0f);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);

            float x = module * Mathf.Cos(angle);
            float z = module * Mathf.Sin(angle);

            Vector3 coord = new Vector3(x, 1, z);
            Instantiate(ballon1, coord, Quaternion.identity);

            // On réinitialise le temps
            tempsEcoule1 = 0f;
        }

        // Si le temps incrémenté atteint le seuil alors on crée un nouveau ballon2
        if (tempsEcoule2 >= 5)
        {
            float module = Random.Range(5.0f, 30.0f);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);

            float x = module * Mathf.Cos(angle);
            float z = module * Mathf.Sin(angle);

            Vector3 coord = new Vector3(x, 1, z);
            Instantiate(ballon2, coord, Quaternion.identity);

            // On réinitialise le temps
            tempsEcoule2 = 0f;
        }

    }
}
