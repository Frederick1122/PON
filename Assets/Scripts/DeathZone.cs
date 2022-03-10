using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public static DeathZone main;
    [SerializeField] private float rebirthTime;
    /*[HideInInspector]*/
    public List<Transform> SpawnsFirstPlayer = new List<Transform>();
    /*[HideInInspector]*/
    public List<Transform> SpawnsSecondPlayer = new List<Transform>();
    [SerializeField] private GameObject firstPlayer;
    [SerializeField] private GameObject secondPlayer;


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
            Instantiate(player, spawn.position, Quaternion.identity);
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
