using UnityEngine;

public class DropTarget : MonoBehaviour
{
	[SerializeField] private Color _baseColor = Color.aquamarine;
	[SerializeField] private Color _hitColor = Color.darkTurquoise;

	[Header("Timers")]
	[SerializeField] private float _hideDelay = 0.1f;
	[SerializeField] private float _resetDelay = 3f;

	private SpriteRenderer _sprite;
	private bool _hasDropped = false;

	private void Awake()
	{
		_sprite = GetComponent<SpriteRenderer>();
		_sprite.color = _baseColor;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!_hasDropped && collision.collider.CompareTag("Ball"))
		{
			_hasDropped = true;
			_sprite.color = _hitColor;

			Invoke(nameof(HideTarget), _hideDelay);
		}
	}

	private void HideTarget()
	{
		gameObject.SetActive(false);
		Invoke(nameof(ResetTarget), _resetDelay);
	}

	private void ResetTarget()
	{
		_sprite.color = _baseColor;
		gameObject.SetActive(true);
		_hasDropped = false;
	}
}
