using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHandler : MonoBehaviour
{
    public float speed = 0.1f;
    public static Vector3 currentSpeed;
    public Transform Top;
    public Transform Bottom;
    Vector3 touchDelta;
    Vector3 temp;

    void Update()
    {
        Move();
    }
    public void Move()
    {

        if (Input.touchCount > 0)
        {
            touchDelta = Input.GetTouch(0).deltaPosition;
            currentSpeed = transform.forward * touchDelta.y * Time.deltaTime * speed;
            transform.position -= currentSpeed;
        }
        temp = transform.localPosition;
        temp.y = Mathf.Clamp(transform.localPosition.y, Top.localPosition.y, Bottom.localPosition.y);
        temp.x = Mathf.Clamp(transform.localPosition.x, Top.localPosition.x, Bottom.localPosition.x);
        temp.z = Mathf.Clamp(transform.localPosition.z, Top.localPosition.z, Bottom.localPosition.z);
        transform.position = temp;

    }


}
