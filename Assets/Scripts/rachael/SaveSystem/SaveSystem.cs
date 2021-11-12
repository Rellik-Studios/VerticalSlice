using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerSave player, bool _isSafeRoom = false)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "";
        if (!_isSafeRoom)
            path = Application.persistentDataPath + "/player.default";
        else
            path = Application.persistentDataPath + "/player.safeRoom";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    

    
    public static PlayerData LoadPlayer(bool _isSafeRoom = false)
    {
        
        string path = Application.persistentDataPath + (_isSafeRoom ? "/player.safeRoom" : "/player.default");

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
}