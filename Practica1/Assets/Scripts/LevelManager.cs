using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Text puntuacion, vida;
    private int score = 0, numVidas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void BotonNuevaPartida()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonSalir()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void BotonVolverMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PantallaFinal()
    {
        SceneManager.LoadScene(3);
    }

    public void modificarPuntuacion(int score)
    {
        this.score = score;
        puntuacion.text = score.ToString();
    }

    public void modificarVida(int v)
    {
        this.numVidas = v;
        vida.text = numVidas.ToString();
    }
}
