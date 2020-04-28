using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        elevationBallon();
    }

    private void elevationBallon()
    {
        if (transform.position.y <= 25)
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
