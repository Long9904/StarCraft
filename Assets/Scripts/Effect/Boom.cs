using UnityEngine;

public class Boom : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        // Destroy the game object after the animation has finished playing
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }


}
