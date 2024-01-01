using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ControllerPlayer>().respowCheck = transform.position;
            Cam CameraDois = Camera.main.GetComponent<Cam>();
            CameraDois.Respawn = CameraDois.transform.position;
        }
    }
}
