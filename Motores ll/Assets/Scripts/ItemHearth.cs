using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHearth : MonoBehaviour
{
    public int valueHaelth;
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
            collision.gameObject.GetComponent<ControllerPlayer>().IncreaseLife(valueHaelth);
            Destroy(gameObject, 0.1f);
        }
    }
}
