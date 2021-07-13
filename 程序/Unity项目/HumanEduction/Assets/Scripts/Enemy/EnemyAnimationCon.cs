using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationCon : MonoBehaviour
{
    public Animator anim;
    private Character character;
    // Start is called before the first frame update
    void Awake()
    {
        character = this.GetComponent<Character>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("X", character.getH());
        anim.SetFloat("Z", character.getV());
        anim.SetBool("isOnGround", character.isOnGrounded());
    }
}
