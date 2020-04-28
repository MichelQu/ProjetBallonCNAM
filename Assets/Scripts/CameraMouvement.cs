using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

		    rotationX = Mathf.Clamp(rotationX, -90, 30);

		    transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        }

	}

}
