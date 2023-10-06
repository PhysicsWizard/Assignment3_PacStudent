using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject rabbit;
    [SerializeField]private Tweener tweener;
    [SerializeField] private Animator animator;
    void Start()
    {
        StartCoroutine(moveMe());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator moveMe()
    {
        while (true)
        {
            tweener.AddTween(rabbit.transform, new Vector3(2.5f, -1), new Vector3(7.5f, -1),2f);
            yield return new WaitForSeconds(2.1f);
            animator.SetFloat("Vertical", -1f);
            tweener.AddTween(rabbit.transform, new Vector3(7.5f, -1), new Vector3(7.5f, -5.2f),2f);
            yield return new WaitForSeconds(2.1f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Horizontal", -1f);
            tweener.AddTween(rabbit.transform, new Vector3(7.5f, -5.2f), new Vector3(2.5f, -5.2f),2f);
            yield return new WaitForSeconds(2.1f);
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 1f);
            tweener.AddTween(rabbit.transform, new Vector3(2.5f, -5.2f), new Vector3(2.5f, -1),2f);
            yield return new WaitForSeconds(2.1f);
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
        }
    }
}
