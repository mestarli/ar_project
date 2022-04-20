using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSkill : MonoBehaviour
{
    private int SelectedSkill_1;
    private int SelectedSkill_2;
    private int SelectedSkill_3;
    public int Skill_1;
    public int Skill_2;
    public int Skill_3;
    
    void Start()
    {
       Skill_1 = PlayerPrefs.GetInt("SelectedSkill_1", SelectedSkill_1);
       Skill_2 = PlayerPrefs.GetInt("SelectedSkill_2", SelectedSkill_2);
       Skill_3 = PlayerPrefs.GetInt("SelectedSkill_3", SelectedSkill_3);
    }
}
