using UnityEngine;

public class SeeThroughWalls : MonoBehaviour
{
    [SerializeField] GameObject PlayerOrientation;
    public LayerMask mask;
    private Ray _backLeftRay;
    private Ray _backRightRay;

    // private Vector3 _leftRotVec = Quaternion.AngleAxis(-30, Vector3.up)

    void Update()
    {
        Vector3 backLeftVect = Quaternion.AngleAxis(230, PlayerOrientation.transform.up) * PlayerOrientation.transform.forward;
        _backLeftRay = new Ray(PlayerOrientation.transform.position, backLeftVect);
        Vector3 backRightVect = Quaternion.AngleAxis(175, PlayerOrientation.transform.up) * PlayerOrientation.transform.forward;
        _backRightRay = new Ray(PlayerOrientation.transform.position, backRightVect);

        RaycastHit hit;

        if (Physics.Raycast(_backLeftRay, out hit, 100, mask))
        {
            // TODO: put this code into a script on the wall itself that changes the material
            hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (Physics.Raycast(_backRightRay, out hit, 100, mask))
        {
            // TODO: put this code into a script on the wall itself that changes the material
            hit.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
