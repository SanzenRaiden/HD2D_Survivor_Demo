using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    /// <summary>
    /// 储存全部升级选项
    /// </summary>
    public List<GameObject> options;
    public List<GameObject> statsUp;
    public List<GameObject> skillFireballUp;
    public List<GameObject> skillTornadoUp;
    public GameObject gainTornado;
    public GameObject gainDarkSawblade;

    //选项栏
    public Transform OptionBox1;
    public Transform OptionBox2;
    public Transform OptionBox3;

    //选项内容
    public GameObject option1;
    public GameObject option2;
    public GameObject option3;

    public Transform skillList;
    public Player player;
    private MusicManagement Music;

    private void OnEnable()
    {
        RefleshOptions();
        Music = GameObject.Find("MusicManagement").GetComponent<MusicManagement>();
    }

    private void Update()
    {
        //角色死亡时关闭选项栏
        if(player.playerDead)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 更新选项栏UI
    /// </summary>
    public void RefleshOptions()
    {
        options = new List<GameObject>();
        options.AddRange(statsUp);
        options.AddRange(skillFireballUp);

        bool haveTornado = false;
        bool haveDarkSawblade = false;
        foreach(Transform ski in skillList)
        {
            BasalSkill skill = ski.GetComponent<BasalSkill>();
            if(skill.skillName == "Tornado")
            {
                haveTornado = true;
            }
            if (skill.skillName == "DarkSawblade")
            {
                haveDarkSawblade = true;
            }
        }

        if(haveTornado)
        {
            options.AddRange(skillTornadoUp);
        }
        else
        {
            options.Add(gainTornado);
        }

        if(haveDarkSawblade)
        {
            //提升技能属性
        }
        else
        {
            options.Add(gainDarkSawblade);
        }

        
        option1 = RandomOption();
        option2 = RandomOption();
        while(option2 == option1)
        {
            option2 = RandomOption();
        }
        option3 = RandomOption();
        while(option3 == option1 || option3 == option2)
        {
            option3 = RandomOption();
        }

        RefleshSingleOption(OptionBox1, option1);
        RefleshSingleOption(OptionBox2, option2);
        RefleshSingleOption(OptionBox3, option3);

    }

    /// <summary>
    /// 随机升级三选一选项总列表里的选项
    /// </summary>
    /// <returns>放入选项栏的选项物体</returns>
    public GameObject RandomOption()
    {
        int optionAmount = options.Count;
        int amount = Random.Range(0, optionAmount);
        return options[amount];
    }

    /// <summary>
    /// 刷新升级三选一UI
    /// </summary>
    /// <param name="optionBox">刷新的选项栏容器</param>
    /// <param name="option">刷新的具体选项</param>
    public void RefleshSingleOption(Transform optionBox, GameObject option)
    {
        BasalLevelupOptions levelupOption = option.GetComponent<BasalLevelupOptions>();
        optionBox.GetChild(0).GetComponent<TextMeshProUGUI>().text = levelupOption.optionName;
        if(levelupOption.Icon)
        {
            optionBox.GetChild(2).gameObject.SetActive(true);
            optionBox.GetChild(2).GetComponent<Image>().sprite = levelupOption.Icon;
        }
        else
        {
            optionBox.GetChild(2).gameObject.SetActive(false);
        }
        optionBox.GetChild(1).GetComponent<TextMeshProUGUI>().text = levelupOption.optionDescription;
    }

    public void ClickOption1()
    {
        Music.PlayUIAudio();
        option1.GetComponent<BasalLevelupOptions>().SelectOption();

    }

    public void ClickOption2()
    {
        Music.PlayUIAudio();
        option2.GetComponent<BasalLevelupOptions>().SelectOption();

    }

    public void ClickOption3()
    {
        Music.PlayUIAudio();
        option3.GetComponent<BasalLevelupOptions>().SelectOption();

    }
}
