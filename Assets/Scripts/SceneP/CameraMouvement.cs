using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ce script crée la rotation de la caméra en fonction du mouvement de la souris
// Lorsqu'on est en menu Resume sur la scène Principale

public class CameraMouvement : MonoBehaviour
{
	private float rotationX;
	private float rotationY;
	public float sensitivity = 2f;

	private void Update()
	{
        if (!Cursor.visible)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
		    rotationY += Input.GetAxis("Mouse X") * sensitivity;

		    rotationX = Mathf.Clamp(rotationX, -40, 25);

		    transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }

	}

}
