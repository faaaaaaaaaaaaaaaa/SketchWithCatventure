using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveHandSetTriggerOn : MonoBehaviour
{
    public Animator animator;
    int rand = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        random();
    }

    // Update is called once per frame
    void Update()
    {
        action();
    }
    void random()
    {
        rand = Random.Range(1, 4);
        //Debug.Log(rand);
  
    }
    void action()
    {
        switch (rand)
        {
            case 1:
                Invoke("action", 3);
                animator.Play("Wave");
                break;
            case 2:
                Invoke("action",3);
                animator.Play("Falling");
                break;
            case 3:
                Invoke("action", 3);
                animator.Play("Idle3");
                break;
        }
    }
}
