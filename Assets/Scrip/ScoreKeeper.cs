using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score { get; private set; }
	float lastEnemyKillTime;
	int streakCount;
	float streakExpiryTime = 1;
	public static int enemy { get; private set; }

	void Start() {
		score = 0;
		Enemy.OnDeathStatic += OnEnemyKilled;
		FindObjectOfType<Player> ().OnDeath += OnPlayerDeath;
	}

	void OnEnemyKilled() {

		if (Time.time < lastEnemyKillTime + streakExpiryTime) {
			streakCount++;
		} else {
			streakCount = 0;
		}

		lastEnemyKillTime = Time.time;

		score += 5 + (int)Mathf.Pow(2,streakCount);
		enemy++;
	}

	void OnPlayerDeath() {
		Enemy.OnDeathStatic -= OnEnemyKilled;
	}
	
}
