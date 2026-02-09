using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private Transform _target;

	[Header("Reaction Settings")]
	[SerializeField] private Color _activatedColor = Color.magenta;
	[SerializeField] private Color _deactivatedColor = Color.darkMagenta;
	[SerializeField] private Vector3 _baseScale = new(0.5f, 0.5f, 1f);
	[SerializeField] private Vector3 _deactivatedScale = new(0.15f, 0.15f, 1f);

	[Header("Timers")]
	[SerializeField] private float _resetDelay = 6f;

	private SpriteRenderer _sprite;

	private bool _active = true;

	private void Awake()
	{
		_sprite = GetComponent<SpriteRenderer>();

		_sprite.color = _activatedColor;
		transform.localScale = _baseScale;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!_active) return;

		if (collider.CompareTag("Ball"))
		{

			if (collider.attachedRigidbody != null)
			{
				// Deactivate this portal and the paired portal
				DeactivatePortal();
				if (_target.TryGetComponent<Portal>(out Portal targetPortal))
					targetPortal.DeactivatePortal();

				// Teleport collider
				collider.attachedRigidbody.position = _target.position;
			}
		}
	}

	public void DeactivatePortal()
	{
		_sprite.color = _deactivatedColor;
		transform.localScale = _deactivatedScale;
		_active = false;
		Invoke(nameof(ResetPortal), _resetDelay);
	}

	private void ResetPortal()
	{
		_sprite.color = _activatedColor;
		transform.localScale = _baseScale;
		_active = true;
	}
}
