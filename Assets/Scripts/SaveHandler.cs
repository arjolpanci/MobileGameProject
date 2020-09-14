
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveHandler
{

    public static void SaveData(GameController gameController){
        
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedata.dat";
        FileStream file = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData(gameController);
        binaryFormatter.Serialize(file, saveData);
        file.Close();
    }

    public static void SaveGold(GameController gameController){
        
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gdata.dat";
        FileStream file = new FileStream(path, FileMode.Create);

        GoldData saveData = new GoldData(gameController);
        binaryFormatter.Serialize(file, saveData);
        file.Close();
    }


    public static SaveData LoadData(){
        
        string path = Application.persistentDataPath + "/savedata.dat";
        
        if(File.Exists(path)){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            SaveData data = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();
            return data;
        }else{
            Debug.Log("No save file has been created");
            return null;
        }
    }
    
    public static GoldData LoadGoldData(){
        
        string path = Application.persistentDataPath + "/gdata.dat";
        
        if(File.Exists(path)){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            GoldData data = (GoldData)binaryFormatter.Deserialize(file);
            file.Close();
            return data;
        }else{
            Debug.Log("No save file has been created");
            return null;
        }
    }

}
