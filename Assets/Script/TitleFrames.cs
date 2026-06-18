using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleFrames : MonoBehaviour
{
    public Transform enemyObject;
    public GameObject battleFrames;
    public GameObject talent;

    public Player player;
    public TextMeshProUGUI talentPointText;
    public Transform healthTalentBox;
    public Transform attackTalentBox;
    int healthLevel = 0;
    int attackLevel = 0;

    public MusicManagement Music;
    public Slider bgmVolume;
    public Slider seVolume;

    private void OnEnable()
    {
        RenderSettings.fogColor = new Color(0.55f, 0.53f, 0.97f, 1);
        RenderSettings.fogDensity = 0.05f;
        Time.timeScale = 1;
        Music.PlayBGM(Music.titleBGM);
        bgmVolume.value = Music.BGMvolume;
        seVolume.value = Music.SEvolume;
    }

    public void Click_StartGame()
    {
        Music.PlayUIAudio();
        if (enemyObject.childCount > 0)
        {
            foreach (Transform enemy in enemyObject)
            {
                Destroy(enemy.gameObject);
            }
        }
        
        battleFrames.SetActive(true);
        RenderSettings.fogDensity = 0;
        player.playerDead = false;
        player.StartPlayerStsts();
        player.TalentAddition();
        
        player.level = 1;
        player.currentExperience = 0;
        player.transform.localPosition = new Vector3(-0.9f, 1.1f, -1.87f);
        gameObject.SetActive(false);

    }

    public void Click_AddPlayers()
    {
        Music.PlayUIAudio();
        Application.OpenURL("https://cppreference.cn/w/");
    }

    public void Click_Talent()
    {
        Music.PlayUIAudio();
        talent.SetActive(true);
        RefleshTalent();

    }

    public void Click_Talent_Exit()
    {
        Music.PlayUIAudio();
        talent.SetActive(false);
    }

    public void Click_ExitGame()
    {
        Music.PlayUIAudio();
        Application.Quit();
    }

    public void AdjustBGMVolume()
    {
        Music.BGMAudioVolumeSlider(bgmVolume);
    }

    public void AdjustSEVolume()
    {
        Music.SEAudioVolumeSlider(seVolume);
    }

    public void AddTalent(int talentAddition, string talentName)
    {
        int consumePoint = (talentAddition * 10 + 10);
        if (player.talentPoint >= consumePoint)
        {
            player.talentPoint -= consumePoint;
            switch (talentName)
            {
                case "Health":
                    player.healthAddition += 1;
                    healthLevel += 1;
                    break;

                case "Attack":
                    player.attackAddition += 1;
                    attackLevel += 1;
                    break;

            }
            RefleshTalent();
        }
    }

    public void ReduceTalent(int talentAddition, string talentName)
    {
        if (talentAddition > 0)
        {
            switch (talentName)
            {
                case "Health":
                    player.healthAddition -= 1;
                    healthLevel -= 1;
                    break;

                case "Attack":
                    player.attackAddition -= 1;
                    attackLevel -= 1;
                    break;

            }
            player.talentPoint += ((talentAddition - 1) * 10 + 10);
            RefleshTalent();
        }
    }

    public void RefleshTalent()
    {
        RefleshSingleTalent(healthTalentBox, player.healthAddition, 20, "Health", healthLevel);
        RefleshSingleTalent(attackTalentBox, player.attackAddition, 5, "attack", attackLevel);
        talentPointText.text = player.talentPoint.ToString();
        
    }

    public void RefleshSingleTalent(Transform box, int talentAddTimes, int addition, string talentName, int level)
    {
        box.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = talentName + "+" + addition + "\n" + "<color=#FF6C6C>(Added " + talentAddTimes * addition + ")" + "|Level " + level + "</color>";
        box.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Consume:" + "\n" + (talentAddTimes * 10 + 10);
    }

    public void Click_AddHealth()
    {
        Music.PlayUIAudio();
        AddTalent(player.healthAddition, "Health");
    }

    public void Click_ReduceHealth()
    {
        Music.PlayUIAudio();
        ReduceTalent(player.healthAddition, "Health");
    }

    public void Click_AddAttack()
    {
        Music.PlayUIAudio();
        AddTalent(player.attackAddition, "Attack");
    }

    public void Click_ReduceAttack()
    {
        Music.PlayUIAudio();
        ReduceTalent(player.attackAddition, "Attack");
    }

    public void Click_ResetTalentPoint()
    {
        Music.PlayUIAudio();
        player.talentPoint = player.TotalTalentPoint;
        player.healthAddition = 0;
        player.attackAddition = 0;
        healthLevel = 0;
        attackLevel = 0;
        RefleshTalent();
    }
}
