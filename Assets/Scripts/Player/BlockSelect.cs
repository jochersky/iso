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
                Plane translationPlane = new Plane(selectedBlock.transform.up, 0);
                Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

                if (translationPlane.Raycast(mouseRay, out float distance))
                {
                    Vector3 newPos = mouseRay.GetPoint(distance);
                    Debug.DrawLine(Vector3.up, newPos);
                    // selectedBlock.transform.position = newPos;
                    selectedBlock.transform.position = new Vector3(newPos.x, selectedBlock.transform.position.y, newPos.y);
                }
            }

            // if (Input.GetMouseButtonDown(0)) _movingBlock = true;

            // if (_movingBlock)
            // {
            //     if (!blockSelected)
            //     {
            //         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //         RaycastHit hit;

            //         if (Physics.Raycast(ray, out hit, rayDistance, mask))
            //         {
            //             blockSelected = true;
            //             selectedBlock = hit;
            //         }
            //     }

            //     Vector3 newBlockPos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z).normalized;
            //     newBlockPos.y = selectedBlock.transform.position.y;
            //     selectedBlock.transform.position = newBlockPos;
            // }
        }
    }

    private void ChangePlayState()
    {
        playEnabled = !playEnabled;
    }
}
