using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARObjectController : MonoBehaviour
{
    public Camera arCamera;
    public Transform selectedObject;
    public float rotationSpeed = 0.2f;
    public float scaleSpeed = 0.005f;
    public Vector2 scaleClamp = new Vector2(0.2f, 2.5f);

    private ARRaycastManager raycaster;
    private Vector2 prevMid;
    private float prevDist;
    private float prevAngle;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycaster = GetComponent<ARRaycastManager>();
        if (arCamera == null) arCamera = Camera.main;
    }

    private bool IsPointerOverUI() => EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(0);

    void Update()
    {
        if (selectedObject == null) return;

        if (Input.touchCount == 1)
        {
            var t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved && !IsPointerOverUI())
            {
                if (raycaster.Raycast(t.position, hits, TrackableType.Planes))
                {
                    var pose = hits[0].pose;
                    selectedObject.position = pose.position;
                }
            }
        }
        else if (Input.touchCount >= 2)
        {
            var t0 = Input.GetTouch(0);
            var t1 = Input.GetTouch(1);
            var mid = 0.5f * (t0.position + t1.position);
            var dist = Vector2.Distance(t0.position, t1.position);
            var angle = Mathf.Atan2(t1.position.y - t0.position.y, t1.position.x - t0.position.x) * Mathf.Rad2Deg;

            if (t1.phase == TouchPhase.Began) { prevDist = dist; prevAngle = angle; prevMid = mid; }

            // Scale
            float scaleDelta = (dist - prevDist) * scaleSpeed;
            var newScale = Mathf.Clamp(selectedObject.localScale.x + scaleDelta, scaleClamp.x, scaleClamp.y);
            selectedObject.localScale = new Vector3(newScale, newScale, newScale);
            prevDist = dist;

            // Rotate
            float angleDelta = Mathf.DeltaAngle(prevAngle, angle);
            selectedObject.Rotate(Vector3.up, angleDelta * rotationSpeed, Space.World);
            prevAngle = angle;
        }
    }
}
