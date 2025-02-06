using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponentInChildren<CoinBlock>() != null)
            {
                child.GetComponentInChildren<CoinBlock>().GameRestart();
            }
            else if (child.GetComponentInChildren<QuestionBlock>() != null)
            {
                child.GetComponentInChildren<QuestionBlock>().GameRestart();
            }

        }
    }
}