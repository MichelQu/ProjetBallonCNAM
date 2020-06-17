using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script permet de récupérer le gyrospoce du téléphone et de l'utiliser pour l'orientation
// dnas la scène.

public class Gyros : MonoBehaviour
{
    // Déclaration des variables
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        // On crée un nouvel gameObject au lieu de la caméra et on lui lie en fils la caméra.
        cameraContainer = new GameObject("CameraContainer");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        // On vérifie si on détecte un gyroscope
        gyroEnabled = enableGyro();
    }

    // Update is called once per frame
    void Update()
    {
        if( gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }

    private bool enableGyro()
    {
        // On récupère les infos sur le gyrospoce du téléphone
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            // On configure pour pouvoir avoir une vision direct
            cameraContainer.transform.rotation = Quaternion.Euler(90f,90f,0f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }
}
