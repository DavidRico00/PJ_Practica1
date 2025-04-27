using UnityEngine;
using UnityEngine.UI;

public class OpcionesScritp : MonoBehaviour
{    
    public Slider sliderMusica, sliderBrillo;
    public Image panelBrillo;
 
    void Start()
    {
        if(sliderMusica != null)
        {
            sliderMusica.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
            AudioListener.volume = sliderMusica.value;
        }
        
        float valor = PlayerPrefs.GetFloat("brillo", 0.5f);

        if(sliderBrillo != null)
            sliderBrillo.value = valor;

        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valor);
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