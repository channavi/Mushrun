using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Enemy_STATE
{
    Idle = 0,
    Jump = 1,
    Run = 2
}

public class PlayerController : MonoBehaviour
{
    public GameManager gm;
    public float speed = 0.0f;          // �̵� �ӵ�
    public float jumpSpeed = 0.0f;      // ���� �ӵ�
    public float high = 1f;             // ���� ���� 
    private Enemy_STATE state = Enemy_STATE.Idle;     // �÷��̾� ���� 
    private float baseYPos = 0.0f;                      // �÷��̾� y ��
    private float targetYPos = 0.0f;                    // �÷��̾� ��ǥ y ��
    private bool isJumpFinish = false;                  // ���� ������ üũ 
    private bool isJump = false;
    public float timer;
    public float waitingTime = 1.0f;

    void Start()
    {
        state = Enemy_STATE.Run;
        baseYPos = transform.position.y;                // �⺻ ��ġ ����
        targetYPos = baseYPos + high;                   // ��ǥ ��ġ ���� 
        if (targetYPos < 0)
            targetYPos = 0.0f;
    }
    Vector3 temp;
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            // ���� 
            case Enemy_STATE.Jump:
                if (transform.position.y <= targetYPos && isJumpFinish == false)
                {
                    // �� �ڵ� 
                    transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed, Camera.main.transform);
                }
                // ������ ���� üũ   
                else if ((int)transform.position.y == (int)targetYPos && isJumpFinish == false)
                {
                    isJumpFinish = true;
                }
                else if (isJumpFinish == true)
                {
                    if ((int)transform.position.y == (int)baseYPos)
                    {
                        state = Enemy_STATE.Run;
                        isJumpFinish = false;
                    }
                    // �ٿ� �ڵ� 
                    transform.Translate(Vector3.down * Time.deltaTime * (jumpSpeed + 2), Camera.main.transform);
                }
                // �̵� �ڵ� 
                temp = transform.position;
                temp.x += (speed - 1) * Time.deltaTime;
                transform.position = temp;
                break;
            // �޸��� 
            case Enemy_STATE.Run:
                //�̵� �ڵ� 
                temp = transform.position;
                temp.x -= (speed) * Time.deltaTime;
                transform.position = temp;
                break;




        }
        if (isJumpFinish == false)
        {
            state = Enemy_STATE.Jump;
        }
        if (transform.position.x < -10.0f) Destroy(this.gameObject);

        if (isJump == true) timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            isJump = false;
            timer = 0;
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            isJump = true;
        }

        if (isJump == true && transform.position.x < -5.0f && transform.position.x > -7.5f)
        {
            GameManager.Instance.GameOver();
        }

        if(GameManager.Instance.isOver == true)
        {
            state = Enemy_STATE.Idle;
        }
    }
}