using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject skillProject;
    public Transform skillBox;
    public Transform playerSkillList;
    public GameObject levelupOptions;
    public Player player;
    public Transform menu;
    public Image health;
    public TextMeshProUGUI healthNumber;
    public Transform experienceBra;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timer;
    public int second;
    public int minute;
    public bool isTime = false;
    public float countTime = 0;
    public GameObject deadUI;
    public GameObject titleFrames;
    public GameObject battleUI;
    public GameObject VictoryUI;
    public GameObject EditUI;
    public GameObject InstructionsUI;
    public MusicManagement Music;
    public Slider bgmVolume;
    public Slider seVolume;

    void OnEnable()
    {
        RefleshSkillUI();
        CountdownTimer();
        Music.PlayBGM(Music.battleBGM);
        bgmVolume.value = Music.BGMvolume;
        seVolume.value = Music.SEvolume;
        Debug.Log(Music.BGMvolume + " , " + Music.SEvolume);
        Debug.Log(bgmVolume.value + " , " + seVolume.value);
    }

    //对UI进行更新
    void Update()
    {
        //技能冷却条UI更新
        int index = 0;
        if(skillBox.childCount > 0)
        {
            foreach (Transform skill in playerSkillList)
            {
                BasalSkill s = skill.GetComponent<BasalSkill>();
                float progressBra = s.cooldownKey / s.cooldownTime;
                skillBox.transform.GetChild(index).GetChild(1).GetComponent<Image>().fillAmount = 1 - progressBra;
                index++;
                if(index > skillBox.childCount)
                    index = 0;
            }
        }
        //生命值UI
        float healthProportion = (float)player.currentHealth / (float)player.maxHealth;
        health.fillAmount = healthProportion;
        healthNumber.text = player.currentHealth + " / " + player.maxHealth;
        //经验、等级UI
        float experienceProportion = (float)player.currentExperience / (float)player.maxExperience;
        experienceBra.localScale = new Vector3(experienceProportion, 1, 1);
        levelText.text = "LEVEL : " + player.level;

        if(isTime)
        {
            countTime += Time.deltaTime;
            if(countTime > 1)
            {
                countTime = 0;
                second -= 1;
                if(second < 0)
                {
                    minute-= 1;
                    second = 59;
                }
                if(second<= 0&&minute<= 0)
                {
                    GameVictory();
                }
            }
            if (second < 10)
                timer.text = minute + ":0" + second;
            else
                timer.text = minute + ":" + second;
        }
    }

    public void CountdownTimer()
    {
        minute = 10;
        second = 0;
        isTime = true;
    }

    //更新UI的图片，并将技能UI放入UI栏
    public void RefleshSkillUI()
    {
        if(skillBox.childCount > 0)
        {
            foreach(Transform ski in skillBox)
            {
                Destroy(ski.gameObject);
            }
        }
        foreach (Transform playerSki in playerSkillList)//技能UI生成为子集，在UI栏加入横向布局组，自动排列子集
        {
            GameObject skill = Instantiate(skillProject, skillBox);

            skill.transform.GetChild(0).GetComponent<Image>().sprite = playerSki.GetComponent<BasalSkill>().icon;
            skill.transform.GetChild(1).GetComponent<Image>().sprite = playerSki.GetComponent<BasalSkill>().icon;

        }
    }

    public void GameVictory()
    {
        VictoryUI.SetActive(true);
        player.playerDead = true;
        int talentPoint = player.level * 10;
        VictoryUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Gain TalentPoint: " + talentPoint;
        player.talentPoint += talentPoint;
        player.TotalTalentPoint += talentPoint;
    }

    public void AdjustBGMVolume()
    {
        Music.BGMAudioVolumeSlider(bgmVolume);
    }

    public void AdjustSEVolume()
    {
        Music.SEAudioVolumeSlider(seVolume);
    }

    public void ClickOpenMenu()
    {
        Music.PlayUIAudio();
        menu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClickContinue()
    {
        Music.PlayUIAudio();
        menu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ClickEdit()
    {
        Music.PlayUIAudio();
        Debug.Log("before: " + Music.BGMvolume + " , " + Music.SEvolume);
        Debug.Log("before: " + bgmVolume.value + " , " + seVolume.value);
        
        bgmVolume.value = Music.BGMvolume;
        seVolume.value = Music.SEvolume;
        EditUI.SetActive(true);
       
        Debug.Log("after: " + Music.BGMvolume + " , " + Music.SEvolume);
        Debug.Log("after: " + bgmVolume.value + " , " + seVolume.value);
    }

    public void ClickExitEdit()
    {
        Music.PlayUIAudio();
        EditUI.SetActive(false);
    }

    public void ClickInstructions()
    {
        Music.PlayUIAudio();
        InstructionsUI.SetActive(true);
    }

    public void ClickExitInstruction()
    {
        Music.PlayUIAudio();
        InstructionsUI.SetActive(false);
    }

    public void ClickExit()
    {

    }


    public void LevelupOptions()
    {
        Time.timeScale = 0;
        levelupOptions.gameObject.SetActive(true);
    }

    public void DeadUI()
    {
        deadUI.SetActive(true);

    }

    public void BackToTitleFrames()
    {
        Music.PlayUIAudio();
        titleFrames.SetActive(true);
        deadUI.SetActive(false);
        VictoryUI.SetActive(false);
        menu.gameObject.SetActive(false);
        player.StartPlayerSkill();
        battleUI.SetActive(false);

    }
}
