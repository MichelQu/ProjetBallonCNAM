using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        elevationBallon();
    }

    private void elevationBallon()
    {
        if (this.gameObject.name == "BallonRouge(Clone)")
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

        if (this.gameObject.name == "BallonDore(Clone)")
        {
            if (transform.position.y <= 25)
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
