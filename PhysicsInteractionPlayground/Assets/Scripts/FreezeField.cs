using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeField : MonoBehaviour
{
	[SerializeField] private float _freezeDuration = 1f;
	[SerializeField] private float _ignoreDuration = 2f;

	private List<Rigidbody2D> _ignoredRb = new();

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// return if the collision does not have a rigidbody
		// or if rigidbody is ignored
		if (collision.attachedRigidbody == null ||
			_ignoredRb.Contains(collision.attachedRigidbody))
			return;

		// freeze the rigidbody if its dynamic
		if (collision.attachedRigidbody.bodyType == RigidbodyType2D.Dynamic)
		{
			collision.attachedRigidbody.bodyType = RigidbodyType2D.Static;

			StartCoroutine(UnfreezeRigidbody(collision.attachedRigidbody));
		}
	}

	private IEnumerator UnfreezeRigidbody(Rigidbody2D rb)
	{
		yield return new WaitForSeconds(_freezeDuration);
		rb.bodyType = RigidbodyType2D.Dynamic;

		_ignoredRb.Add(rb);
		yield return new WaitForSeconds(_ignoreDuration);
		_ignoredRb.Remove(rb);
    }
}
