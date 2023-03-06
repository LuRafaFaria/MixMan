using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBar : MonoBehaviour
{
    [SerializeField]GameObject l_I_Prog;
    [SerializeField]GameObject r_I_Prog;
    [SerializeField]GameObject goal;

    RectTransform l_I_Rect;
    RectTransform r_I_Rect;
    RectTransform goalRect;


    float max_L_I_Prog;
    float max_R_I_Prog;

    float current_L_I_Prog;
    float current_R_I_Prog;

    float l_I_Overshoot;
    float r_I_Overshoot;
    // Start is called before the first frame update
    void Start()
    {
        l_I_Rect = l_I_Prog.GetComponent<RectTransform>(); 
        r_I_Rect = r_I_Prog.GetComponent<RectTransform>(); 
        goalRect = goal.GetComponent<RectTransform>();

        max_L_I_Prog = goalRect.anchoredPosition.x + goalRect.pivot.x  * goalRect.rect.width;
        max_R_I_Prog = goalRect.anchoredPosition.x + goalRect.pivot.x  * goalRect.rect.width;

        current_L_I_Prog = l_I_Rect.rect.width;
        current_R_I_Prog = r_I_Rect.rect.width;

        l_I_Overshoot = 0;
        r_I_Overshoot = 0;
       

    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundaries();
        CheckOvershoot();
        

        if (Input.GetKeyDown(KeyCode.Q))
        {
            l_I_Rect.sizeDelta = new Vector2(l_I_Rect.rect.width + 5, l_I_Rect.rect.height);
            current_L_I_Prog = l_I_Rect.rect.width;
        }            
        else if (Input.GetKeyDown(KeyCode.E))
        {
            r_I_Rect.sizeDelta = new Vector2(r_I_Rect.rect.width + 5, r_I_Rect.rect.height);
            current_R_I_Prog = r_I_Rect.rect.width;
        }
             
    }

    void CheckOvershoot()
    {
        if (current_L_I_Prog > max_L_I_Prog)
        {
            l_I_Overshoot = current_L_I_Prog - max_L_I_Prog;

            Debug.Log(l_I_Overshoot);
        }

        if (current_R_I_Prog > max_R_I_Prog)
        {
            r_I_Overshoot = current_R_I_Prog - max_R_I_Prog;
            Debug.Log(r_I_Overshoot);
        }
    }

    void CheckBoundaries()
    {
        if(l_I_Rect.rect.width >= 100)
        {
            l_I_Rect.sizeDelta = new Vector2(99, l_I_Rect.rect.height);
            r_I_Rect.sizeDelta = new Vector2(1, r_I_Rect.rect.height);
        }else if(r_I_Rect.rect.width >= 100)
        {
            l_I_Rect.sizeDelta = new Vector2(1, l_I_Rect.rect.height);
            r_I_Rect.sizeDelta = new Vector2(99, r_I_Rect.rect.height);
        }
    }
}
