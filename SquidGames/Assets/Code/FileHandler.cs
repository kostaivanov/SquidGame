using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public static class FileHandler
{
    public static void SaveToJSON<T>(List<T> toSave, string fileName)
    {
        Debug.Log(GetPath(fileName));
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFIle(GetPath(fileName), content);
    }

    public static List<T> ReadFromJSON<T>(string fileName)
    {
        string content = ReadFile(GetPath(fileName));

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }

        List<T> res = JsonHelper.FromJson<T>(content).ToList();
        return res;
    }

    private static string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    private static void WriteFIle(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    } 
}
