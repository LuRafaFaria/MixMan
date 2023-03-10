using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigBar : MonoBehaviour
{
    //TODO: Temporario, modificar depois para o igrediente nos so
    public enum Difficulty { Easy, Medium, Hard};
    
    public Difficulty difficulty;

    MistakeManager mistakeManager;

    [SerializeField]GameObject l_I_Prog;
    [SerializeField]GameObject r_I_Prog;
    [SerializeField]GameObject goal;


    [SerializeField] GameObject tempBar1;
    [SerializeField] GameObject tempBar2;
    [SerializeField] GameObject qSquare;
    [SerializeField] GameObject eSquare;


    RectTransform l_I_Rect;
    RectTransform r_I_Rect;
    RectTransform goalRect;

    public bool bothInGoal;

    public GameManager gameManager;

    float max_L_I_Prog;
    float max_R_I_Prog;

    float min_L_I_Prog;
    float min_R_I_Prog;

    float current_L_I_Prog;
    float current_R_I_Prog;

    float l_I_Overshoot;
    float r_I_Overshoot;

    bool finishedMix;

    int currentMixInp;
    int mixCounter;



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        bothInGoal = false;
        l_I_Rect = l_I_Prog.GetComponent<RectTransform>(); 
        r_I_Rect = r_I_Prog.GetComponent<RectTransform>(); 
        goalRect = goal.GetComponent<RectTransform>();

        l_I_Prog.transform.localPosition = new Vector3(GetComponent<RectTransform>().rect.width / -2, 0, 1);

        r_I_Prog.transform.localPosition = new Vector3(GetComponent<RectTransform>().rect.width / 2, 0, 1);


        RandomizeGoalWidth();

        max_L_I_Prog = goalRect.anchoredPosition.x + goalRect.pivot.x  * goalRect.rect.width;
        max_R_I_Prog = goalRect.anchoredPosition.x + goalRect.pivot.x  * goalRect.rect.width;

        min_L_I_Prog = goalRect.anchoredPosition.x - goalRect.pivot.x * goalRect.rect.width;
        min_R_I_Prog = goalRect.anchoredPosition.x - goalRect.pivot.x * goalRect.rect.width;

        current_L_I_Prog = l_I_Rect.rect.width;
        current_R_I_Prog = r_I_Rect.rect.width;

        l_I_Overshoot = 0;
        r_I_Overshoot = 0;

        mixCounter = 0;

        mistakeManager = GameObject.Find("MistakesHolder").GetComponent<MistakeManager>();
    }

    IEnumerator Wait1FrameQ() 
    {
        yield return null;
        Debug.Log("coisou1");

        l_I_Rect.sizeDelta = new Vector2(l_I_Rect.rect.width + tempBar1.GetComponent<TempBar>().valueWhenPressed1, l_I_Rect.rect.height);
        current_L_I_Prog = l_I_Rect.rect.width;
        CheckOvershoot(KeyCode.Q);
    }

    IEnumerator Wait1FrameE()
    {
        yield return null;
        Debug.Log("coisou2");
        r_I_Rect.sizeDelta = new Vector2(r_I_Rect.rect.width + tempBar2.GetComponent<TempBar>().valueWhenPressed2, r_I_Rect.rect.height);
        current_R_I_Prog = r_I_Rect.rect.width;
        CheckOvershoot(KeyCode.E);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!bothInGoal)
        {
            CheckGoal();
            CheckBoundaries();
        }
        else
        {

        }
        
        

             
    }


    private void FixedUpdate()
    {
        
        if (!bothInGoal)
        {
            if (tempBar1.GetComponent<TempBar>().isReadingInput && Input.GetKeyUp(KeyCode.Q))
            {
                StartCoroutine(Wait1FrameQ());
            }
            if (tempBar2.GetComponent<TempBar>().isReadingInput && Input.GetKeyUp(KeyCode.E))
            {
                StartCoroutine(Wait1FrameE());
            }

        }
        else
        {
            MixMaterials();
        }

    }


    void RandomizeGoalWidth()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                goalRect.sizeDelta = new Vector2(Random.Range(15, 20), goalRect.rect.height);
                break;
            case Difficulty.Medium:
                goalRect.sizeDelta = new Vector2(Random.Range(10, 15), goalRect.rect.height);

                break;
            case Difficulty.Hard:
                goalRect.sizeDelta = new Vector2(Random.Range(5, 10), goalRect.rect.height);

                break;
        }
    }


    void MixMaterials()
    {
        if (mixCounter < 8)
        {

            if (currentMixInp == 1)
            {
                qSquare.GetComponent<Image>().color = Color.green;
                eSquare.GetComponent<Image>().color = Color.white;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    currentMixInp = 2;
                    mixCounter++;
                }

            }
            else if (currentMixInp == 2)
            {
                qSquare.GetComponent<Image>().color = Color.white;
                eSquare.GetComponent<Image>().color = Color.green;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentMixInp = 1;
                    mixCounter++;
                }

            }
        }
        else
        {
            Debug.Log("aha you won, loser");
            gameManager.WinLevel();
        }
    }


    void CheckOvershoot(KeyCode keyCode)
    {
        bool isOver100 = current_L_I_Prog + current_R_I_Prog > gameObject.GetComponent<RectTransform>().rect.width;

        if (isOver100 && keyCode == KeyCode.Q)
        {
            mistakeManager.RemoveMistake();
            r_I_Rect.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width - current_L_I_Prog, r_I_Rect.rect.height);
            l_I_Overshoot =  current_L_I_Prog - max_L_I_Prog;
            if(current_L_I_Prog > current_R_I_Prog)
            r_I_Overshoot = 0;
        }

        if (isOver100 && keyCode == KeyCode.E)
        {
            mistakeManager.RemoveMistake();
            l_I_Rect.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width - current_R_I_Prog, l_I_Rect.rect.height);
            r_I_Overshoot = current_R_I_Prog - max_R_I_Prog;
            if(current_R_I_Prog > current_L_I_Prog)
            l_I_Overshoot = 0;
        }

        current_L_I_Prog = l_I_Rect.rect.width;
        current_R_I_Prog = r_I_Rect.rect.width;
    }

    void CheckBoundaries()
    {
        if(l_I_Rect.rect.width >= gameObject.GetComponent<RectTransform>().rect.width)
        {
            l_I_Rect.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width - 1, l_I_Rect.rect.height);
            r_I_Rect.sizeDelta = new Vector2(1, r_I_Rect.rect.height);
        }else if(r_I_Rect.rect.width >= gameObject.GetComponent<RectTransform>().rect.width)
        {
            l_I_Rect.sizeDelta = new Vector2(1, l_I_Rect.rect.height);
            r_I_Rect.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().rect.width - 1, r_I_Rect.rect.height);
        }
    }

    void CheckGoal()
    {
        if((current_L_I_Prog >= min_L_I_Prog && current_L_I_Prog <= max_L_I_Prog)
            &&
            (current_R_I_Prog >= min_R_I_Prog && current_R_I_Prog <= max_R_I_Prog)
            )
        {
            bothInGoal = true;
            goal.GetComponent<Image>().color = Color.green;
            currentMixInp = 1;
        }
    }
}
