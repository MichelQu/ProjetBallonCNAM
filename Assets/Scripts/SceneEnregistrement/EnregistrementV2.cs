using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script gère le replay des rotations de la caméra

public class EnregistrementV2 : MonoBehaviour
{
    float savePeriod = 0.1f; // Le temps d'échantillonage
    float time = 0; // Timer

    void Start()
    {
        // On lance le replay
        SaveManager.si.Load();
    }

    void Update()
    {
        time = this.GetComponent<InitialisationSE>().temps; // On actualise le temps 

        // On récupère l'enregistrement
        if (SaveManager.si.SaveTransforms.Count > 1)
        {
            int ist = (int)(time / savePeriod);
            float t = (time / savePeriod) - ist;
            ist %= SaveManager.si.SaveTransforms.Count;

            if (SaveManager.si.SaveTransforms[ist + 1] != null)
            // s'il y a encore des trucs dans l'enregistrement
            {
                SaveManager.SaveTransform nextSt = SaveManager.si.SaveTransforms[ist+1];
                SaveManager.SaveTransform prevSt = SaveManager.si.SaveTransforms[ist];
                transform.rotation = Quaternion.Lerp(prevSt.rot, nextSt.rot, t);
                // transform.position = Vector3.Lerp(prevSt.pos, nextSt.pos, t);
            }
            else
            // S'il n'y a plus rien dans l'enregistrement, on ne bouge plus la caméra
            {
                SaveManager.SaveTransform nextSt = SaveManager.si.SaveTransforms[ist];
                SaveManager.SaveTransform prevSt = SaveManager.si.SaveTransforms[ist];
                transform.rotation = Quaternion.Lerp(prevSt.rot, nextSt.rot, t);
            }
        }
    }
}
