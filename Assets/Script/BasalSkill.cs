using System.Collections;
using UnityEngine;

public class BasalSkill : MonoBehaviour
{

    public Sprite icon;//技能图标

    //技能属性
    public string skillName;
    public float cooldownTime;//技能，发射子弹组的冷却间隔
    public float cooldownKey;
    public int bulletNumber;
    public float interval;//一个技能，一组子弹内每个子弹的发射间隔

    //技能及子弹属性
    public int damage;
    public int level;
    public float lifeTime;
    public int penetration;
    public float bulletSpeed;
    public float bulletSize;
    public GameObject bulletObject;//创建子弹对象
    
    public GameObject player;//获取玩家对象坐标以生成子弹
    public float angle;//子弹生成角度
    public bool isForwardTarget;//是否朝向最近敌人

    void FixedUpdate()
    {
        cooldownKey += Time.fixedDeltaTime;
        if(cooldownKey > cooldownTime)
        {
            cooldownKey = cooldownTime;
        }
        
    }

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
