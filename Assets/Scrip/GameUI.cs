using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public Image fadePlane;
	public GameObject gameOverUI;
	public GameObject gameWinUI;
	//man choi
	public RectTransform newWaveBanner;
	public Text newWaveTitle;
	public Text newWaveEnemyCount;
	// diem, mau
	public Text scoreUI;
	public Text gameOverScoreUI;
	public RectTransform healthBar;

	public Text gameWinScoreUI;

	Spawner spawner;

	Player player;


	void Start()
	{	

		player = FindObjectOfType<Player>();
		player.OnDeath += OnGameOver;
			
	}

	void Awake()
	{
		spawner = FindObjectOfType<Spawner>();
		spawner.OnNewWave += OnNewWave;
	}

	void Update()
	{
		scoreUI.text = ScoreKeeper.score.ToString("D6");
		// mau con lai cua nguoi choi
		float healthPercent = 0;
		if (player != null)
		{
			healthPercent = player.health / player.startingHealth;
		}
		healthBar.localScale = new Vector3(healthPercent, 1, 1);

        if (ScoreKeeper.enemy == 200)
        {
			OnGameWin();

        }
	}

	void OnNewWave(int waveNumber)
	{
		string[] numbers = { "One", "Two", "Three", "Four" };
		newWaveTitle.text = "- Wave " + numbers[waveNumber - 1] + " -";
		//string enemyCountString = ((spawner.waves[waveNumber - 1].infinite) ? "Infinite" : spawner.waves[waveNumber - 1].enemyCount + "");
		//newWaveEnemyCount.text = "Enemies: " + enemyCountString;
		newWaveEnemyCount.text = "Enemies: " + spawner.waves[waveNumber - 1].enemyCount;
		StopCoroutine("AnimateNewWaveBanner");
		StartCoroutine("AnimateNewWaveBanner");
	}

	void OnGameOver()
	{
		StartCoroutine(Fade(Color.clear, Color.black, 1));
		gameOverScoreUI.text = scoreUI.text;
		scoreUI.gameObject.SetActive(false);
		gameOverUI.SetActive(true);
	}

	void OnGameWin()
    {
		
		StartCoroutine(Fade(Color.clear, Color.black, 1));
		gameWinScoreUI.text = scoreUI.text;
		scoreUI.gameObject.SetActive(false);
		gameWinUI.SetActive(true);
		gameOverUI.SetActive(false);
	}

	IEnumerator AnimateNewWaveBanner()
	{

		float delayTime = 1.5f;
		float speed = 3f;
		float animatePercent = 0;
		int dir = 1;

		float endDelayTime = Time.time + 1 / speed + delayTime;

		while (animatePercent >= 0)
		{
			animatePercent += Time.deltaTime * speed * dir;

			if (animatePercent >= 1)
			{
				animatePercent = 1;
				if (Time.time > endDelayTime)
				{
					dir = -1;
				}
			}

			newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-170, 45, animatePercent);
			yield return null;
		}

	}

	IEnumerator Fade(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp(from,to,percent);
			yield return null;
		}
	}

	// UI Input
	public void StartNewGame()
	{
		SceneManager.LoadScene("Game");
	}
	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
