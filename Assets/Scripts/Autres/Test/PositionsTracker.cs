using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script test

public class PositionsTracker : MonoBehaviour
{
    //float time = 0;
    //bool recording = true;
    //bool play = false;
    //float savePeriod = 0.1f;

    //void Start()
    //{
    //    SaveManager.si.Clear(true);
    //    SaveManager.si.SaveTransformRotPos(transform);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    time += Time.deltaTime;

    //    if (recording && time >= savePeriod)
    //    {
    //        time = 0;
    //        SaveManager.si.SaveTransformRotPos(transform);
    //    }

    //    if (play)
    //    {
            
    //        if(SaveManager.si.SaveTransforms.Count > 1)
    //        {
    //            int ist = (int)(time / savePeriod);
    //            float t = (time / savePeriod) - ist;
    //            ist %= SaveManager.si.SaveTransforms.Count;
                
    //            SaveManager.SaveTransform nextSt = SaveManager.si.SaveTransforms[ist+1];
    //            SaveManager.SaveTransform prevSt = SaveManager.si.SaveTransforms[ist];
    //            transform.rotation = Quaternion.Lerp(prevSt.rot, nextSt.rot, t);
    //            transform.position = Vector3.Lerp(prevSt.pos, nextSt.pos, t);
    //        }
    //    }
        

    //    if (Input.GetKey(KeyCode.C))
    //    {
    //        recording = false;
    //        play = true;
    //        time = 0;
    //        SaveManager.si.Load();
    //        GetComponent<Rigidbody>().isKinematic = true;
    //    }
    //}
}
