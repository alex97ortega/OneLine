using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// este script crea un archivo donde se van a guardar los datos del progreso 
public class LoadManager : MonoBehaviour {

    public string directoryPath = "/Saves/";
    public string fileName = "data.dat";
    private string path;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if UNITY_EDITOR_WIN
        directoryPath = Application.dataPath + directoryPath;
#else
        directoryPath = Application.persistentDataPath + directoryPath;
#endif
        path = directoryPath + fileName;
        // si no existe creamos el directorio
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
    }
    // guardar el progreso. lo haremos cada vez que se complete un nivel o desafío, 
    // o se añadan o quiten monedas
    public void SaveItems(uint coins, uint challenges, uint[] lvls)
    {
        //Open a stream into the save File
        FileStream serializationStream = new FileStream(path, FileMode.Create);
        // serialize
        new BinaryFormatter().Serialize(serializationStream, new Data(coins, challenges, lvls));
        //Close the stream
        serializationStream.Close();
    }
    // cargar el progreso, se hará siempre en la pantalla de inicio (intro)
    public Data LoadItems()
    {
        //First time playing
        if (!File.Exists(path)) return InitialValues();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        Data data = (Data)bf.Deserialize(file);
        file.Close();
        return data;
    }

    // Datos a guardar:
    // - Progreso de los niveles (siguiente nivel a pasar de la dificultad que corresponda)
    // - Número de monedas
    // - Número de desafíos logrados
    // - Hora en la que se completó el último desafío

    [Serializable]
    public class Data
    {
        public uint coins;
        public uint challenges;
        public uint[] nextLevels;

        public Data(uint cns, uint challs, uint[] lvls)
        {
            coins = cns;
            challenges = challs;
            nextLevels = (uint[])lvls.Clone();
        }
    }
    public static Data InitialValues()
    {
        uint[] lvls = new uint[5];
        for (int i = 0; i < lvls.Length; i++)
            lvls[i] = 1;        

        Data data = new Data(0, 0, lvls);
        return data;
    }
}
