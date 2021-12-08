using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharactorMoving>())
        {
            other.GetComponent<CharactorMoving>().OnFinishLine();
        }
    }
}
