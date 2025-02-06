using Unity.VisualScripting;
using UnityEngine;

public class CoinBlock : MonoBehaviour


{
    private bool collected = false;
    public GameObject coin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (collected == false)
        {
            collected = true;
            coin.GetComponent<Animator>().SetTrigger("OnCollision");
            coin.GetComponent<AudioSource>().Play();
        }
    }
    public void GameRestart()
    {
        collected = false;
    }
}
