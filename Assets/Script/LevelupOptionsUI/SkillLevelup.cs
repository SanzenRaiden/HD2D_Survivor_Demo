using UnityEngine;

public class SkillLevelup : BasalLevelupOptions
{
    public override void SelectOption()
    {
        player = GameObject.Find("PlayerObject").transform.GetChild(0).GetComponent<Player>();
        battleUI = GameObject.Find("BattleUI").GetComponent<BattleUI>();
        BasalSkill skill = null;
        foreach(Transform ski in player.skillList)
        {
            BasalSkill s = ski.GetComponent<BasalSkill>();
            if(skillInOption.skillName == s.skillName)
            {
                skill = s;
            }
        }

        switch (skillStats)
        {
            case SkillStats.cooldownTime:
                skill.cooldownTime += (int)upValue;
                break;
            case SkillStats.bulletNumber:
                skill.bulletNumber += (int)upValue;
                break;
            case SkillStats.damage:
                skill.damage += (int)upValue;
                break;
            case SkillStats.lifeTime:
                skill.lifeTime += (int)upValue;
                break;
            case SkillStats.penetration:
                skill.penetration += (int)upValue;
                break;
            case SkillStats.bulletSize:
                skill.bulletSize += upValue;
                break;

        }
        CloseOption();
    }
}
