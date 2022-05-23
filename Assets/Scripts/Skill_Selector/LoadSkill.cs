using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadSkill : MonoBehaviour
{
    public static LoadSkill Instance;
    private int SelectedSkill_1;
    private int SelectedSkill_2;
    private int SelectedSkill_3;
    public int Skill_1;
    public int Skill_2;
    public int Skill_3;

    public GameObject player;
    
    
    [SerializeField] private List<Component> skills_scripts;

    public GameObject SelectedSkillImage1;
    public GameObject SelectedSkillImage2;
    public GameObject SelectedSkillImage3;

    public List<Sprite> Sprites;
    public MonoBehaviour EnableSkill_01;
    public MonoBehaviour EnableSkill_02;
    public MonoBehaviour EnableSkill_03;
    
    [SerializeField]private List<string> Skills;
    void Awake()
    {
        Instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        
        Skill_1 = PlayerPrefs.GetInt("SelectedSkill_1", SelectedSkill_1);
        Skill_2 = PlayerPrefs.GetInt("SelectedSkill_2", SelectedSkill_2);
        Skill_3 = PlayerPrefs.GetInt("SelectedSkill_3", SelectedSkill_3);
        Skills.Add("PlayerClone");
        Skills.Add("IceWall");
        Skills.Add("Dash");
        Skills.Add("Regeneracion");
        Skills.Add("Invisibility");
        Skills.Add("Blind");
        Skills.Add("Polimorfo");
        Skills.Add("SmokeBomb");
        PrintSelectedSkill();
        LoadSkills();
    }

    public void PrintSelectedSkill()
    {
        SelectedSkillImage1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Sprites[Skill_1];
        SelectedSkillImage2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite  = Sprites[Skill_2];
        SelectedSkillImage3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite  = Sprites[Skill_3];
    }
    
    private void LoadSkills(){
        EnableSkill_01= (MonoBehaviour) player.GetComponent(Type.GetType(Skills[Skill_1]));
        EnableSkill_02 = (MonoBehaviour) player.GetComponent(Type.GetType(Skills[Skill_2]));
        EnableSkill_03 = (MonoBehaviour) player.GetComponent(Type.GetType(Skills[Skill_3]));
        EnableSkill_01.enabled = true;
        EnableSkill_02.enabled = true;
        EnableSkill_03.enabled = true;

    }
}
