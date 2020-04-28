using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationBallon : MonoBehaviour
{
    private float tempsEcoule;
    public GameObject ballon;

    // Start is called before the first frame update
    void Start()
    {
        tempsEcoule = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // On incrémente le temps
        tempsEcoule += Time.deltaTime;

        // Si le temps incrémenté atteint le seuil alors on crée un nouveau ballon
        if (tempsEcoule >= 1)
        {
            float module = Random.Range(5.0f, 30.0f);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);

            float x = module * Mathf.Cos(angle);
            float z = module * Mathf.Sin(angle);

            Vector3 coord = new Vector3(x, 1, z);
            Instantiate(ballon, coord, Quaternion.identity);

            // On réinitialise le temps
            tempsEcoule = 0f;
        }

    }
}
