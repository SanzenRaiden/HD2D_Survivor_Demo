using TMPro;
using UnityEngine;
using System.Collections;

public class BasalEnemy : BasalStats
{
    public GameObject damageNumber;
    public EnemyState enemyState;
    private GameObject chasingTarget;
    private Transform playerObject;
    private Animator anim;
    public Material normalMaterial;
    public Material hitedMaterial;
    public GameObject dropObject;
    private float begintransform = 5f;
    public AudioClip enemyHitedAudio;

    public enum EnemyState
    {
        Idle,
        Moving,
        Die,
    }

    void Awake()
    {
        playerObject = GameObject.Find("PlayerObject").transform;
        anim = GetComponent<Animator>();
    }

    //碰撞检测，对玩家造成伤害
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.currentHealth > 0)
            {
                player.currentHealth -= attack;
                GameObject text = Instantiate(damageNumber, other.transform.position, default);
                text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = attack.ToString();
                other.gameObject.GetComponent<Player>().StartHitedback();
                if (player.currentHealth <= 0)
                {
                    player.Dying();
                }

            }
        }
    }
    
    //受击特效
    public void StartHitedback()
    {
        if (enemyHitedAudio != null)
        {
            GetComponent<AudioSource>().clip = enemyHitedAudio;
            GetComponent<AudioSource>().volume = GameObject.Find("MusicManagement").GetComponent<MusicManagement>().SEvolume;
            GetComponent<AudioSource>().Play();
        }
        StartCoroutine(Hitfeedback());
    }

    public IEnumerator Hitfeedback()
    {
        GetComponent<SpriteRenderer>().material = hitedMaterial;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().material = normalMaterial;
    }

    //死亡事件
    public void Dying()
    {
        if (enemyState != EnemyState.Die)
        {
            enemyState = EnemyState.Die;
            Instantiate(dropObject, transform.position, Quaternion.Euler(45, 0, 0));
            anim.SetTrigger("died");
            StartCoroutine(DestroySelf());
        }
    }

    public IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    //追击玩家
    public void GetTargetRole()
    {
        float chaseDistance = 999;
        Transform currentChaeing = null;

        if(playerObject.childCount > 0)
        {
            foreach(Transform item in playerObject)
            {
                float distance = Vector3.Distance(item.position, transform.position);//怪物自身和玩家的距离

                if(distance < chaseDistance)            //比较距离，追击最近的玩家
                {
                    chaseDistance = distance;
                    currentChaeing = item;
                }                                                   
            }
            chasingTarget = currentChaeing.gameObject;
        }
    }

    void FixedUpdate()
    {
        if(playerObject.GetChild(0).GetComponent<Player>().playerDead == false)
        {
            //处理敌人状态机，更改敌人动作
            switch (enemyState)
            {
                case EnemyState.Idle:
                    anim.SetBool("isMoving", false);

                    if (chasingTarget == null)
                    {
                        GetTargetRole();
                    }
                    else
                    {
                        enemyState = EnemyState.Moving;
                    }
                    break;

                case EnemyState.Moving:
                    anim.SetBool("isMoving", true);

                    if (chasingTarget == null)
                    {
                        enemyState = EnemyState.Idle;
                    }
                    else
                    {
                        Vector3 targetPosition = chasingTarget.transform.position;//目标坐标
                        Vector3 distance = targetPosition - transform.position;//移动向量

                        //动画翻转逻辑
                        if (targetPosition.x < transform.position.x)
                        {
                            transform.localScale = new Vector3(-1 * begintransform, begintransform, begintransform);
                        }
                        else if (targetPosition.x > transform.position.x)
                        {
                            transform.localScale = new Vector3(begintransform, begintransform, begintransform);
                        }

                        Vector3 move = new Vector3(distance.x, 0, distance.z).normalized * moveSpeed;
                        transform.position += move * Time.fixedDeltaTime;
                    }
                    break;

                case EnemyState.Die:

                    break;
            }
        }
        
    }

}
