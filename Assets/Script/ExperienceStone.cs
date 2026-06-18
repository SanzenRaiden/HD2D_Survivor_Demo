using UnityEngine;

public class ExperienceStone : MonoBehaviour
{
    private Player player;
    private int playerLayer;
    public bool toPlayer = false;
    private MusicManagement Music;

    private void OnEnable()
    {
        player = GameObject.Find("PlayerObject").transform.GetChild(0).GetComponent<Player>();
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        Music = GameObject.Find("MusicManagement").GetComponent<MusicManagement>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Music.PlayGainObjectAudio();
            Player isPlayer = collision.gameObject.GetComponent<Player>();
            player.currentExperience += 5;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, player.gainRadius, playerLayer);
        if(colliders.Length > 0)
        {
            toPlayer = true;
        }
        //飞向玩家
        if (toPlayer)
        {
            Vector3 target = player.transform.position;
            Vector3 distance = target - transform.position;
            Vector3 moveToPlayer = new Vector3(distance.x, 0, distance.z).normalized * 10;
            transform.position += moveToPlayer * Time.deltaTime;
        }
    }
}
