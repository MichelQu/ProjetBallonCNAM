using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    // On crée les variables utiles dans la suite
    public List<GameObject> listGO;
    public int intersection;

    // Start is called before the first frame update
    void Start()
    {
        listGO = new List<GameObject>();
        intersection = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Totem")
        {
            intersection += 1;
            listGO.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Totem")
        {
            intersection -= 1;
            listGO.Remove(other.gameObject);
        }
    }
}
