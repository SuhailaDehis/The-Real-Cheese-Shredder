using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShreddingScript : MonoBehaviour
{
    #region Testing Variables
    public float decreaseMargin = 0.05f;// how much the cheese will decrease over speed
    float cheeseSpeed;
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
    public Text percentageText;
    public GameObject cheese;
    //Because calculating percentage based on distance on X axis is problematic
    //since the cheese moves in an inclined plane so the X keeps changing between up and down positions which causes the fill value to flicker
    //So we're going to use an empty object and move that on X only when the movement of the cheese itself is not at all used in calculations
    public GameObject pseudoCheeseForPercentageCalculation;
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
        //  float newY = this.gameObject.transform.localPosition.y
        pseudoCheeseForPercentageCalculation.transform.localPosition -= pseudoCheeseForPercentageCalculation.transform.up * (decreaseMargin * Time.deltaTime * cheeseSpeed);
        cheese.transform.localPosition -= cheese.transform.up * (decreaseMargin * Time.deltaTime * cheeseSpeed);

        // fill the progress bar
        FillProgressBarByTransfor();

        //if (this.transform.position.x > endPointObject.transform.position.x)
        //{
        //    GameManager.instance.GameOver();
        //}
    }

    void FillProgressBarByTransfor()
    {
        float distanceTravelled = Mathf.Abs(originalX - pseudoCheeseForPercentageCalculation.transform.position.x);
        float fillValue = distanceTravelled / distanceBetweenCheeseAndEndoint; // progressBarImage.rectTransform.rect.width;
        progressBarImage.fillAmount = fillValue;

        int percentage = (int)(progressBarImage.fillAmount * 100);

        percentageText.text = percentage + " %";
        if (progressBarImage.fillAmount == 1)
        {
            GameManager.CurrentState = GameStates.EndMenu;
        }
    }

    #endregion

    #region Unity Callbacks
    private void Start()
    {
        originalScale = pseudoCheeseForPercentageCalculation.transform.localScale.x;
        orginalZ = pseudoCheeseForPercentageCalculation.transform.position.z;
        originalY = pseudoCheeseForPercentageCalculation.transform.position.y;
        originalX = pseudoCheeseForPercentageCalculation.transform.position.x;
        distanceBetweenCheeseAndEndoint = Mathf.Abs(pseudoCheeseForPercentageCalculation.transform.position.x - endPointObject.transform.position.x);
    }
    private void Update()
    {
        //if (cheeseSpeed > 0 && this.transform.localScale.y > minimumScaleToFinish)
        //{
        //    ShredCheeseByScale();
        //}
        cheeseSpeed = Mathf.Abs(MovingHandler.currentSpeed);
        if (cheeseSpeed > 0 && progressBarImage.fillAmount<1)
        {
            ShredCheeseByTransform();
        }
    }
    #endregion
}