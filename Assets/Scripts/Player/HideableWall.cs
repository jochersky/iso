using UnityEngine;
using UnityEngine.UIElements;

public class HideableWall : MonoBehaviour, IVisible
{
    [SerializeField] HideAllFloorObjects hideAllFloorObjects;

    private MeshRenderer mr;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        hideAllFloorObjects.toggleObjectVisibility += ToggleVisibility;
    }

    private void OnDisable()
    {
        hideAllFloorObjects.toggleObjectVisibility -= ToggleVisibility;
    }

    private void ToggleVisibility()
    {
        mr.enabled = !mr.enabled;
    }

    public void HideFromCamera()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void UnhideFromCamera()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
