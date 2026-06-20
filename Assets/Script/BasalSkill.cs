using System.Collections;
using UnityEngine;

public class BasalSkill : MonoBehaviour
{

    public Sprite icon;//技能图标

    //技能属性
    public string skillName;
    /// <summary>
    /// 技能，发射子弹组的冷却间隔
    /// </summary>
    public float cooldownTime;
    public float cooldownKey;
    public int bulletNumber;
    /// <summary>
    /// 一个技能，一组子弹内每个子弹的发射间隔
    /// </summary>
    public float interval;

    //技能及子弹属性
    public int damage;
    public int level;
    public float lifeTime;
    public int penetration;
    public float bulletSpeed;
    public float bulletSize;
    /// <summary>
    /// 创建子弹对象
    /// </summary>
    public GameObject bulletObject;
    
    public GameObject player;//获取玩家对象坐标以生成子弹
    public float angle;//子弹生成角度
    public bool isForwardTarget;//是否朝向最近敌人

    void FixedUpdate()
    {
        //冷却倒计时
        cooldownKey += Time.fixedDeltaTime;
        if(cooldownKey > cooldownTime)
        {
            cooldownKey = cooldownTime;
        }
        
    }

    /// <summary>
    /// 冷却计时，冷却好时使用技能
    /// </summary>
    public virtual IEnumerator UseSkill()
    {
        cooldownKey = 0;
        for(int i = 0; i < bulletNumber; i++)
        {
            GameObject newBullet = Instantiate(bulletObject, player.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            BasalBullet bullet = newBullet.GetComponent<BasalBullet>();
            bullet.fatherSkill = this;
            bullet.GetFather();
            bullet.GetTargetRole();
            bullet.cango = true;
            yield return new WaitForSeconds(interval);
        }
    }
}
