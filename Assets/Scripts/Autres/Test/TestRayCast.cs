using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{
    public GameObject Cube;

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100);

        Debug.DrawRay(transform.position, transform.forward*100, Color.red);

        // Debug.Log("Il y a " + hits.Length + " objet(s) touché(s)");

        Transform Tcube = Cube.transform;
        Tcube.position = transform.forward * 30 + transform.position;
        // Instantiate(Cube, Tcube);

        // Debug.Log(transform.forward * 30);

        foreach (var item in hits)
        {
            // Debug.Log(item.transform.name);
        }
    }
}
