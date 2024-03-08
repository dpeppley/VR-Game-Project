using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour {
    private Animator anim;
     void Start() {
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);
        Debug.Log(anim.GetBool("isWalking"));
        StartCoroutine("ChangeAnim");
     }

     private IEnumerator ChangeAnim() {
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("isWalking", false);
        Debug.Log(anim.GetBool("isWalking"));
     }
}
