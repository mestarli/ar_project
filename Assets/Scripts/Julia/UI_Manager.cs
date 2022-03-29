using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // Variables
    public Slider volumeSlider;
    public float volumeSliderValue;
    public Image muteImage;
    public Image unmuteImage;
    
    // Start is called before the first frame update
    void Start()
    {
        // Crear valor inicial la primera vez que se juega
        volumeSlider.value = PlayerPrefs.GetFloat("audioVolume", 0.5f);
        
        // Volumen del juego de 0 a 1 (muted or max volume)
        AudioListener.volume = volumeSlider.value;
        
        // Revisar muteado
        AmIMuted();
    }

    public void ChangeSliderVolume(float value)
    {
        volumeSliderValue = value;
        
        // Guardado del valor al mover el slider para cuando se cierren las options o el juego
        PlayerPrefs.SetFloat("audioVolume", volumeSliderValue);
        AudioListener.volume = volumeSlider.value;
        
        // Revisar muteado
        AmIMuted();
    }

    // Método para saber si el juego está muteado
    public void AmIMuted()
    {
        // Si el juego está muteado, se mostrará la imagen de muteado
        if (volumeSliderValue == 0)
        {
            muteImage.enabled = true;
            unmuteImage.enabled = false;
        }
        
        // Si el juego no está muteado, no se mostrará la imagen de muteado
        else 
        {
            muteImage.enabled = false;
            unmuteImage.enabled = true;
        }
    }
}
