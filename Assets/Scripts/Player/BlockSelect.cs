using System;
using UnityEngine;

public class BlockSelect : MonoBehaviour
{
    [SerializeField] Camera cam;
    public LayerMask mask;
    [SerializeField] float rayDistance = 1000f;

    [SerializeField] StateSelect stateSelect;
    bool playEnabled = true;
    bool blockSelected;
    bool _movingBlock;
    RaycastHit selectedBlock;

    private void OnEnable()
    {
        stateSelect.togglePressed += ChangePlayState;
    }

    private void OnDisable()
    {
        stateSelect.togglePressed -= ChangePlayState;
    }

    void Update()
    {
        DetectBlock();
    }

    private void DetectBlock()
    {
        if (!playEnabled)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _movingBlock = false;
                // blockSelected = false;
                // selectedBlock = new RaycastHit();
            }

            if (Input.GetMouseButton(0))
                {
                    _movingBlock = true;

                    // Check if the player is trying to select a block to translate.
                    if (!blockSelected)
                    {
                        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(mouseRay, out hit, rayDistance, mask))
                        {
                            blockSelected = true;
                            selectedBlock = hit;
                        }
                    }
                }

            // TODO: make it so that the player needs to drag on the move icon to translate a block.
            if (_movingBlock)
            {
                Plane translationPlane = new Plane(Vector3.down, 0);
                Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

                if (translationPlane.Raycast(mouseRay, out float distance))
                {
                    Vector3 newPos = mouseRay.GetPoint(distance);
                    newPos = NearestUnitCoords(newPos);

                    selectedBlock.transform.position = newPos;
                }
            }
        }
    }

    private Vector3 NearestUnitCoords(Vector3 v)
    {
        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);
        v.z = Mathf.Round(v.z);
        return v;
    }

    private void ChangePlayState()
    {
        playEnabled = !playEnabled;
    }
}
