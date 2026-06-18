using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform enemyObject;
    public BattleUI b;
    public List<GameObject> enemyList;
    public float spawnInterval;
    public int maxAmount;
    public float timer = 0;
    public Player player;

    private void FixedUpdate()
    {
        if(b.isTime && player.playerDead == false)
        {
            timer += Time.fixedDeltaTime;
            if(timer > spawnInterval)
            {
                timer = 0;
                Spawn();
               
            }
        }
    }

    public void Spawn()
    {
        if(enemyObject.childCount < maxAmount)
        {
            Instantiate(RandomSpawn(), RandomPoint().position, Quaternion.Euler(45, 0, 0), enemyObject);
        }
    }

    public GameObject RandomSpawn()
    {
        int randomEnemy = Random.Range(0, enemyList.Count);
        return enemyList[randomEnemy];
    }

    public Transform RandomPoint()
    {
        int randomPoint = Random.Range(0, transform.childCount);
        return transform.GetChild(randomPoint);
    }

}
