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
    [SerializeField] private Text timeOnScreen;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;
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
            Debug.Log("Hola user "+ new_user.player_nickname);
            Debug.Log("Hola record "+ new_user.player_record);
            DataManager.Instance.NewUserTMP = new_user;
            WebRequestManager.Instance.LeerJSON_Usuarios_Y_Crear_Usuario();
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
}
