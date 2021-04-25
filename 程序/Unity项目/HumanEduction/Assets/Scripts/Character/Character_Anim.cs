using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Anim : MonoBehaviour
{   
    public Animator anim;
    private Character character;
    // Start is called before the first frame update
    void Awake() {
        character = this.GetComponent<Character>();
        anim = this.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("X",character.getH());
        anim.SetFloat("Z",character.getV());
    }
}
