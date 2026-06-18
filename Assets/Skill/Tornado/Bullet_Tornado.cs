using UnityEngine;

public class Bullet_Tornado : BasalBullet
{
    public float forwardAngle;

    //移动逻辑
    private void FixedUpdate()
    {
        if (cango)
        {
            float angle = forwardAngle * Mathf.Deg2Rad;
            Vector3 forward = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            rb.linearVelocity = forward * bulletSpeed;

            lifeTime -= Time.fixedDeltaTime;
            if (lifeTime < 0)
            {
                DestroySelf();
            }
        }
    }
}
