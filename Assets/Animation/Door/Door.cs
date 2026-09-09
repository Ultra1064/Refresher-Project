using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator animator;
    public void OpenDoor()
    {
        animator.SetTrigger("open");
    }

    public void CloseDoor()
    {
        animator.SetTrigger("close");
    }
}
