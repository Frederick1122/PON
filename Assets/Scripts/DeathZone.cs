using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public static DeathZone main;

    [Header("SpawnSettings")]
    [SerializeField] private GameObject firstPlayer;
    [SerializeField] private AttackEnergy firstAttackEnergy;
    [SerializeField] private GameObject secondPlayer;
    [SerializeField] private AttackEnergy secondAttackEnergy;

    private float rebirthTime;
    [HideInInspector] public List<Transform> SpawnsFirstPlayer = new List<Transform>();
    [HideInInspector] public List<Transform> SpawnsSecondPlayer = new List<Transform>();
    
    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(this);
            return;
        }

        main = this;
    }
    private void Start()
    {
        rebirthTime = DataHolder.main.rebirthTime;
        Spawn(SpawnPosition("Player1"), firstPlayer, true);
        Spawn(SpawnPosition("Player2"), secondPlayer, true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player1") && !other.CompareTag("Player2"))
        {
            return;
        }
        //other.gameObject.GetComponent<PlayerScript>().ResetRays();
        Respawn(other.gameObject);
    }

    public IEnumerator WaitCoroutine(GameObject player)
    {

        player.SetActive(false);
        yield return new WaitForSeconds(rebirthTime);
        Spawn(SpawnPosition(player.tag), player);


    }
    public Transform SpawnPosition(string playerTag)
    {
        if (playerTag == "Player1")
        {
            return (SpawnsFirstPlayer[Random.Range(0, SpawnsFirstPlayer.Count)]);
        }
        else
        {
            return (SpawnsSecondPlayer[Random.Range(0, SpawnsSecondPlayer.Count)]);
        }
    }
    public void Spawn(Transform spawn, GameObject player, bool start = false)
    {
        if (start)
        {
            GameObject g = Instantiate(player, spawn.position, Quaternion.identity);
            if (player.CompareTag("Player1"))
                g.GetComponent<PlayerScript>().attackEnergy = firstAttackEnergy;
            else
                g.GetComponent<PlayerScript>().attackEnergy = secondAttackEnergy;

        }
        else
        {
            player.GetComponent<PlayerScript>().ResetGravityForce();
            player.transform.position = spawn.position;
            player.SetActive(true);
        }
    }
    public void Respawn(GameObject player)
    {
        StartCoroutine(WaitCoroutine(player));
    }
    public void EraseSpawns()
    {
        SpawnsFirstPlayer.Clear();
        SpawnsSecondPlayer.Clear();
    }
}
