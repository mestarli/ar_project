using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSkill : MonoBehaviour
{
    private int SelectedSkill_1;
    private int SelectedSkill_2;
    private int SelectedSkill_3;
    public int Skill_1;
    public int Skill_2;
    public int Skill_3;
    
    public Image SelectedSkillImage1;
    public Image SelectedSkillImage2;
    public Image SelectedSkillImage3;

    public List<Sprite> Sprites;
    
    void Start()
    {
       Skill_1 = PlayerPrefs.GetInt("SelectedSkill_1", SelectedSkill_1);
       Skill_2 = PlayerPrefs.GetInt("SelectedSkill_2", SelectedSkill_2);
       Skill_3 = PlayerPrefs.GetInt("SelectedSkill_3", SelectedSkill_3);
       PrinSelectedSkill();
    }
    
    public void PrinSelectedSkill()
    {
        SelectedSkillImage1.sprite = Sprites[Skill_1];
        SelectedSkillImage2.sprite = Sprites[Skill_2];
        SelectedSkillImage3.sprite = Sprites[Skill_3];
    }
}
