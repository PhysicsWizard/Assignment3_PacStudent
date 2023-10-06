using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject rabbit;
    [SerializeField]private Tweener tweener;
    [SerializeField] private Animator animator;
    //[SerializeField] private AudioSource playerSound;
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
        Vector3 startPos = rabbit.transform.position;
        Vector3 trueStart = startPos;
        Vector3 endPos = new Vector3(startPos.x + 5f, startPos.y);
        while (true)
        {
            tweener.AddTween(rabbit.transform, startPos, endPos,2f);
            startPos = endPos;
            endPos = new Vector3(endPos.x, endPos.y - 4.2f);
            yield return new WaitForSeconds(2.1f);
            
            animator.SetFloat("Vertical", -1f);
            tweener.AddTween(rabbit.transform, startPos, endPos,2f);
            startPos = endPos;
            endPos = new Vector3(endPos.x - 5f, endPos.y);
            
            yield return new WaitForSeconds(2.1f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Horizontal", -1f);
            tweener.AddTween(rabbit.transform, startPos, endPos,2f);
            startPos = endPos;
            endPos = new Vector3(endPos.x, endPos.y + 4.2f);
            
            yield return new WaitForSeconds(2.1f);
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 1f);
            tweener.AddTween(rabbit.transform, startPos, endPos,2f);
            startPos = endPos;
            endPos = new Vector3(endPos.x+5f, endPos.y);
            yield return new WaitForSeconds(2.1f);
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
        }
    }
}
