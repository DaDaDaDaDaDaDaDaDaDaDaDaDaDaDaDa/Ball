using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendering : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private LineRenderer lineRenderer;
    private bool disabled;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        if (disabled)
            return;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.position);
    }
    public void DisableLine()
    {
        lineRenderer.enabled = false;
        disabled = true;
    }
    public void EnableLine()
    {
        lineRenderer.enabled = true;
        disabled = false;
    }
}
