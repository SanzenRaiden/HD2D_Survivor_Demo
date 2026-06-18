using System.Collections;
using UnityEngine;

public class Player : BasalStats
{
    public Material normalMaterial;
    public Material hitedMaterial;
    public AudioClip hitedAudio;
    public AudioSource seVolume;
    public float gainRadius;

    public bool playerDead = false;
   
    private Rigidbody rb;
    public Animator anim;
    public BasalStats startStats;

    public Transform skillList;
    public BasalSkill startSkill;

    public BattleUI battleUI;

    public int talentPoint = 0;
    public int TotalTalentPoint = 0;
    public int healthAddition = 0;
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

            if (Input.GetKeyDown(KeyCode.Return))
            {
                currentExperience += 20;
            }

            if (currentExperience >= maxExperience)
            {
                currentExperience -= maxExperience;
                LevelUp();
            }
            if (Input.GetKeyUp(KeyCode.Delete))
            {
                currentHealth -= 20;
                if(currentHealth <= 0)
                {
                    Dying();
                }
            }
        }
    }

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

    public void TalentAddition()
    {
        maxHealth += healthAddition * 20;
        currentHealth = maxHealth;
        attack += attackAddition * 5;
    }

    public void LevelUp()
    {
        level += 1;
        maxHealth += 20;
        currentHealth = maxHealth;
        maxExperience += 20;
        battleUI.LevelupOptions();
    }

    //死亡事件
    public void Dying()
    {
        playerDead = true;
        anim.SetTrigger("Dead");
        battleUI.DeadUI();
    }

    //受击特效
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

    public IEnumerator Hitfeedback()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = hitedMaterial;
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().material = normalMaterial;
    }
}
