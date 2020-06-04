using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère le replay des rotations de la caméra

public class EnregistrementDevV2 : MonoBehaviour
{
    float savePeriod = 0.1f; // Le temps d'échantillonage
    float time = 0; // Timer

    void Start()
    {
        // On lance le replay
        SaveManagerDev.si.Load();
    }

    void Update()
    {
        time = this.GetComponent<InitialisationDev>().temps; // On actualise le temps 

        // On récupère l'enregistrement
        if (SaveManagerDev.si.SaveTransforms.Count > 1)
        {
            int ist = (int)(time / savePeriod);
            float t = (time / savePeriod) - ist;
            ist %= SaveManagerDev.si.SaveTransforms.Count;

            if (SaveManagerDev.si.SaveTransforms.Count > ist+1)
            // s'il y a encore des trucs dans l'enregistrement
            {
                SaveManagerDev.SaveTransform nextSt = SaveManagerDev.si.SaveTransforms[ist+1];
                SaveManagerDev.SaveTransform prevSt = SaveManagerDev.si.SaveTransforms[ist];
                transform.rotation = Quaternion.Lerp(prevSt.rot, nextSt.rot, t);
                // transform.position = Vector3.Lerp(prevSt.pos, nextSt.pos, t);
            }
            else
            // S'il n'y a plus rien dans l'enregistrement, on ne bouge plus la caméra
            {
                SaveManagerDev.SaveTransform nextSt = SaveManagerDev.si.SaveTransforms[ist];
                SaveManagerDev.SaveTransform prevSt = SaveManagerDev.si.SaveTransforms[ist];
                transform.rotation = Quaternion.Lerp(prevSt.rot, nextSt.rot, t);
            }
        }
    }
}
