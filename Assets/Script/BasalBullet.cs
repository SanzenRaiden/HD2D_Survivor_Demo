using TMPro;
using UnityEngine;

public class BasalBullet : MonoBehaviour
{
    public int damage;
    public int level;
    public float lifeTime;
    public int penetration;
    public float bulletSpeed;
    public float bulletSize;

    public GameObject damageNumber;
    /// <summary>
    /// 获取技能属性数值
    /// </summary>
    public BasalSkill fatherSkill;
    public BasalStats player;

    /// <summary>
    /// 子弹是否可以发射
    /// </summary>
    public bool cango = false;

    public Rigidbody rb;
    //子弹追踪敌人逻辑变量
    public Transform enemyObject;
    public GameObject chasingTarget;
    public Vector3 distance;

    public AudioClip skillPlayAudio;
    public AudioClip bulletHitAudio;

    private void OnEnable()
    {
        if(skillPlayAudio != null)
        {
            GetComponent<AudioSource>().clip = skillPlayAudio;
            GetComponent<AudioSource>().volume = GameObject.Find("MusicManagement").GetComponent<MusicManagement>().SEvolume;
            GetComponent<AudioSource>().Play();
            
        }
    }

    /// <summary>
    /// 获取父技能的属性
    /// </summary>
    public virtual void GetFather()
    {
        damage = fatherSkill.damage;
        level = fatherSkill.level;
        lifeTime = fatherSkill.lifeTime;
        penetration = fatherSkill.penetration;
        bulletSpeed = fatherSkill.bulletSpeed;
        bulletSize = fatherSkill.bulletSize;
        player = GameObject.Find("PlayerObject").transform.GetChild(0).GetComponent<BasalStats>();
        rb = GetComponent<Rigidbody>();
        enemyObject = GameObject.Find("EnemyObject").transform;
    }

    /// <summary>
    /// 子弹造成伤害
    /// </summary>
    /// <param name="other">碰撞物体检测</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            if (bulletHitAudio != null)
            {
                GetComponent<AudioSource>().clip = bulletHitAudio;
                GetComponent<AudioSource>().volume = GameObject.Find("MusicManagement").GetComponent<MusicManagement>().SEvolume;
                GetComponent<AudioSource>().Play();
            }
            penetration -= 1;
            if(penetration <= 0)
            {
                DestroySelf();
            }
            
            BasalEnemy enemy = other.GetComponent<BasalEnemy>();

            if (enemy.currentHealth > 0)
            {
                float finalDamage = damage + player.attack;
                float random = Random.value;
                if(player.criticalChance >= random)
                {
                    finalDamage *= 2;
                }
                finalDamage -= enemy.armor;
                enemy.currentHealth -= (int)finalDamage;
                
                GameObject text = Instantiate(damageNumber, other.transform.position, default);
                text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)finalDamage).ToString();
                other.GetComponent<BasalEnemy>().StartHitedback();
                if (enemy.currentHealth <= 0)
                {
                    enemy.Dying();
                }
            }
        }

    }

    /// <summary>
    /// 生存时间结束删除子弹
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 子弹获取目标追踪逻辑
    /// </summary>
    public void GetTargetRole()
    {
        float chaseDistance = 999;
        Transform currentChaeing = null;

        if (enemyObject.childCount > 0)
        {
            foreach (Transform item in enemyObject)
            {
                float distance = Vector3.Distance(item.position, transform.position);//怪物自身和玩家的距离

                if (distance < chaseDistance)//比较距离，追击最近的玩家
                {
                    chaseDistance = distance;
                    currentChaeing = item;
                }
            }
            chasingTarget = currentChaeing.gameObject;
        }
        if(chasingTarget != null)
        {
            Vector3 targetPosition = chasingTarget.transform.position;//目标坐标
            distance = targetPosition - transform.position;//指向目标的移动向量
        }
        //无敌人随机坐标移动
        else
        {
            float randomXAxis = Random.Range(-1f, 1f);
            float randomZAxis = Random.Range(-1f, 1f);
            Vector3 targetPosition = transform.position + new Vector3(randomXAxis, 0, randomZAxis);//随机目标坐标
            distance = targetPosition - transform.position;//指向目标的移动向量
        }
       
    }

    private void FixedUpdate()
    {
        //子弹移动
        if (cango)
        {
            Vector3 move = new Vector3(distance.x, 0, distance.z).normalized * bulletSpeed;
            rb.linearVelocity = move;
            float angle = Mathf.Atan2(distance.z, distance.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            lifeTime -= Time.fixedDeltaTime;
            if(lifeTime < 0)
            {
                DestroySelf();
            }
        }
    }
}
