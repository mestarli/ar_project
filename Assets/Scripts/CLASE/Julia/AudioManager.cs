using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Variables
    public static AudioManager instance;

    public Sound[] listSounds;

    //public Slider volumeSlider;

    //public float volumeSliderValue;
    
    void Awake()
    {
        instance = this;

        foreach (Sound sound in listSounds)
        {
            // Añadimos a la lista un audioSource y un clip para cada componente
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            
            // Accedemoa a cada audioSource y hacemos referencia al volumen, el loop y el playOnAwake para
            // poder modificar estos valores
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.playOnAwake = sound.playOnAwake;
        }
    }
}
