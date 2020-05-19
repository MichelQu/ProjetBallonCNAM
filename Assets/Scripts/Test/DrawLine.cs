using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public GameObject objet1;
    public GameObject objet2;

    LineRenderer LineRend;

    // Start is called before the first frame update
    void Start()
    {
        LineRend = GetComponent<LineRenderer>();
        LineRend.positionCount = 3;

        LineRend.SetPosition(0, objet1.transform.position);
        LineRend.SetPosition(1, objet2.transform.position);
        LineRend.SetPosition(2, new Vector3(10,5,0));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
