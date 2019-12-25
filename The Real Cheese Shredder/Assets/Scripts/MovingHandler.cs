using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHandler : MonoBehaviour
{
    public float sideSpeed = 0.1f;
   // public LevelData levelData;
   // public PlayerSavedData playerData;
    public Transform roadRight;
    public Transform roadLeft;
   // MyData currentLevelData;
    Vector3 touchDelta;
    Vector3 temp;
    float offset;
    private void Awake()
    {
    //    currentLevelData = levelData.GetCurrentLevel(playerData.actualLevel);
    }
    void Update()
    {
        Move();
    }
    public void Move()
    {
       // if (GameStateOwner.GameState == GameState.Play)
       // {
            if (Input.touchCount > 0)
            {
                touchDelta = Input.GetTouch(0).deltaPosition;
                transform.position += Vector3.right * touchDelta.x * Time.deltaTime * sideSpeed;
            }
         //   transform.position += Vector3.forward * currentLevelData.cheeseSpeed * Time.deltaTime;
            temp = transform.position;
            offset = 1.5f * transform.localScale.x;
            temp.x = Mathf.Clamp(transform.position.x, roadLeft.position.x + offset, roadRight.position.x - offset);
            transform.position = temp;
       // }
    }
 

}
