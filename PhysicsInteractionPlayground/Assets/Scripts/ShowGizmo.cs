using UnityEngine;

public class ShowGizmo : MonoBehaviour
{
	[SerializeField] private Color _gizmoColour = Color.red;
	[SerializeField] private float _radius = 0.5f;

	void OnDrawGizmos()
	{
		Gizmos.color = _gizmoColour;
		Gizmos.DrawSphere(transform.position, _radius);
	}
}
