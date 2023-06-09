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
    public float speed = 0.0f;          // 이동 속도
    public float jumpSpeed = 0.0f;      // 점프 속도
    public float high = 1f;             // 점프 높이 
    private Enemy_STATE state = Enemy_STATE.Idle;     // 플레이어 상태 
    private float baseYPos = 0.0f;                      // 플레이어 y 값
    private float targetYPos = 0.0f;                    // 플레이어 목표 y 값
    private bool isJumpFinish = false;                  // 점프 끝났나 체크 
    private bool isJump = false;
    public float timer;
    public float waitingTime = 1.0f;

    void Start()
    {
        state = Enemy_STATE.Run;
        baseYPos = transform.position.y;                // 기본 위치 설정
        targetYPos = baseYPos + high;                   // 목표 위치 설정 
        if (targetYPos < 0)
            targetYPos = 0.0f;
    }
    Vector3 temp;
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            // 점프 
            case Enemy_STATE.Jump:
                if (transform.position.y <= targetYPos && isJumpFinish == false)
                {
                    // 업 코드 
                    transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed, Camera.main.transform);
                }
                // 목적지 도착 체크   
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
                    // 다운 코드 
                    transform.Translate(Vector3.down * Time.deltaTime * (jumpSpeed + 2), Camera.main.transform);
                }
                // 이동 코드 
                temp = transform.position;
                temp.x += (speed - 1) * Time.deltaTime;
                transform.position = temp;
                break;
            // 달리기 
            case Enemy_STATE.Run:
                //이동 코드 
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