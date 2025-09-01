using System;
using UnityEngine;

public class HideCeiling : MonoBehaviour
{
    public LayerMask floorMask;
    RaycastHit[] hits = new RaycastHit[10];

    void Update()
    {
        HideFloors();
        UnhideFloors();
    }

    void HideFloors()
    {
        Ray aboveFloorRay = new Ray(transform.position + Vector3.up, Vector3.up);

        int numHits = Physics.RaycastNonAlloc(aboveFloorRay, hits, Mathf.Infinity, floorMask);

        // Make all floors above invisible when walking below them
        for (int i = 0; i < numHits; i++)
        {
            MeshRenderer mr = hits[i].transform.gameObject.GetComponent<MeshRenderer>();
            if (mr.enabled)
            {
                mr.enabled = false;  
            }
        }
    }

    void UnhideFloors()
    {
        Ray belowFloorRay = new Ray(transform.position + Vector3.up, Vector3.down);

        RaycastHit hit;

        // Make floor reappear when walking on top of it
        if (Physics.Raycast(belowFloorRay, out hit, Mathf.Infinity, floorMask))
        {
            MeshRenderer mr = hit.transform.gameObject.GetComponent<MeshRenderer>();
            if (!mr.enabled)
            {
                mr.enabled = true;  
            }
        }
    }
}
