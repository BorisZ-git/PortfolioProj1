using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STest : MonoBehaviour
{
    public GameObject target;
    public float speed;
    private bool flag;
    private void FixedUpdate()
    {
        if(flag)
        Vector3.MoveTowards(transform.position, target.transform.position,speed*Time.deltaTime);
    }
    private void SetFlag()
    {
        flag = true;
    }
}
