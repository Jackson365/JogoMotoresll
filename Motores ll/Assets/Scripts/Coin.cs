using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreCoin;
    
    public AudioSource caxixi;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            caxixi.Play();
            GameController.instance.UpdateScore(scoreCoin);
            Destroy(gameObject, 0.4f);
        }
    }
}
