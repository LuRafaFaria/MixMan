using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistakeManager : MonoBehaviour
{
    public GameManager gameManager;


    public int mistakes;
    public int startMistakes = 3;

    [SerializeField] List<GameObject> mistakesList;

    // Start is called before the first frame update
    void Start()
    {
        mistakes = startMistakes;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveMistake()
    {
        mistakesList[mistakes-1].SetActive(false);
        mistakes--;

        if (mistakes == 0)
        {
            gameManager.EndGame();
        }

    }




}
