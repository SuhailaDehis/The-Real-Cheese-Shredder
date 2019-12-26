using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShreddingScript : MonoBehaviour
{
    #region Testing Variables
    public float decreaseMargin = 0.05f;// how much the cheese will decrease over speed
    public float cheeseSpeed = 5f;
    public float minimumScaleToFinish = 0.3f;
    #endregion

    #region Private Variables
    float originalScale = 2.5f;
    float orginalZ, originalY, originalX;
    float distanceBetweenCheeseAndEndoint;
    #endregion


    #region public variables
    public GameObject endPointObject; // the end point that the cheese reaches to stop shredding
    public Image progressBarImage;
    #endregion

    #region Private Methods

    void ShredCheeseByScale()
    {
        // implementation 1 , decrease the scale constantly * time.deltatime
        float newScale = this.gameObject.transform.localScale.y - (decreaseMargin * Time.deltaTime * cheeseSpeed);
        this.transform.localScale = new Vector3(originalScale, newScale, originalScale);
    }

    void ShredCheeseByTransform()
    {
        // implementation 2 , move the cheese inside the gritter until its unseen
        float newX = this.gameObject.transform.position.x + (decreaseMargin * Time.deltaTime * cheeseSpeed);
        this.transform.position = new Vector3(newX, originalY, orginalZ);

        // fill the progress bar
        FillProgressBarByTransfor();

        if (this.transform.position.x > endPointObject.transform.position.x)
        {
            GameManager.instance.GameOver();
        }
    }

    void FillProgressBarByTransfor()
    {
        float distanceTravelled = Mathf.Abs(originalX - this.transform.position.x);
        Debug.Log("distance travelled > " + distanceTravelled);
        Debug.Log("distance between cheese and end point > "+ distanceBetweenCheeseAndEndoint);
        float fillValue = distanceTravelled / distanceBetweenCheeseAndEndoint; // progressBarImage.rectTransform.rect.width;

        Debug.Log("Fill value > " + fillValue);
        progressBarImage.fillAmount = fillValue;
        Debug.Log("____________________________________________");

    }
    

    #endregion


    #region Unity Callbacks
    private void Start()
    {
        originalScale = this.transform.localScale.x;
        orginalZ = this.transform.position.z;
        originalY = this.transform.position.y;
        originalX = this.transform.position.x;
        distanceBetweenCheeseAndEndoint = Mathf.Abs(this.transform.position.x - endPointObject.transform.position.x);
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