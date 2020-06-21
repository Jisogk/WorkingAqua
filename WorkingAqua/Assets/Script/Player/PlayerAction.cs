using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public float speed = 2.0f;

    public Rigidbody2D m_Rigidbody2D;
    private Animator m_Anim;
    //private PlayerControll m_PlayerControll;

    // Use this for initialization
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        //m_PlayerControll = GetComponent<PlayerControll>();
        m_Anim = this.transform.Find("PlayerPic").GetComponent<Animator>();
    }

    void Update()
    {
        if (PauseManager.instance.IsPause)
        {
            m_Anim.speed = 0f;
        }
        else
        {
            m_Anim.speed = 1f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void Move(Vector2 direction)
    {
        //控制主角的移动
        //输入为二维向量
        m_Rigidbody2D.velocity = speed * direction.normalized;
    }

    public void AnimControll(bool isMoving, float dir_x, float dir_y)
    {
        m_Anim.SetBool("isMoving", isMoving);
        m_Anim.SetFloat("X", dir_x);
        m_Anim.SetFloat("Y", dir_y);
    }
}
