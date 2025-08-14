using UnityEngine;

public class StateSelect : MonoBehaviour
{
    // Invoked on player toggling between play and toggle modes
    public delegate void TogglePressed();
    public TogglePressed togglePressed;

    private void Update()
    {
        CheckToggleChange();
    }

    private void CheckToggleChange()
    {
        if (Input.GetButtonDown("Toggle State"))
        {
            togglePressed?.Invoke();
        }
    }
}
