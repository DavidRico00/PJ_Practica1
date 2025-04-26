using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class PlayerRecord
{
    public string playerName;        // Nombre del jugador
    public int score;                // Puntos totales
    public string date;              // Fecha de la partida
}

public class ControladorGlobalPuntación : MonoBehaviour
{

    private string filePath; // Ruta del archivo donde se guardarán los registros

    private List<PlayerRecord> allRecords; // Lista de registros de jugadores

    void Start()
    {

        filePath = Directory.GetParent(Application.dataPath).ToString() + "/Datos/playerRecords.json"; 
        
        LoadRecords(); 
    }

    public void AddPlayerRecord(string playerName, int score)
    {
        PlayerRecord newRecord = new PlayerRecord();
        newRecord.playerName = playerName;
        newRecord.score = score;
        newRecord.date = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        if(allRecords.Count < 10) allRecords.Add(newRecord);
        else
        {
            PlayerRecord pl = allRecords[0];
            
            foreach (PlayerRecord record in allRecords)
            {
                if (record.score < pl.score)
                {
                    pl = record;
                }
            }


            if (newRecord.score > pl.score)
            {
                allRecords.Remove(pl);
                allRecords.Add(newRecord);
            }
        }

        allRecords.Sort((a, b) => b.score.CompareTo(a.score));

        SaveRecords(); 
    }

    public void LoadRecords()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            allRecords = JsonConvert.DeserializeObject<List<PlayerRecord>>(json);
        }
        else
        {
            allRecords = new List<PlayerRecord>(); // Inicia la lista vacía si no existe el archivo
        }
    }

    public void SaveRecords()
    {
            string json = JsonConvert.SerializeObject(allRecords, Formatting.Indented);
            File.WriteAllText(filePath, json);
    }

}
