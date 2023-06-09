using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3 : MonoBehaviour
{
    public float speed = 5.0f;
    private bool isJump = false;
    public float timer;
    public float waitingTime = 1.0f;

    void Update()
    {
        float EnemyMove = Time.deltaTime * speed;
        transform.Translate(Vector3.left * EnemyMove);
        if (transform.position.x < -9.0f) Destroy(this.gameObject);

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

        if (isJump == true && transform.position.x < -5.2f && transform.position.x > -7.3f)
        {
            GameManager.Instance.GameOver();
        }
        if (GameManager.Instance.isOver == true)
        {
            speed = 0.0f;
        }
    }
}
