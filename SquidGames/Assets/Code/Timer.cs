using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    //private static TimeCounter instance;
    [SerializeField] internal Text timer;
    private GameObject player;
    internal float seconds, minutes;
    internal float time;
    internal float timeToWait;

    internal float savedTime = 0;

    private bool startCounting;

    //private MovePlayer movePlayer;

    private void Start()
    {
        startCounting = false;
        timer.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        MovePlayer.OnClickTimer += StartTickingTime;
    }

    private void OnDisable()
    {
        MovePlayer.OnClickTimer -= StartTickingTime;
    }

    private void StartTickingTime(float timeSeconds)
    {
        timer.gameObject.SetActive(true);
        startCounting = true;
        timeToWait = timeSeconds + 1;
        //Debug.Log("1 are de");
    }


    private void Update()
    {
        if (startCounting == true)
        {
            CountTime(timeToWait);
        }       
    }

    //private IEnumerator WaitForPlayerToRevive()
    //{
    //    if (isCoroutineExecuting)
    //        yield break;

    //    isCoroutineExecuting = true;


    //    yield return new WaitForSeconds(0.5f);

    //    player = GameObject.FindGameObjectWithTag("Player");
    //    isCoroutineExecuting = false;
    //}


    private void CountTime(float timeSeconds)
    {
        if (time < timeSeconds)
        {
            //Debug.Log("timer = " + time);

            time += Time.deltaTime;
            seconds = Mathf.Floor(time % 60);
            minutes = Mathf.Floor(time / 60);

            timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            //timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            startCounting = false;
            timer.gameObject.SetActive(false);
            time = 0;
        }
    }
}
