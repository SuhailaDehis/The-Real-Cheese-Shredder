using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHandler : MonoBehaviour
{
    public float speed = 0.1f;
    public static float currentSpeed;
    public Transform startPoint;
    public float maxDistance;
    Vector3 touchDelta;
    Vector3 temp;
    float distance;
    Vector3 tempPos;
    float deltaObjectDistance;
    float directionalAngle;
    public static bool canMove;
    private void Awake()
    {
        GameManager.CheckMyStates += CheckGameState;
    }
    void Update()
    {
        Move();
    }
    public void Move()
    {
        if (Input.touchCount > 0 && canMove)
        {
            touchDelta = Input.GetTouch(0).deltaPosition;
            touchDelta.y = Mathf.Clamp(touchDelta.y, -20, 20);
            distance = transform.position.y- startPoint.position.y;
            currentSpeed = 0;
            if (touchDelta.y > 0 && distance < maxDistance)
            {
                currentSpeed = -touchDelta.y * Time.deltaTime * speed;

            }
           else  if (touchDelta.y < 0 && distance > 0.4f)
            {
                currentSpeed = -touchDelta.y * Time.deltaTime * speed;

            }
            transform.position += transform.forward * currentSpeed;

        }
        else
        {
            currentSpeed = 0;
        }
    }
    void CheckGameState(GameStates currentState)
    {
        if (currentState==GameStates.Playing)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }
}

