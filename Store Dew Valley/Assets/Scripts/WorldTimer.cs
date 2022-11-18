using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTimer : MonoBehaviour
{
    public float timeBetweenTicks = 100f;
    private float tickTimer;

    public static WorldTimer instance;

    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        StartCoroutine (WorldTimerTick());
    }
    public void Update()
    {
        

        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0)
        {
            tickTimer = timeBetweenTicks;
        }
    }

    IEnumerator WorldTimerTick()
    {
        yield return new WaitForSeconds(10f);
        //Tick
        StartCoroutine(WorldTimerTick());
    }
}
