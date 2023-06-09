using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleControl : MonoBehaviour
{
    public int speed;
    private bool isStart = false;
    void Start()
    {
        speed = 4;
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
          isStart = true;
        }
        if(isStart ==true) transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (transform.position.x < -20) speed = 0;
    }
}
