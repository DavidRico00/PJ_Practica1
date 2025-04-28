using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Transformacion_Mapa : MonoBehaviour
{
    public GameObject jugador;
    public Transform puntoIni;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            puntoIni = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
            jugador.transform.position = puntoIni.position;

            jugador.GetComponent<PlayerScript>().SumarPuntos(0);
            Debug.Log("Jugador movido al SpawnPoint al inicio.");
        }
        else
        {
            Debug.Log("No se encontr� el objeto Player al inicio.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)   SceneManager.LoadScene(2);
            else if (SceneManager.GetActiveScene().buildIndex == 2) SceneManager.LoadScene(1);
        }
    }
}
