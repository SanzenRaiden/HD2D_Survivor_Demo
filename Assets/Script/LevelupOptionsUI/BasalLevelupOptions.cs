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

    /// <summary>
    /// 选择对应类型的选项执行对应类型的加成操作
    /// </summary>
    public virtual void SelectOption()
    {

    }

    /// <summary>
    /// 关闭选项栏
    /// </summary>
    public void CloseOption()
    {
        Time.timeScale = 1;
        battleUI.levelupOptions.gameObject.SetActive(false);
    }
}
