using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.UI;

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

    public Text puntuacion, nombreJugador;

    void Start()
    {

        filePath = Directory.GetParent(Application.dataPath).ToString() + "/Datos/playerRecords.json"; 
        
        LoadRecords();

        if(puntuacion != null)
        {
            string texto = $"{"#", -6} {"Nombre",-15} {"Puntos",7}\n";
            for (int i = 0; i < allRecords.Count; i++)
            {
                texto += $"{i + 1,-6} {allRecords[i].playerName, -15} {allRecords[i].score,7}\n";
            }
            
            puntuacion.text = texto;
        }
    }

    public void AddPlayerRecord(string playerName, int score)
    {
        PlayerRecord newRecord = new PlayerRecord();
        newRecord.playerName = playerName;
        newRecord.score = score;

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
            allRecords = new List<PlayerRecord>();
        }
    }

    public void SaveRecords()
    {
            string json = JsonConvert.SerializeObject(allRecords, Formatting.Indented);
            File.WriteAllText(filePath, json);
    }

     public void GuardarNombre()
    {
        string rutaDirectorio = Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "Datos");
        string rutaArchivo = Path.Combine(rutaDirectorio, "nombre.txt");

        if(nombreJugador.text == "" || nombreJugador.text == null)
        {
            File.WriteAllText(rutaArchivo, "Jugador");
        }
        else
        {
            File.WriteAllText(rutaArchivo, nombreJugador.text);
        }
    }

    public string getNombreJugador()
    {
        string rutaDirectorio = Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "Datos");
        string rutaArchivo = Path.Combine(rutaDirectorio, "nombre.txt");

        if (File.Exists(rutaArchivo))
            return File.ReadAllText(rutaArchivo);
        else
            return "Jugador";
    }

}
