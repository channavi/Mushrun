using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATE
{
    Idle = 0,
    Jump = 1
}
public class PlayerJump : MonoBehaviour
{
    public GameManager gm;
    public float speed = 0.0f;
    public float jumpSpeed = 0.0f;
    public float high = 1f;
    private PLAYER_STATE state = PLAYER_STATE.Idle;
    private float baseYPos = 0.0f;
    private float targetYPos = 0.0f;
    private bool isJumpFinish = false;
    void Start()
    {
        state = PLAYER_STATE.Idle;
        baseYPos = transform.position.y;
        targetYPos = baseYPos + high;
        if (targetYPos < 0)
            targetYPos = 0.0f;
    }

    Vector3 temp;
    void Update()
    {
        switch (state)
        {
            case PLAYER_STATE.Jump:
                if (transform.position.y <= targetYPos && isJumpFinish == false)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed, Camera.main.transform);
                }
                else if ((int)transform.position.y == (int)targetYPos && isJumpFinish == false)
                {
                    isJumpFinish = true;
                }
                else if (isJumpFinish == true)
                {
                    if ((int)transform.position.y == (int)baseYPos)
                    {
                        state = PLAYER_STATE.Idle;
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
            case PLAYER_STATE.Idle:
                gm.isJump = false;
                break;
        }
        // 마우스 클릭 
        if (Input.GetMouseButtonDown(0) == true)
        {
            gm.isJump = true;
            state = PLAYER_STATE.Jump;
        }
        
        if (gm.isOver == true) 
        {
            jumpSpeed = 0.0f;
        }
    }
}
