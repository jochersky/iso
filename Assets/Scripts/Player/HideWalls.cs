using UnityEngine;

public class HideWalls : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out IVisible visible))
        {
            visible.HideFromCamera();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out IVisible visible))
        {
            visible.UnhideFromCamera();
        }
    }
}
