using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private List<Transform> _spawnPoints = new();

    [Header("Respawn Settings")]
    [SerializeField] private float _respawnWaitTime = 1.5f;

	private void Start()
	{
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("Respawn"))
        {
            _spawnPoints.Add(spawn.GetComponent<Transform>());
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Ball"))
        {
            GameObject ball = collision.gameObject;
            ball.SetActive(false);
            StartCoroutine(RespawnBall(ball));
        }
	}

    private IEnumerator RespawnBall(GameObject ball)
    {
        yield return new WaitForSeconds(_respawnWaitTime);

		ball.SetActive(true);

		Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

        Transform spawn = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        ball.transform.position = spawn.position;
    }
}
