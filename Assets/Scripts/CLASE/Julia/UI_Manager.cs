using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // Variables
    
    [Header("UI")]
    public Slider volumeSlider;
    public Dropdown imageQualityDropdown;
    public Dropdown screenResolutionDropdown;
    public Toggle fullScreenToggle;
    public Text userRecord;
    private Resolution[] screenResolutions;
    
    private float volumeSliderValue;
    private int imageQualityInt;
    
    [SerializeField] private Image muteImage;
    [SerializeField] private Image unmuteImage;

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject mainMenuPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        gamePanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        if (userRecord)
        {
            userRecord.text = UIManager.Instance.timeOnScreen.text;
        }

        #region Volume
        
        // Crear valor inicial la primera vez que se juega
        volumeSlider.value = PlayerPrefs.GetFloat("audioVolume", 0.5f);
        
        // Volumen del juego de 0 a 1 (muted or max volume)
        AudioListener.volume = volumeSlider.value;
        
        // Revisar muteado
        AmIMuted();

        #endregion

        #region Fullscreen

        // Detecci�n de modo ventana o modo pantalla completa cuando entramos al men� del juego
        if (Screen.fullScreen)
        {
            fullScreenToggle.isOn = true;
        }

        else
        {
            fullScreenToggle.isOn = false;
        }

        #endregion

        #region Screen Resolution

        CheckScreenResolution();

        #endregion

        #region Image Quality

        imageQualityInt = PlayerPrefs.GetInt("qualityNum", 3);
        imageQualityDropdown.value = imageQualityInt;
        AdjustQualityImage();

        #endregion
    }

    #region Play

    public void PlayGame()
    {
        StartCoroutine(Coroutine_GameScene());
    }

    IEnumerator Coroutine_GameScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("SelectAbilities");
        optionsPanel.SetActive(false);
        gamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    #endregion

    #region Options

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
        gamePanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        
        Time.timeScale = 0f;
    }

    public void ToMenu()
    {
        StartCoroutine(Coroutine_MainMenuScene());
    }
    
    public void ToGame()
    {
        optionsPanel.SetActive(false);
        gamePanel.SetActive(true);        
        mainMenuPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quited");
    }

    IEnumerator Coroutine_MainMenuScene()
    {
        yield return new WaitForSeconds(2.0f);
        //SceneManager.LoadScene("StartMenu");
        optionsPanel.SetActive(false);
        gamePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    #endregion
    
    #region Volume
    
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
    
    #endregion

    #region FullScreen

    public void ActivateFullScreenMode(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    #endregion

    #region Screen Resolution

    private void CheckScreenResolution()
    {
        // Guardado de todas las resoluciones de cada ordenador
        screenResolutions = Screen.resolutions;
        
        // Borrar las opciones predeterminadas del dropdown
        screenResolutionDropdown.ClearOptions();

        // Lista de strings para guardar el tama�o de la resoluci�n
        List<string> options = new List<string>();

        // Variable para iniciar de 0
        int actualResolution = 0;

        // 
        for (int i = 0; i < screenResolutions.Length; i++)
        {
            // Mostrar las resoluciones en la barra de opciones del dropdown (Ex: 1920x1080)
            string option = screenResolutions[i].width + " x " + screenResolutions[i].height;
            options.Add(option);

            // Revisado de la opci�n guardada para guardar la resoluci�n actual de la pantalla
            if (Screen.fullScreen && screenResolutions[i].width == Screen.currentResolution.width &&
                screenResolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }

        // Agregado de opciones guardadas en la lista
        screenResolutionDropdown.AddOptions(options);

        // Detecci�n de la resoluci�n en la que estamos
        screenResolutionDropdown.value = actualResolution;

        // Actualizado de la lista
        screenResolutionDropdown.RefreshShownValue();

        // Valor predeterminado para el primer inicio del juego
        screenResolutionDropdown.value = PlayerPrefs.GetInt("screenResolutionNum", 0);
    }

    // M�todo para cambiar la resoluci�n en el dropdown
    public void ChangeScreenResolution(int screenResolutionIndex)
    {
        // Cambiado de valor y guardado de este mismo y se muestre en la pantalla una vez cerrado el juego
        PlayerPrefs.SetInt("screenResolutionNum", screenResolutionDropdown.value);

        // Creado moment�neo de un valor de resoluci�n
        Resolution resolution = screenResolutions[screenResolutionIndex];

        // Cambia la resoluci�n solamente en pantalla completa
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #endregion

    #region ImageQuality

    public void AdjustQualityImage()
    {
        QualitySettings.SetQualityLevel(imageQualityDropdown.value);
        PlayerPrefs.SetInt("qualityNum", imageQualityDropdown.value);
        imageQualityInt = imageQualityDropdown.value;
    }

    #endregion
}
