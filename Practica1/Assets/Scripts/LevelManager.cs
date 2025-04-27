using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
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

}
