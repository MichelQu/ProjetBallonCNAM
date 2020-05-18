using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMonobehavior<T> : MonoBehaviour where T : MonoBehaviour {

    private static T instance = null;

    public static T si
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
            }
            return instance;
        }
    }

}
