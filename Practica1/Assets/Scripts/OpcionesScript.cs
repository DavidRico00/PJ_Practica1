using UnityEngine;
using UnityEngine.UI;

public class OpcionesScritp : MonoBehaviour
{    
    public Slider sliderMusica, sliderBrillo;
    public Image panelBrillo;
 
    void Start()
    {
        sliderMusica.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = sliderMusica.value;

        sliderBrillo.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, sliderBrillo.value);
    }

    public void modificarSlider(float valor)
    {
        PlayerPrefs.SetFloat("volumenAudio", valor);
        AudioListener.volume = valor;
    }

    public void modificarBrillo(float valor)
    {
        PlayerPrefs.SetFloat("brillo", valor);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valor);
    }

}