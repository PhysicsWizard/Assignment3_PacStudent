using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{

    private Tween activeTween;
    [SerializeField] private GameObject rabbit;
    private float timer;
    void Start()
    {
        StartCoroutine(moveMe());
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTween != null)
        {
            float distance = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);
            timer += Time.deltaTime;
            if (distance > 0.1f)
            {
                double distanceTravelled = timer / activeTween.Duration;
                double easeInExpo = Math.Pow(2, 10 * distanceTravelled - 10);
                double easeInCubic = Math.Pow(distanceTravelled, 3);
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, (float)easeInCubic);
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                timer = 0;
                activeTween = null;
            }
        }
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        //bool returner = TweenExists(targetObject);
        
        if (activeTween == null)
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            Debug.Log("Added");
            //return true;
        }
        //return false;
    }

    IEnumerator moveMe()
    {
        while (true)
        {
            AddTween(rabbit.transform, new Vector3(2.5f, -1), new Vector3(7.5f, -1),2f);
            yield return new WaitForSeconds(2.1f);
            AddTween(rabbit.transform, new Vector3(7.5f, -1), new Vector3(7.5f, -5.2f),2f);
            yield return new WaitForSeconds(2.1f);
            AddTween(rabbit.transform, new Vector3(7.5f, -5.2f), new Vector3(2.5f, -5.2f),2f);
            yield return new WaitForSeconds(2.1f);
            AddTween(rabbit.transform, new Vector3(2.5f, -5.2f), new Vector3(2.5f, -1),2f);
            yield return new WaitForSeconds(2.1f);
        }
    }
}

