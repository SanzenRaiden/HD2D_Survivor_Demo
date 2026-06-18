using UnityEngine;

public class Bullet_DarkSawblade : BasalBullet
{
    public float radius;
    public float startAngle = 0;

    public override void GetFather()//获取父技能的属性
    {
        damage = fatherSkill.damage;
        level = fatherSkill.level;
        lifeTime = fatherSkill.lifeTime;
        penetration = fatherSkill.penetration;
        bulletSpeed = fatherSkill.bulletSpeed;
        bulletSize = fatherSkill.bulletSize;
        radius = fatherSkill.GetComponent<Skill_DarkSawblade>().radius;
        player = GameObject.Find("PlayerObject").transform.GetChild(0).GetComponent<BasalStats>();
        rb = GetComponent<Rigidbody>();
        enemyObject = GameObject.Find("EnemyObject").transform;
    }

    void FixedUpdate()
    {
        if(cango)
        {
            startAngle += bulletSpeed * Time.fixedDeltaTime;

            Vector3 bulletPosition = player.transform.position + new Vector3(radius *Mathf.Cos(startAngle), 2, radius * Mathf.Sin(startAngle));
            transform.position = bulletPosition;

            Vector3 forward = new Vector3(radius * Mathf.Cos(startAngle), 0, radius * Mathf.Sin(startAngle));
            transform.forward = new Vector3(0, 0, forward.z + 180);
            
            lifeTime -= Time.fixedDeltaTime;
            if (lifeTime < 0)
            {
                DestroySelf();
            }
        }
    }
}
