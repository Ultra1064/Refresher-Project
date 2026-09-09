using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] UnityEvent OnEnterTrigger;
    [SerializeField] UnityEvent OnLeaveTrigger;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered trigger");
        OnEnterTrigger.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exited trigger");
        OnLeaveTrigger.Invoke();
    }
}
