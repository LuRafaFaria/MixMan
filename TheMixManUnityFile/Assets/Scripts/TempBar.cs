using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBar : MonoBehaviour
{
    enum StatBar {Temp1, Temp2};
    [SerializeField] StatBar statBarTemp;

    RectTransform barRect;
    [SerializeField] float speed = 0.1f;
    bool achievedEnd = false;

    public float valueWhenPressed1 = 0;
    public float valueWhenPressed2 = 0;

    [SerializeField] float nerfMultiplier;

    public bool isReadingInput;


    // Start is called before the first frame update
    void Start()
    {
        barRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!achievedEnd)
        {
            barRect.sizeDelta += new Vector2(Time.deltaTime * speed, 0);

            if (barRect.sizeDelta.x >= 100)
            {
                achievedEnd = true;
            }
        }
        else
        {
            barRect.sizeDelta -= new Vector2(Time.deltaTime * speed, 0);
        
            if (barRect.sizeDelta.x <= 0)
            {
                achievedEnd = false;
            }
        }

        
        


    }


    private void FixedUpdate()
    {
        switch (statBarTemp)
        {
            case StatBar.Temp1:
                if (!isReadingInput && Input.GetKeyUp(KeyCode.Q))
                {
                    isReadingInput = true;
                    StartCoroutine(ReadInput());
                    StoreValueNowTemp1();
                }
                break;
            case StatBar.Temp2:
                if (!isReadingInput && Input.GetKeyUp(KeyCode.E))
                {
                    isReadingInput = true;
                    StartCoroutine(ReadInput());
                    StoreValueNowTemp2();
                }
                break;
        }
    }


    public IEnumerator ReadInput()
    {
        yield return new WaitForSeconds(0.1f);
        isReadingInput = false;
    }


    void StoreValueNowTemp1()
    {
        valueWhenPressed1 = barRect.sizeDelta.x / nerfMultiplier;
        Debug.Log("Temp1:" + valueWhenPressed1);
    }

    void StoreValueNowTemp2()
    {
        valueWhenPressed2 = barRect.sizeDelta.x / nerfMultiplier;
        Debug.Log("Temp2:" + valueWhenPressed2);
    }

}
