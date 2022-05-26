using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SEnemy : MonoBehaviour
{
    public bool suicide, tower, distance, soldier, elite, boss;
    public bool die;
    private bool attack;

    public Animator animControl;
    public int score;
    public ParticleSystem explosion;

    private GameObject player;
    public GameObject bullet;
    public GameObject turret;

    public float speed, attackSpeed, damage;
    private float timer;
    private int value;

    private NavMeshAgent nav;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        attack = false; die = false;
    }
    private void Start()
    {
       value = SDifficulty.SetDifficulty(0,1,3);
    }
    private void FixedUpdate()
    {
        if (!tower & !die)
        {
            transform.LookAt(player.transform);
            if (!attack)
                nav.SetDestination(player.transform.position);
            if (attack)
                nav.SetDestination(transform.position);                    
        }
        if (die)
            nav.enabled = false;
    }
    public void AddScore()
    {
        UIGameScene.score += score + value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") & suicide)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") & !die)
        {
            if (tower)
                turret.transform.LookAt(player.transform);
            attack = true;
            timer += Time.deltaTime;
            if (timer > attackSpeed)
                Attack();
            if (animControl != null & !suicide)
            {
                animControl.SetBool("IsWalk", false);
                animControl.SetBool("IsFight", true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = 0f;
            attack = false;
            if (animControl != null)
            {
                animControl.SetBool("IsWalk", true);
                animControl.SetBool("IsFight", false);
            }
        }
    }
    void Attack()
    {
        timer = 0f;
        if (soldier)
            MeleeAttack();
        if (tower || distance || elite)
            DistanceAttack();
    }
    void MeleeAttack()
    {
        player.GetComponent<SCharacter>().TakeDamage(damage);
    }
    void DistanceAttack()
    {
        if (tower)
            Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        else 
        {
            Vector3 pos = transform.position;
            pos.Set(transform.position.x, 1f, transform.position.z);
            Instantiate(bullet, pos, transform.rotation);
        }
    }
}
