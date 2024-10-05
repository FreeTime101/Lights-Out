using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameAnimation : MonoBehaviour
{

    [Header("Object References")]
    [SerializeField] private Vector2Int flashInterval = new Vector2Int(3, 15);
    [SerializeField] private string[] triggerNames;

    private int flashTime;
    private float timer;

    private Animator nameAnimator;

    private void Awake()
    {
        nameAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        flashTime = GetFlashTime();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flashTime) 
        {
            FireRandomTrigger();
            flashTime = GetFlashTime();
            timer = 0;
        }
    }

    private void FireRandomTrigger() 
        => nameAnimator.SetTrigger(triggerNames[UnityEngine.Random.Range(0, triggerNames.Length)]);

    private int GetFlashTime() => UnityEngine.Random.Range(flashInterval.x, flashInterval.y + 1);

}
