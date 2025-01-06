using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCont : MonoBehaviour
{
    [SerializeField] float animationTime;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animationTime);
        Destroy(this.gameObject);
    }      
}
