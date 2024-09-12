using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mover : MonoBehaviour, IDragHandler
{

    public Camera cameraToMove;
    public float moveSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cameraToMove != null)
        {
            float deltaX = eventData.delta.x * moveSpeed;
            cameraToMove.transform.Translate(-deltaX, 0, 0);
        }
    }
}
