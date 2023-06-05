using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform player;
    public CinemachineVirtualCamera cam;
    public float scrollSpeed;
    float desiredZoom;
    readonly float maxZoom = 35;
    readonly float minZoom = 25;

    private void Start()
    {
        desiredZoom = 25;
    }

    private void FixedUpdate()
    {
        if (player.GetComponent<PlayerController>().controls.Keyboard.MouseWheel.ReadValue<Vector2>().normalized.y > 0)
            desiredZoom -= 0.1f * cam.m_Lens.FieldOfView * scrollSpeed;
        else if (player.GetComponent<PlayerController>().controls.Keyboard.MouseWheel.ReadValue<Vector2>().normalized.y < 0)
            desiredZoom += 0.1f * cam.m_Lens.FieldOfView * scrollSpeed;
        if (desiredZoom > maxZoom)
            desiredZoom = maxZoom;
        if (desiredZoom < minZoom)
            desiredZoom = minZoom;
        float currentZoom = Mathf.Lerp(cam.m_Lens.FieldOfView, desiredZoom, Time.fixedDeltaTime * scrollSpeed);
        cam.m_Lens.FieldOfView = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }
}