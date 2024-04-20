using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class ÑameraControll : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject _panel;

    [Header("General")]
    [SerializeField] private float sensitivity = 0.4f;
    [SerializeField] private float distance = 5;
    [SerializeField] private float height = 2.3f;

    void LateUpdate()
    {
        if (_panel != null)
        {
            Touch touch = GetTouchOverPanel();
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                float deltaX = (touch.deltaPosition.x) * sensitivity;
                transform.RotateAround(player.position, Vector3.up, deltaX);
            }
        }
        Vector3 position = player.position - (transform.rotation * Vector3.forward * distance);
        position = new Vector3(position.x, player.position.y + height, position.z);
        position = PositionCorrection(player.position, position);
        transform.position = position;
    }

    Touch GetTouchOverPanel()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = touch.position;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                RaycastResult panelResult = results.FirstOrDefault(result => result.gameObject == _panel);
                if (panelResult.isValid)
                {
                    return touch;
                }
            }
        }
        return new Touch();
    }

    Vector3 PositionCorrection(Vector3 target, Vector3 position)
    {
        RaycastHit hit;
        float sphereRadius = 0.3f;
        if (Physics.SphereCast(target, sphereRadius, position - target, out hit, Vector3.Distance(target, position)))
        {
            position = hit.point + hit.normal * sphereRadius;
        }

        return position;
    }
}