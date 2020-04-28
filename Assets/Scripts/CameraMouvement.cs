using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMouvement : MonoBehaviour
{
	private float rotationX;
	private float rotationY;
	public float sensitivity = 3.0f;

	// public GameObject canvas;

	private void Start()
	{
		
	}

	private void Update()
	{
		rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
		rotationY += Input.GetAxis("Mouse X") * sensitivity;

		rotationX = Mathf.Clamp(rotationX, -90, 30);

		transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
	}

}
