using System.Collections;
using UnityEngine;

public class Bumper : MonoBehaviour
{
	[Header("Physics Settings")]
	[SerializeField] private float _force = 100f;

	[Header("Reaction Settings")]
	[SerializeField] private float _litDuration = 0.2f;
	[SerializeField] private Color _litColor = Color.red;
	[SerializeField] private Color _baseColor = Color.darkRed;

	private bool _isLit = false;
	private SpriteRenderer _sprite;

	private void Awake()
	{
		_sprite = GetComponent<SpriteRenderer>();

		// Changed base color to be set by the variable so it is easier to compare the off and on colors
		_sprite.color = _baseColor;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Ball"))
		{
			ApplyBumperForce(collision);

			if (!_isLit)
			{
				StartCoroutine(LightUp());
			}
		}	
	}

	private void ApplyBumperForce(Collision2D collision)
	{
		Vector2 normal = Vector2.zero;
		if (collision.rigidbody != null)
		{
			if (collision.contactCount > 0)
			{
				ContactPoint2D contact = collision.GetContact(0);
				normal = contact.normal;
			}
			else if (normal == Vector2.zero)
			{
				Vector2 dir = (collision.rigidbody.position - (Vector2)transform.position);
				normal = dir.normalized;
			}

			Vector2 impulse = -normal * _force;
			collision.rigidbody.AddForce(impulse, ForceMode2D.Impulse);
		}
	}

	private IEnumerator LightUp()
	{
		_isLit = true;
		_sprite.color = _litColor;
		yield return new WaitForSeconds(_litDuration);
		_isLit = false;
		_sprite.color = _baseColor;
	}
}
