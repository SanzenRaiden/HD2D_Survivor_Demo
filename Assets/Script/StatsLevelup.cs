using UnityEngine;

public class StatsLevelup : BasalLevelupOptions
{
    public override void SelectOption()
    {
        player = GameObject.Find("PlayerObject").transform.GetChild(0).GetComponent<Player>();
        battleUI = GameObject.Find("BattleUI").GetComponent<BattleUI>();

        switch (playerStats)
        {
            case PlayerStats.maxHealth:
                player.maxHealth += (int)upValue;
                player.currentHealth += (int)upValue;
                break;
            case PlayerStats.moveSpeed:
                player.moveSpeed += (int)upValue;
                break;
            case PlayerStats.attack:
                player.attack += (int)upValue;
                break;
            case PlayerStats.armor:
                player.armor += (int)upValue;
                break;
            case PlayerStats.criticalChance:
                player.criticalChance += upValue;
                break;
            case PlayerStats.bodgeChance:
                player.bodgeChance += upValue;
                break;
            
        }
        CloseOption();
    }
}
