using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnemyElite : MonoBehaviour
{
    public Transform parentTransform;
    public ParticleSystem tail;
    public float dodgeTime;
    bool dodge,jump;
    float timerDodge, timerJump, speed, degree;
    Vector3 dodgeDir;
    #region Ray
    Ray ray;
    RaycastHit raycastHit;
    GameObject player;
    public LayerMask layerMask;
    #endregion
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Gun");
        dodge = true; jump = false;
    }
    private void Update()
    {
        if (!dodge & !jump)
            RestoreDodge();
        if (dodge)
            CheckDodge();
    }
    private void FixedUpdate()
    {
        if (jump)
            Jump();
    }
    private void RestoreDodge()
    {
        timerDodge += Time.deltaTime;
        if (timerDodge > dodgeTime)
        {
            dodge = true;
            timerDodge = 0f;
        }
    }
    private void CheckDodge()
    {
        ray.origin = player.transform.position;
        ray.direction = player.transform.forward;
        if (Physics.Raycast(ray, out raycastHit, 100f, layerMask))        
            if (raycastHit.collider.gameObject == gameObject)            
                Dodge();               
    }
    private void Jump()
    {
        timerDodge = 0f;
        timerJump += Time.deltaTime;
        parentTransform.Translate(dodgeDir * speed * Time.deltaTime);
        if(timerJump > 0.15f)
            speed -= degree;
        if (timerJump > 0.6f)
        {
            timerJump = 0f;
            jump = false;
            tail.Stop();
        }
    }
    private void Dodge()
    {
        degree = Random.Range(1, 6);
        speed = 50f;
        dodge = false;
        jump = true;
        TakeJumpDir();
        tail.Play();
    }
    private void TakeJumpDir()
    {
        if (Random.Range(0, 2) == 1)
            dodgeDir = Vector3.left;
        else
            dodgeDir = Vector3.right;
    }
}
