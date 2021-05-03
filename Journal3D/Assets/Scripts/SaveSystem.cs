using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem 
{
    public static void Save (Saveable ts)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save_text.jAR";
        FileStream fs = new FileStream(path, FileMode.Create);
        bf.Serialize(fs, ts);
        fs.Close();

    }

    public static Saveable Load()
    {
        string path = Application.persistentDataPath + "/save_text.jAR";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            Saveable ts = bf.Deserialize(fs) as Saveable;
            fs.Close();
            return ts;
        }
        else
        {
            return new Saveable();
        }

    }

    public static void ClearData()
    {
        string path = Application.persistentDataPath + "/save_text.jAR";
        File.Delete(path);

    }
}
