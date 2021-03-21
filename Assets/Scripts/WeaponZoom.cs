using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float zoomedOutFOV = 60f;
    [SerializeField] private float zoomedInFOV = 20f;

    [SerializeField] private RigidbodyFirstPersonController fPSController;
    [SerializeField] private float zoomedOutSensitivity = 2f;
    [SerializeField] private float zoomedInSensitivity = 0.5f;
    
    private bool zoomedInToggle = false;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        
        zoomedInToggle = !zoomedInToggle;
        
        UpdateZoom();
    }

    private void UpdateZoom()
    {
        camera.fieldOfView = zoomedInToggle ? zoomedInFOV : zoomedOutFOV;

        var sensitivity = zoomedInToggle ? zoomedInSensitivity : zoomedOutSensitivity;

        fPSController.mouseLook.XSensitivity = sensitivity;
        fPSController.mouseLook.YSensitivity = sensitivity;
    }

    private void OnDisable()
    {
        zoomedInToggle = false;
        UpdateZoom();
    }
}