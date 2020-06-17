using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : SingleMonobehavior<SaveManager>
{
    private string fileName;
    public List<SaveTransform> SaveTransforms;


    // Classe SaveTransform : information de la rotation d'un go à un temps t
    public class SaveTransform
    {
        public float time;
        public Quaternion rot;
    }


    private void Start()
    {
        // Le lieu où l'on sauvegarde le fichier
        fileName = Application.persistentDataPath + Path.DirectorySeparatorChar + "dataOrientationCamera.txt";
        // On crée la liste des saveTransforms
        SaveTransforms = new List<SaveTransform>();
    }

    // Pour clear une liste
    public void Clear(bool clearFile)
    {
        SaveTransforms.Clear();
        if (clearFile)
        {
            Save();
        }
    }


    // Pour créer et sauvegarder un SaveTransform
    public void SaveTransformRotPos (Transform t, float time)
    {
        SaveTransform st = new SaveTransform();
        st.time = time;
        st.rot = t.rotation;
        SaveTransforms.Add(st);
        Save();
    }

    // Pour sauvegarder dans un fichier texte
    public void Save(bool append = false)
    {
        int i = 0;
        StreamWriter sw = new StreamWriter(fileName, append);
        foreach (SaveTransform t in SaveTransforms)
        {
            // On note le numéro de la sauvegarde, le temps et sa rotation
            sw.WriteLine("#" + i);
            sw.WriteLine(t.time);
            sw.WriteLine(t.rot.x);
            sw.WriteLine(t.rot.y);
            sw.WriteLine(t.rot.z);
            sw.WriteLine(t.rot.w);
            i += 1;
        }
        sw.Close();
    }

    public void Load()
    {
        StreamReader sw = new StreamReader(fileName);
        SaveTransforms.Clear();
        while (!sw.EndOfStream)
        {
            SaveTransform t = new SaveTransform();
            // On saute la ligne contenant le numéro de la sauvegarde.
            sw.ReadLine();
            // On récupère le temps associé à la sauvegarde
            float time = System.Single.Parse(sw.ReadLine());
            t.time = time;
            // On récupère la rotation associé à la sauvegarde
            float x = System.Single.Parse(sw.ReadLine());
            float y = System.Single.Parse(sw.ReadLine());
            float z = System.Single.Parse(sw.ReadLine());
            float w = System.Single.Parse(sw.ReadLine());
            t.rot = new Quaternion(x, y, z, w);

            SaveTransforms.Add(t);
        }
        sw.Close();
    }
}

