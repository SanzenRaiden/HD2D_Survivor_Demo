using Unity.VisualScripting;
using UnityEngine;

public class GainSkill : BasalLevelupOptions
{
    public override void SelectOption()
    {
        player = GameObject.Find("PlayerObject").transform.GetChild(0).GetComponent<Player>();
        battleUI = GameObject.Find("BattleUI").GetComponent<BattleUI>();

        Instantiate(skillInOption.gameObject, player.skillList);
        battleUI.RefleshSkillUI();
        CloseOption();
    }
}
