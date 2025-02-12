using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
    
    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("���� �Ϸ�");
    }

    public static GameData Load()
    {
        if(File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("�ε� �Ϸ�");
            return data;
        }
        else
        {
            Debug.LogWarning("���嵥���� ����");
            return null;
        }
    }

    public static void DeleteSave()
    {
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("���� ������ ����");
        }
    }
}
