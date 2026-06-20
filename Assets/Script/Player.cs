using System.Collections;
using UnityEngine;

public class Player : BasalStats
{
    public Material normalMaterial;
    public Material hitedMaterial;

    public AudioClip hitedAudio;
    public AudioSource seVolume;

    /// <summary>
    /// 判断角色是否死亡
    /// </summary>
    public bool playerDead = false;

    public float gainRadius;

    private Rigidbody rb;
    public Animator anim;
    public BasalStats startStats;

    public Transform skillList;
    public BasalSkill startSkill;

    public BattleUI battleUI;

    public int talentPoint = 0;
    public int TotalTalentPoint = 0;
    /// <summary>
    /// 生命值加成次数
    /// </summary>
    public int healthAddition = 0;
    /// <summary>
    /// 攻击力加成次数
    /// </summary>
    public int attackAddition = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //旧输入系统的角色移动逻辑
    void Update()
    {
        
        if (playerDead ==false)
        {
            //移动逻辑
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            rb.linearVelocity = new Vector3(horizontal, 0, vertical).normalized * moveSpeed;

            if (horizontal != 0 || vertical != 0)
            {
                anim.SetBool("moving", true);
            }
            else if (horizontal == 0 && vertical == 0)
            {
                anim.SetBool("moving", false);
            }

            //角色朝向
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            //使用技能
            if (skillList.childCount > 0)
            {
                foreach (Transform skill in skillList)
                {
                    BasalSkill ski = skill.GetComponent<BasalSkill>();
                    ski.player = gameObject;
                    if (ski.cooldownKey >= ski.cooldownTime)
                    {
                        StartCoroutine(ski.UseSkill());
                    }
                }
            }

            //升级
            if (currentExperience >= maxExperience)
            {
                currentExperience -= maxExperience;
                LevelUp();
            }
        }
    }

    /// <summary>
    /// 玩家属性初始化
    /// </summary>
    public void StartPlayerStsts()
    {
        maxHealth = startStats.maxHealth;
        currentHealth = maxHealth;
        maxExperience = startStats.maxExperience;
        currentExperience = 0;
        attack = startStats.attack;
        armor = startStats.armor;
        criticalChance = startStats.criticalChance;
        bodgeChance = startStats.bodgeChance;
        moveSpeed = startStats.moveSpeed;

    }

    /// <summary>
    /// 角色技能表初始化
    /// </summary>
    public void StartPlayerSkill()
    {
        foreach(Transform skill in skillList)
        {
            BasalSkill ski = skill.GetComponent<BasalSkill>();
            if(ski.skillName != startSkill.skillName)
            {
                Destroy(ski.gameObject);
            }
        }
    }

    /// <summary>
    /// 天赋加成
    /// </summary>
    public void TalentAddition()
    {
        maxHealth += healthAddition * 20;
        currentHealth = maxHealth;
        attack += attackAddition * 5;
    }

    /// <summary>
    /// 等级提升
    /// </summary>
    public void LevelUp()
    {
        level += 1;
        maxHealth += 20;
        currentHealth = maxHealth;
        maxExperience += 20;
        battleUI.LevelupOptions();
    }

    /// <summary>
    /// 角色死亡，播放死亡动画
    /// </summary>
    public void Dying()
    {
        playerDead = true;
        anim.SetTrigger("Dead");
        battleUI.DeadUI();
    }

    /// <summary>
    /// 受击效果，特效、音效
    /// </summary>
    public void StartHitedback()
    {
        if (hitedAudio != null)
        {
            GetComponent<AudioSource>().clip = hitedAudio;
            GetComponent<AudioSource>().volume = seVolume.volume;
            GetComponent<AudioSource>().Play();
        }
        StartCoroutine(Hitfeedback());
    }

    //受击时更改材质，一段时间再变回原材质
    public IEnumerator Hitfeedback()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = hitedMaterial;
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = normalMaterial;
    }
}
