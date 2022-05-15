using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SillSelection : MonoBehaviour
{
    public int SelectedSkill_1 = 10;
    public int SelectedSkill_2 = 10;
    public int SelectedSkill_3 = 10;

    public Image SelectedSkillImage1;
    public Image SelectedSkillImage2;
    public Image SelectedSkillImage3;

    public List<Sprite> Sprites;
    
    public GameObject AllSkillsSelectedBttn;
    
    private void Start()
    {
        AllSkillsSelectedBttn.SetActive(false);
    }

    public void SelectSkill(int skill)
    {
        if (SelectedSkill_1 == 10)
        {
            SelectedSkill_1 = skill;
            SelectedSkillImage1.sprite = Sprites[skill];
            if (SelectedSkill_1 == SelectedSkill_2 || SelectedSkill_1 == SelectedSkill_3)
            {
                SelectedSkill_1 = 10;
                SelectedSkillImage1.sprite = Sprites[8];
            }
        }
        else if (SelectedSkill_2 == 10)
        {
            SelectedSkill_2 = skill;
            SelectedSkillImage2.sprite = Sprites[skill];
            if (SelectedSkill_2 == SelectedSkill_1 || SelectedSkill_2 == SelectedSkill_3)
            {
                SelectedSkill_2 = 10;
                SelectedSkillImage2.sprite = Sprites[8];
            }
        }
        else if (SelectedSkill_3 == 10)
        {
            SelectedSkill_3 = skill;
            SelectedSkillImage3.sprite = Sprites[skill];

            if (SelectedSkill_3 == SelectedSkill_2 || SelectedSkill_3 == SelectedSkill_1)
            {
                SelectedSkill_3 = 10;
                SelectedSkillImage3.sprite = Sprites[8];
                AllSkillsSelectedBttn.SetActive(false);
            }
        }

        if (SelectedSkill_1 != 10 && SelectedSkill_2 != 10 && SelectedSkill_3 != 10)
        {
            AllSkillsSelectedBttn.SetActive(true);
        }
    }
    
    public void UnselectSkill(int skill)
    {
        if (skill == 0)
        {
            SelectedSkill_1 = 10;
            SelectedSkillImage1.sprite = Sprites[8];
        }
        else if (skill == 1)
        {
            SelectedSkill_2 = 10;
            SelectedSkillImage2.sprite = Sprites[8];
        }
        else if (skill == 2)
        {
            SelectedSkill_3 = 10;
            SelectedSkillImage3.sprite = Sprites[8];
        }
        AllSkillsSelectedBttn.SetActive(false);
    }
    
    public void AllSkillsSelected()
    {
        PlayerPrefs.SetInt("SelectedSkill_1", SelectedSkill_1);
        PlayerPrefs.SetInt("SelectedSkill_2", SelectedSkill_2);
        PlayerPrefs.SetInt("SelectedSkill_3", SelectedSkill_3);
        //LoadScene
        SceneManager.LoadScene("MainLevelOptimizar");
    }
}
