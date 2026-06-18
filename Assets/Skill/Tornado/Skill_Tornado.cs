using UnityEngine;
using System.Collections;

public class Skill_Tornado : BasalSkill
{
    //覆写生成逻辑
    public override IEnumerator UseSkill()
    {
        cooldownKey = 0;
        float angleSpace = 360 / bulletNumber;
        float currentAngle = 0;
        for (int i = 0; i < bulletNumber; i++)
        {
            Vector3 startPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            GameObject newBullet = Instantiate(bulletObject, startPosition, Quaternion.Euler(new Vector3(0, 0, angle)));
            BasalBullet bullet = newBullet.GetComponent<BasalBullet>();
            bullet.fatherSkill = this;
            bullet.GetFather();
            bullet.GetComponent<Bullet_Tornado>().forwardAngle = currentAngle;
            bullet.cango = true;
            yield return new WaitForSeconds(interval);
            currentAngle += angleSpace;
        }
    }
}
