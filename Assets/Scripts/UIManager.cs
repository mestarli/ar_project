using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private Text nickname;
    [SerializeField] private Text record_game;
    public Text timeOnScreen;
    [SerializeField] private Image countLife;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;
    [SerializeField] private GameObject panelYouWin;
    [SerializeField] private GameObject panelYouLoose;
    [SerializeField] private GameObject butonSave;
    
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        UpdateTimerUI();
    }

    /// <summary>
    /// Método que guardara el record del usuario
    /// </summary>
    public void saveRecordUser()
    {
        if (!string.IsNullOrEmpty(nickname.text))
        {
            User new_user = new User();
            new_user.player_nickname = nickname.text;
            new_user.player_record = record_game.text;
            DataManager.Instance.NewUserTMP = new_user;
            WebRequestManager.Instance.LeerJSON_Usuarios_Y_Crear_Usuario();
            butonSave.SetActive(false);
        }
    }
    
    public void UpdateTimerUI(){
        //set timer UI
        secondsCount += Time.deltaTime;
        timeOnScreen.text = hourCount.ToString("00") +":"+ minuteCount.ToString("00") +":"+secondsCount.ToString("00") + "";
        if(secondsCount >= 60){
            minuteCount++;
            secondsCount %= 60;
            if(minuteCount >= 60){
                hourCount++;
                minuteCount %= 60;
            }
        }    
    }
    
    public void UpdateLife(float life,float maxlife)
    {
        countLife.fillAmount = life / maxlife;
    }

    public void restartGame()
    {
        SceneManager.LoadScene("MainLevelOptimizar");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void YouWin()
    {
        record_game.text = timeOnScreen.text;
        Cursor.lockState = CursorLockMode.None;
        panelYouWin.SetActive(true);
    }
    public void YouLose()
    {
        Cursor.lockState = CursorLockMode.None;
        panelYouLoose.SetActive(true);
    }
}
