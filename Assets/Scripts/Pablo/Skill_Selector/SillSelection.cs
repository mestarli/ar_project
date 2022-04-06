using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SillSelection : MonoBehaviour
{
    public GameObject[] skills;
    public int SelectedSkill_1 = 0;
    public int SelectedSkill_2 = 0;
    public int SelectedSkill_3 = 0;

    public Text Skill_1;
    public Text Skill_2;
    public Text Skill_3;

    public int SkillsSelected = 0;

    public void NextSkill()
    {
        if (SkillsSelected == 0)
        {
            skills[SelectedSkill_1].SetActive(false);
            SelectedSkill_1 = (SelectedSkill_1 + 1) % skills.Length;
            skills[SelectedSkill_1].SetActive(true);
        }
        else if (SkillsSelected == 1)
        {
            skills[SelectedSkill_2].SetActive(false);
            SelectedSkill_2 = (SelectedSkill_2 + 1) % skills.Length;
            skills[SelectedSkill_2].SetActive(true);
        }
        else if (SkillsSelected == 2)
        {
            skills[SelectedSkill_3].SetActive(false);
            SelectedSkill_3 = (SelectedSkill_3 + 1) % skills.Length;
            skills[SelectedSkill_3].SetActive(true);
        }

    }

    public void PreviousSkill()
    {
        if (SkillsSelected == 0)
        {
            skills[SelectedSkill_1].SetActive(false);
            SelectedSkill_1--;
            if (SelectedSkill_1 < 0)
            {
                SelectedSkill_1 += skills.Length;
            }
            skills[SelectedSkill_1].SetActive(true);
        }
        else if (SkillsSelected == 1)
        {
            skills[SelectedSkill_2].SetActive(false);
            SelectedSkill_2--;
            if (SelectedSkill_2 < 0)
            {
                SelectedSkill_2 += skills.Length;
            }
            skills[SelectedSkill_2].SetActive(true);
        }
        else if (SkillsSelected == 2)
        {
            skills[SelectedSkill_3].SetActive(false);
            SelectedSkill_3--;
            if (SelectedSkill_3 < 0)
            {
                SelectedSkill_3 += skills.Length;
            }
            skills[SelectedSkill_3].SetActive(true);
        }

    }

    public void SelectSkill()
    {
        SkillsSelected++;
        
        if (SkillsSelected == 1)
        {
            Skill_1.text = "" + SelectedSkill_1;
            PlayerPrefs.SetInt("SelectedSkill_1", SelectedSkill_1);
        }
        else if (SkillsSelected == 2)
        {
            Skill_2.text = "" + SelectedSkill_2;
            PlayerPrefs.SetInt("SelectedSkill_2", SelectedSkill_2);
        }
        else if (SkillsSelected == 3)
        {
            Skill_3.text = "" + SelectedSkill_3;
            PlayerPrefs.SetInt("SelectedSkill_3", SelectedSkill_3);
            //LoadScene
        }
    }
}
