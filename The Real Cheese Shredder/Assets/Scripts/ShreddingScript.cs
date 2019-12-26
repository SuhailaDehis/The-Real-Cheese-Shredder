using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShreddingScript : MonoBehaviour
{
    #region Testing Variables
    public float decreaseMargin = 0.05f;// how much the cheese will decrease over speed
    public float cheeseSpeed = 5f;
    public float minimumScaleToFinish = 0.3f;
    #endregion

    #region Private Variables
    float originalScale = 2.5f;
    float orginalZ, originalY;

    #endregion


    #region public variables
    public GameObject endPointObject;
    #endregion

    #region Public Methods

    public void ShredCheeseByScale()
    {
        // implementation 1 , decrease the scale constantly * time.deltatime
        float newScale = this.gameObject.transform.localScale.y - (decreaseMargin * Time.deltaTime * cheeseSpeed);
        this.transform.localScale = new Vector3(originalScale, newScale, originalScale);
    }

    public void ShredCheeseByTransform()
    {
        // implementation 1 , decrease the scale constantly * time.deltatime
        float newX = this.gameObject.transform.position.x + (decreaseMargin * Time.deltaTime * cheeseSpeed);
        this.transform.position = new Vector3(newX, originalY, orginalZ);

        if(this.transform.position.x > endPointObject.transform.position.x)
        {
            GameManager.instance.GameOver();
        }
    }

    #endregion


    #region Unity Callbacks
    private void Start()
    {
        originalScale = this.transform.localScale.x;
        orginalZ = this.transform.position.z;
        originalY = this.transform.position.y;
    }
    private void Update()
    {
        //if (cheeseSpeed > 0 && this.transform.localScale.y > minimumScaleToFinish)
        //{
        //    ShredCheeseByScale();
        //}
        if (cheeseSpeed > 0 && this.transform.position.x < endPointObject.transform.position.x)
        {
            ShredCheeseByTransform();
        }
    }
    #endregion
}