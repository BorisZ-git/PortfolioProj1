using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourGameScene : MonoBehaviour
{
    public Camera cam;
    public GameObject[] Enemy;
    public GameObject[] Bonus;
    public GameObject[] Ammo;
    public Transform[] SpawnPointsEnemy;
    public Transform[] SpawnPointsBonus;
    public Transform[] SpawnPointsAmmo;

    private float spawnTime = 3f;
    private GameObject player;
    #region CameraFollow
    public Vector3 distanceFromPlayer;
    private Vector3 positionToGo;
    #endregion
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        InvokeRepeating("EnemySet", spawnTime, spawnTime);
        InvokeRepeating("BonusSet", spawnTime * 6, spawnTime * 6);
        InvokeRepeating("AmmoSet", spawnTime * 3, spawnTime * 4);
    }
    private void FixedUpdate()
    {
        CamLookToPlayer();
    }
    private void CamLookToPlayer()
    {
        positionToGo = player.transform.position + distanceFromPlayer; 
        cam.transform.position = Vector3.Lerp(cam.transform.position, positionToGo, 1.25F * Time.deltaTime);
    }
    private void Spawn(GameObject obj, Transform[] arr)
    {
        int r = Random.Range(0, arr.Length);
        Instantiate(obj, arr[r]);
    }
    private void EnemySet()
    {
        Spawn(Enemy[Random.Range(0, Enemy.Length)], SpawnPointsEnemy);
    }
    private void BonusSet()
    {
        int v = Random.Range(0, 3);
        Spawn(Bonus[v], SpawnPointsBonus);
    }
    private void AmmoSet()
    {
        int v = Random.Range(0, 10);
        if (v <= 8)
            Spawn(Ammo[0], SpawnPointsAmmo);
        if (v > 8)
            Spawn(Ammo[1], SpawnPointsAmmo);
    }

}
