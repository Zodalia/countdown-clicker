using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;



public class TimerManager: MonoBehaviour
{
    private struct Timer
    {
        public TimerObject obj;
        public float endTime;
        public int secondsLeft;
    }

    public AnimationCurve spawnTimeCurve;

    public float spawnTimeVariance;
    public float minSpawnTime;

    public float emptyBoardDelay;

    public GameObject prefab;

    private Vector2 bottomLeft;
    private Vector2 topRight;

    private List<Timer> timers;

    private float startTime;
    private float nextSpawnTime;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        timers = new List<Timer>();

        var bottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        bottomLeft = new Vector2(bottom.x, bottom.y);

        var top = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        topRight = new Vector2(top.x, top.y);

        startTime = Time.time;
        CalculateNextSpawnTime();
    }

    private void CalculateNextSpawnTime()
    {
        nextSpawnTime = Mathf.Floor(Time.time + Math.Max(minSpawnTime, spawnTimeCurve.Evaluate(Time.time - startTime) + Random.Range(-spawnTimeVariance, spawnTimeVariance)));
    }

    private void Spawn()
    {
        Timer timer = new()
        {
            obj = Instantiate(prefab).GetComponent<TimerObject>(),
            endTime = Time.time + 10f,
            secondsLeft = 9
        };
        timer.obj.SetCallback(delegate(){OnClick(timer);});

        var objSize = timer.obj.GetComponent<SpriteRenderer>().size;
        var halfHeight = objSize.y * 0.5f;
        var halfWidth = objSize.x * 0.5f;
        timer.obj.transform.position = new Vector3(Random.Range(bottomLeft.x + halfWidth, topRight.x - halfWidth), Random.Range(bottomLeft.y + halfHeight, topRight.y - halfHeight), 0f);
        timers.Add(timer);
    }

    void OnClick(Timer timer)
    {
        ScoreTracker.Instance.AddScore(timer.obj.transform.position, timer.endTime - Time.time);
        timers.Remove(timers.First(x => x.obj == timer.obj));
        Destroy(timer.obj.gameObject);
    }

    void Update()
    {
        if(timers.Count == 0)
        {
            nextSpawnTime = Math.Min(nextSpawnTime, Time.time + emptyBoardDelay);
        }

        if(Time.time >= nextSpawnTime)
        {
            Spawn();
            CalculateNextSpawnTime();
        }

        for(int i = 0; i < timers.Count; i++)
        {
            var timer = timers[i];
            if(timer.endTime <= Time.time)
            {
                gameManager.EndGame();
                return;
            }

            var remainingTime = (int)(timer.endTime - Time.time);
            if(remainingTime != timer.secondsLeft)
            {
                timer.obj.Pulse();

                timer.secondsLeft = remainingTime;
                timers[i] = timer;
            }
        }
    }
}