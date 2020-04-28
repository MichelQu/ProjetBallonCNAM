using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructionBallon : MonoBehaviour
{
    Camera cam;
    public int score;

    public Text zoneText;

    void Start()
    {
        cam = GetComponent<Camera>();
        score = 0;
        zoneText.text = "Votre Score est de : " + score;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // debug Ray
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (hit.transform.tag == "Ballon")
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("Destruction d'un ballon");
                    score += 1;

                    zoneText.text = "Votre Score est de : " + score;
                }
            }
        }

    }
}
