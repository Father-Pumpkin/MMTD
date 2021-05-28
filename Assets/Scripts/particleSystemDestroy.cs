using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSystemDestroy : MonoBehaviour
{
    private float timeLeft = 1.5f;
    private float timeToDestroy;
    public void Awake()
    {
        timeToDestroy = Time.time + timeLeft;
    }
    public void Update()
    {
        if (Time.time >= timeToDestroy)
        {
            Debug.Log("Deleting");
            GameObject.Destroy(gameObject);
        }
    }

}
