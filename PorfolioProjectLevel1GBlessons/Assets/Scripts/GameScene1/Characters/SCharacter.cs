using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCharacter : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animControl;
    public float hp, sink;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public GameObject parent;
    public GameObject bomb;
    private UIGameScene ui;
    private bool IsSink = false;
    private float timer;
    private int value;
    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UIGameScene").GetComponent<UIGameScene>();
        value = SDifficulty.SetDifficulty(0, 20, 40);
        hp += value;
    }
    private void FixedUpdate()
    {
        if (IsSink)
        {
            timer += Time.deltaTime;
            if (timer > sink)
                Death(sink);
        }
    }
    public void TakeDamage(float value)
    {
        hp -= value;
        if (hp > 0 && gameObject.CompareTag("Player"))
        {
            ui.ShowEffect(new Color(1f, 0f, 0f, 0.3f));
            audioSource.Play();
        }
        if (!IsSink)
        {
            if (animControl != null)
            {
                animControl.SetTrigger("IsGetHit");
            }
            if (hp <= 0)
            {
                if (animControl != null)
                {
                    animControl.SetTrigger("IsDie");
                    animControl.SetBool("IsWalk", false);
                    Dying();
                }
                else
                    Death(sink);
            }
        }
    }
    public void TakeHeal(float value)
    {
        hp += value;
        if (gameObject.CompareTag("Player"))
            ui.ShowEffect(new Color(0f, 1f, 0f, 0.3f));
        if (hp > 100 && gameObject.CompareTag("Player")) 
        {
            hp = 100f;
        }
    }
    private void Death(float t)
    {
        timer = -1f;
        SPlayer player = gameObject.GetComponent<SPlayer>();
        SEnemy enemy = gameObject.GetComponent<SEnemy>();
        if (parent != null)
        {
            parent.GetComponent<SEnemy>().AddScore();
            Destroy(parent, t);
        }
        if (enemy != null)
        {
            enemy.AddScore();
            if (enemy.suicide)
                Instantiate(bomb, transform.position, transform.rotation);
        }
        if (player != null)
            ui.GameOver();
        if (rb != null)
            rb.useGravity = true;
        if(player == null)
            Destroy(gameObject,t);        
    }
    private void Dying()
    {
        IsSink = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        if (parent != null)
        {
            if (gameObject.GetComponent<SEnemyElite>() != null)
                gameObject.GetComponent<SEnemyElite>().enabled = false;
            gameObject.GetComponentInParent<SEnemy>().die = true;
        }
        else
        {
            gameObject.GetComponent<SEnemy>().die = true;
        }
    }
}
