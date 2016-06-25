using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	bool isFaceLeft = true;
	bool isChangingDir = false;

	void Start () {
	
	}

	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow) )
		{
			this.transform.position -= new Vector3(5f * Time.deltaTime, 0, 0);

			if(!isFaceLeft)
			{
				isFaceLeft = true;
				isChangingDir = true;
			}
		}
		if(Input.GetKey (KeyCode.RightArrow) )
		{
			this.transform.position += new Vector3(5f * Time.deltaTime, 0, 0);

			if(isFaceLeft)
			{
				isFaceLeft = false;
				isChangingDir = true;
			}
		}

		if (isChangingDir) 
		{
			this.transform.Rotate (new Vector3 (0, 180, 0));
			isChangingDir = false;
		}
	}
}
