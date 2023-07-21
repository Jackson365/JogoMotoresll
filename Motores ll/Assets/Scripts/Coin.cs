using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreCoin;
    private AudioSource som;

    private void Awake()
    {
        som = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            som.Play();
            GameController.instance.UpdateScore(scoreCoin);
            Destroy(gameObject, 0.1f);
        }
    }
}
