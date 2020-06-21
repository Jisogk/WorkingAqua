using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControll : MonoBehaviour {

    private PlayerAction m_Player;
    private Vector2 move_Direction;
    private bool isMouseDown = false;
    public List<Transform> Contacts = null;

    public enum PlayerDirection   //主角朝向的枚举变量
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    public PlayerDirection m_playerDirection;  //记录主角朝向

    struct AnimParameter
    {
        public bool isMoving;
        public float dir_x;
        public float dir_y;
    }

    // Use this for initialization
	void Start () {
        m_Player = GetComponent<PlayerAction>();
        m_playerDirection = PlayerDirection.DOWN;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //控制暂停
        if (PauseManager.instance.IsPause)
        {
            return;
        }

        //控制移动
        MouseDetect();
        move_Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

       if (isMouseDown)
        {
            move_Direction = MouseMove();
        }

        DirectionDetection(move_Direction);
        m_Player.Move(move_Direction);
        m_Player.AnimControll(AnimParameterDetect().isMoving,AnimParameterDetect().dir_x,AnimParameterDetect().dir_y);

        //控制交互触发
        if(Input.GetKeyDown(KeyCode.J)||Input.GetKeyDown(KeyCode.Mouse1))
        {
            foreach (Transform collider in Contacts)
            {
                if (collider.tag == "NPC")
                {
                    collider.gameObject.GetComponent<NPCAction>().TalkTrigger();
                }
            }
        }
	}

    private void DirectionDetection(Vector2 direction)
    {
        //检测角色面对的方向

        float angle = Vector2.SignedAngle(Vector2.right,direction);

        if (direction.magnitude < 0.1)
        { }
        else
        {
            if (angle < 45.0 && angle > -45.0)
            {
                m_playerDirection = PlayerDirection.RIGHT;
            }
            else if (angle < 135.0 && angle > 45.0)
            {
                m_playerDirection = PlayerDirection.UP;
            }
            else if (angle < -135.0 || angle > 135.0)
            {
                m_playerDirection = PlayerDirection.LEFT;
            }
            else if (angle < -45.0 && angle > -135.0)
            {
                m_playerDirection = PlayerDirection.DOWN;
            }
        }

    }

    private AnimParameter AnimParameterDetect()
    {
        //控制动画状态机的参数

        AnimParameter parameter;
        bool isMoving = false;
        float Dir_x = 0;
        float Dir_y = 0;

        if (m_Player.m_Rigidbody2D.velocity.magnitude > 1.0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        switch (m_playerDirection)
        {
            case PlayerDirection.DOWN:
                Dir_x = 0;
                Dir_y = -1.0f;
                break;
            case PlayerDirection.UP:
                Dir_x = 0;
                Dir_y = 1.0f;
                break;
            case PlayerDirection.RIGHT:
                Dir_x = 1.0f;
                Dir_y = 0;
                break;
            case PlayerDirection.LEFT:
                Dir_x = -1.0f;
                Dir_y = 0;
                break;
        }

        parameter.isMoving = isMoving;
        parameter.dir_x = Dir_x;
        parameter.dir_y = Dir_y;

        return parameter;
    }

    private void MouseDetect()
    {
        //检测鼠标是否按下

        if(Input.GetButtonDown("Fire1"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //是UI的时候，执行相关的UI操作
            }
            else
            {
                //不是UI层的时候，执行其它操作
                isMouseDown = true;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isMouseDown = false;
        }
    }

    private Vector2 MouseMove()
    {
        //根据鼠标点击位置确定移动方向

        Vector2 direction = Vector2.zero;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //Debug.Log(hit.collider.tag);

        direction = hit.point - new Vector2(this.transform.position.x, this.transform.position.y);

        return direction.normalized;
    }

}
