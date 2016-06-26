using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

	public static bool isPlayerAttacking = false;

	public static bool isBlackNWhite = true;
	private int frameCounterSwitchTheme = 0;
	private int switchThreshold = 30;
	public GameObject BWstuff;
	public GameObject Cstuff;
	public SpriteRenderer EnemySR;
	public Sprite GoodEnemySprite;
	public Sprite BadEnemySprite;

	const int switchFrameMin = 420;
	const int switchFrameMax = 840;


	void Start()
	{
		int randFrameTheshold = Random.Range (switchFrameMin, switchFrameMax);
		switchThreshold = randFrameTheshold;
	}

	void FixedUpdate()
	{
		if (++frameCounterSwitchTheme >= switchThreshold) 
		{
			isBlackNWhite = !isBlackNWhite;
			switchTheme();
			switchThreshold = Random.Range (switchFrameMin, switchFrameMax);
			frameCounterSwitchTheme = 0;
		}
	}

	void switchTheme()
	{
		BWstuff.SetActive(isBlackNWhite);
		Cstuff.SetActive (!isBlackNWhite);

		if (isBlackNWhite) 
		{
			EnemySR.sprite = BadEnemySprite;
		} 
		else 
		{
			EnemySR.sprite = GoodEnemySprite;
		}
	}
}
