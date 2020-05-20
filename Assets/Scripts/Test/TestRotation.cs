using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{

    public GameObject objet1;
    public GameObject objet2;

    void Start()
    {
    }

    void Update()
    {
        Vector3 pos1 = objet1.transform.position;
        Vector3 pos2 = objet2.transform.position;

        Vector3 dir1 = (pos1 - transform.position).normalized;
        Vector3 dir2 = (pos2 - transform.position).normalized;

        float scalar = (dir1.x * dir2.x) + (dir1.y * dir2.y) + (dir1.z * dir2.z);

        float angle = Mathf.Acos(scalar);
        Debug.Log("Numéro 1 : " + angle + "degrés");

        angle = Vector3.Angle(dir1, dir2);
        Debug.Log("Numéro 2 : " + angle + "degrés");

    }
}
