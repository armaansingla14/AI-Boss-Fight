using UnityEngine;

public class RaycastVisualizer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform raycastOrigin;
    public float maxDistance = 100f;

    void Start()
    {
        // Ensure the LineRenderer is initialized properly
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Cast a ray from the origin in the forward direction
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, maxDistance))
        {
            // If the ray hits something, update the LineRenderer positions
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, raycastOrigin.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // If the ray doesn't hit anything, disable the LineRenderer
            lineRenderer.enabled = false;
        }
    }
}
