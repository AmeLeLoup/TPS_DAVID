using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, camera.farClipPlane));
            Debug.DrawRay(ray.origin,ray.direction * 10, Color.green);
        }
    }
}
