using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Trigger()
    {
        Debug.Log("test");
        anim.SetTrigger("Start");
    }
}
