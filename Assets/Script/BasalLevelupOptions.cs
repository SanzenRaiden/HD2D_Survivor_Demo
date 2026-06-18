using UnityEngine;

public class BasalLevelupOptions : MonoBehaviour
{
    public string optionName;
    public string optionDescription;
    
    public float upValue;
    public OptionsType optionsType;
    public PlayerStats playerStats;
    public SkillStats skillStats;

    public Player player;
    public BasalSkill skillInOption;
    public Sprite Icon;
    public BattleUI battleUI;

    public enum OptionsType
    {
        StatsLevelup,
        SkillLevelup,
        GainSkill,
    }

    public enum PlayerStats
    {
        maxHealth,
        moveSpeed,
        attack,
        armor,
        criticalChance,
        bodgeChance,
    }

    public enum SkillStats
    {
        cooldownTime,
        bulletNumber,
        damage,
        lifeTime,
        penetration,
        bulletSize,
    }


    public virtual void SelectOption()
    {

    }

    public void CloseOption()
    {
        Time.timeScale = 1;
        battleUI.levelupOptions.gameObject.SetActive(false);
    }
}
