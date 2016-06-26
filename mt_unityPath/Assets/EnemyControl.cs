using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public Sprite eNormalSprite;
	public Sprite eAttackSprite;
	public Sprite eDefendSprite;

	SpriteRenderer enemySR;

	int eState = -1;
	const int ENEMY_STATE_ATTACK = 1;
	const int ENEMY_STATE_DEFEND = 2;
	const int ENEMY_STATE_IDLE = 3;
	const int ENEMY_STATE_MOVE_TO_PLAYER = 4;
	const int ENEMY_STATE_MOVE_FROM_PLAYER = 5;

	int health = 100;
	int attackPower = 10;
	int defendPower = 5;

	const int DEFEND_THRESHOLD = 30;

	int frameCounterDiceOpportunity = 0;
	const int DICE_TIME = 150;

	bool isActionSpriteSlack = false;
	int frameActionSpriteSlack = 0;

	bool isFaceLeft = true;
	bool isChangingDir = false;

	public Transform playerT;

	bool isMovingToPlayer = false;
	bool isMovingFromPlayer = false;

	bool isCloseToPlayer = false;
	bool isVeryFarFromPlayer = false;

	bool isEnemyAttacking = false;


	void Start () {
		enemySR = this.GetComponent ("SpriteRenderer") as SpriteRenderer;
	}

	void Update () {

		// return if the current theme is colorful instead of black-and-white
		if (!GlobalVariables.isBlackNWhite)
			return;


		/// <summary>X difference between player and enemy</summary>
		float differX = playerT.position.x - this.transform.position.x;
		Debug.Log("differX = " + differX);
		if (differX <= 1f && differX >= -1f) 
		{
			isCloseToPlayer = true;
		}
		else 
		{
			isCloseToPlayer = false;
		}
		Debug.Log("isCloseToPlayer=" + isCloseToPlayer);
//		if(differX >= 6 || differX <= -6)
//		{
//			isVeryFarFromPlayer = true;
//		}
//		else 
//		{
//			isVeryFarFromPlayer = false;
//		}
		/// <remarks>isCloseToPlayer and isVeryFarFromPlayer exclude each other</remarks>
//		if(isCloseToPlayer)
//		{
//			isVeryFarFromPlayer = false;
//		}
//		if(isVeryFarFromPlayer)
//		{
//			isCloseToPlayer = false;
//		}


		/// <summary>facing directions</summary>
		// always facing the player's direction
		if (this.transform.position.x > playerT.position.x) 
		{
			if (!isFaceLeft)
			{	isFaceLeft = true;
				isChangingDir = true;
			}
		} 
		else 
		{
			if(isFaceLeft) 
			{
				isFaceLeft = false;
				isChangingDir = true;
			}		
		}

		// always checking if is changing direction, if yes, rotate 180 degree.
		if (isChangingDir) 
		{
			this.transform.Rotate(new Vector3(0, 180, 0));
			isChangingDir = false;
		}


		// if enough health, move to player and attack when they're close
		if (health > DEFEND_THRESHOLD 
		    && !isEnemyAttacking
		    && !isCloseToPlayer
		    ) 
		{
			Debug.Log("isMovingToPlayer=" + isMovingToPlayer);
			if(!isMovingToPlayer && !isCloseToPlayer) isMovingToPlayer = true;
			else if(isMovingToPlayer && isCloseToPlayer) isMovingToPlayer = false;
		}

		// hold the attack/defend sprite for a while after returning to normal sprite
		if (isActionSpriteSlack) 
		{
			if(++frameActionSpriteSlack >= 20)
			{
				enemySR.sprite = eNormalSprite;
				isActionSpriteSlack = false;
				frameActionSpriteSlack = 0;
				isEnemyAttacking = false;
			}
		}

		if(isCloseToPlayer && !isEnemyAttacking)
		{
			isMovingToPlayer = false;
			enemyAttack();
			Debug.Log ("Enemy attack");
//			enemyMoveFromPlayer();
		}


		if (isMovingToPlayer) 
		{

			if(isFaceLeft)
			{
				this.transform.position -= new Vector3(3 * Time.deltaTime, 0, 0);
			}
			else 
			{
				this.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
			}
		}
//
//		if(isMovingFromPlayer)
//		{
//			if(isVeryFarFromPlayer)
//			{
//				isMovingFromPlayer = false;
//				enemyMoveToPlayer();
//			}
//
//			if(isFaceLeft)
//			{
//				this.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
//			}
//			else 
//			{
//				this.transform.position -= new Vector3(3 * Time.deltaTime, 0, 0);
//			}
//		}

		// if player is attacking and player is close to enemy
		if (GlobalVariables.isPlayerAttacking) 
		{
			if(isCloseToPlayer)
			{
				enemyDefend();
			}
		}
	}

	void enemyAttack()
	{
		enemySR.sprite = eAttackSprite;
		isActionSpriteSlack = true;
		isEnemyAttacking = true;
	}

	void enemyDefend()
	{
		enemySR.sprite = eDefendSprite;
		isActionSpriteSlack = true;
	}

	void enemyStayIdle()
	{
		enemySR.sprite = eNormalSprite;
	}

	void enemyMoveToPlayer() 
	{
		enemySR.sprite = eNormalSprite;
		isMovingToPlayer = true;
	}

	void enemyMoveFromPlayer()
	{
		enemySR.sprite = eNormalSprite;
		isMovingFromPlayer = true;
	}
}
