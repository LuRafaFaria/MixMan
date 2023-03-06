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
    // Start is called before the first frame update
    void Start()
    {
        l_I_Rect = l_I_Prog.GetComponent<RectTransform>(); 
        r_I_Rect = r_I_Prog.GetComponent<RectTransform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            l_I_Rect.sizeDelta = new Vector2(l_I_Rect.rect.width + 10, l_I_Rect.rect.height);
        else if(Input.GetKeyDown(KeyCode.E))
            r_I_Rect.sizeDelta = new Vector2(r_I_Rect.rect.width + 10, r_I_Rect.rect.height); ;
    }
}
