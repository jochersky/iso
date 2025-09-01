using UnityEngine;

public class HideAllFloorObjects : MonoBehaviour
{
    // Invoked on enabled being changed to hide/unhide other objects on the floor.
    public delegate void ToggleObjectVisibility();
    public ToggleObjectVisibility toggleObjectVisibility;

    private MeshRenderer mr;

    private bool prevEnable;
    private bool changedMeshRendererEnabled;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        prevEnable = mr.enabled;
    }

    private void Update()
    {
        if (mr.enabled != prevEnable)
        {
            changedMeshRendererEnabled = true;
        }

        if (changedMeshRendererEnabled)
        {
            toggleObjectVisibility?.Invoke();
            changedMeshRendererEnabled = false;
        }

        prevEnable = mr.enabled;
    }
}
