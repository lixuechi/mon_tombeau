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


	void Start () {
		enemySR = this.GetComponent ("SpriteRenderer") as SpriteRenderer;
	}

	void Update () {

		if (!GlobalVariables.isBlackNWhite)
			return;

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
		/*
		if(++frameCounterDiceOpportunity >= DICE_TIME)
		{
			int randN = Random.Range (3, 6);

			switch(randN)
			{
			case ENEMY_STATE_IDLE:
				enemyStayIdle();
				break;
			case ENEMY_STATE_MOVE_TO_PLAYER:
				enemyMoveToPlayer();
				break;
			case ENEMY_STATE_MOVE_FROM_PLAYER:
				enemyMoveFromPlayer();
				break;
			}
			frameCounterDiceOpportunity = 0;

			frameActionSpriteSlack = 0;
		}
		*/

		if (health > DEFEND_THRESHOLD) 
		{
			if(!isMovingToPlayer) isMovingToPlayer = true;
		}

		if (isActionSpriteSlack) 
		{
			if(++frameActionSpriteSlack >= 20)
			{
				enemySR.sprite = eNormalSprite;
				isActionSpriteSlack = false;
			}
		}

		if (isMovingToPlayer) 
		{
			float differX = playerT.position.x - this.transform.position.x;
			if(differX <= 0.3f || differX >= -0.3f)
			{
				isMovingToPlayer = false;
				enemyAttack();
			}

			if(isFaceLeft)
			{
				this.transform.position -= new Vector3(3 * Time.deltaTime, 0, 0);
			}
			else 
			{
				this.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
			}
		}

		// if player is attacking and player is close to enemy
		if (GlobalVariables.isPlayerAttacking) 
		{
			float differX = playerT.position.x - this.transform.position.x;

			if(differX <= 0.3f || differX >= -0.3f)
			{
				enemyDefend();
			}
		}
	}

	void enemyAttack()
	{
		enemySR.sprite = eAttackSprite;
		isActionSpriteSlack = true;
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
