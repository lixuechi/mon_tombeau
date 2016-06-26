using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	bool isFaceLeft = true;
	bool isChangingDir = false;

	public Sprite pNormal;
	public Sprite pAttack;
	public Sprite pDefend;

	SpriteRenderer playerSR;

	int frameCounterAction = 0;
	bool isActionToEnemy = false;

	void Start () {
		playerSR = this.GetComponent ("SpriteRenderer") as SpriteRenderer;
	}

	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow) )
		{
			playerSR.sprite = pNormal;
			this.transform.position -= new Vector3(5f * Time.deltaTime, 0, 0);

			if(!isFaceLeft)
			{
				isFaceLeft = true;
				isChangingDir = true;
			}
		}
		else if (Input.GetKey (KeyCode.RightArrow)) 
		{
			playerSR.sprite = pNormal;
			this.transform.position += new Vector3 (5f * Time.deltaTime, 0, 0);

			if (isFaceLeft) 
			{
				isFaceLeft = false;
				isChangingDir = true;
			}
		} 
		else if (Input.GetKeyDown (KeyCode.A)) 
		{ 
			// attack
			playerSR.sprite = pAttack;
			isActionToEnemy = true;
			GlobalVariables.isPlayerAttacking = true;
		} 
		else if (Input.GetKeyDown (KeyCode.D)) 
		{ 
			// defend
			playerSR.sprite = pDefend;
			isActionToEnemy = true;
		} 
		else 
		{
			if(isActionToEnemy && frameCounterAction >= 30)
			{ 
				playerSR.sprite = pNormal;
				frameCounterAction = 0;
				isActionToEnemy = false;
				GlobalVariables.isPlayerAttacking = false;
			}
		}

		if (isChangingDir) 
		{
			this.transform.Rotate (new Vector3 (0, 180, 0));
			isChangingDir = false;
		}

		if (isActionToEnemy) 
		{ 
			frameCounterAction++;
		}
	}
}
