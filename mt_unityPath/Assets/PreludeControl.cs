using UnityEngine;
using System.Collections;

public class PreludeControl : MonoBehaviour {

	const int STATE_RAIN = 0;
	const int STATE_TITLE = 1;
	const int STATE_GROW = 2;
	const int STATE_REVIVE = 3;
	int state = -1;
	int frameCounter = 0;

	public GameObject rains;
	public GameObject title;
	public GameObject veges;
	public GameObject corpse;
	public GameObject soil;

	SpriteRenderer titleSR;

	void Start () {
		state = STATE_RAIN;

		if (title != null) {
			titleSR = title.GetComponent("SpriteRenderer") as SpriteRenderer;
			Color tempColor = titleSR.color;
			titleSR.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0); // init as transparent
		}

		if (soil != null) {
			soil.transform.position = new Vector3(0, -8, 0);		
		}
	}

	void Update () {
	
	}

	void FixedUpdate()
	{
		if (state == STATE_RAIN) {
						frameCounter++;
						//if(++frameCounter < 200)
						{
								if (rains == null)
										return;
								rains.transform.position -= new Vector3 (3 * Time.deltaTime, 3 * Time.deltaTime, 0);
						}
						if (frameCounter >= 150 && frameCounter < 200) {
								titleSR.color += new Color (0, 0, 0, Time.deltaTime);
						} else if (frameCounter >= 200 && frameCounter < 300) {
								titleSR.color -= new Color (0, 0, 0, Time.deltaTime);
						} else if (frameCounter >= 300) {
								state = STATE_TITLE;
								frameCounter = 0;
						}
				} else if (state == STATE_TITLE) {
						if (rains == null)
								return;
						rains.transform.position -= new Vector3 (3 * Time.deltaTime, 3 * Time.deltaTime, 0);

						if (++frameCounter < 150) {
								soil.transform.position += new Vector3 (0, 3 * Time.deltaTime, 0);
						} else { 
								state = STATE_GROW;
								frameCounter = 0;
						}
				} else if (state == STATE_GROW) { 
						if (rains == null)
								return;
						rains.transform.position -= new Vector3 (3 * Time.deltaTime, 3 * Time.deltaTime, 0);


						if (++frameCounter < 200) {
								if (veges == null)
										return;
								veges.transform.position += new Vector3 (0, Time.deltaTime, 0);
						} else {
								state = STATE_REVIVE;
								frameCounter = 0;
						}
				} else if (state == STATE_REVIVE) {
					if(rains == null) return; 
					rains.transform.position -= new Vector3(3 * Time.deltaTime, 3 * Time.deltaTime, 0);

						if(++frameCounter < 300)
						{
							if(corpse == null) return; 
							corpse.transform.position += new Vector3(0, Time.deltaTime, 0);
						}
						else if(++frameCounter >= 400)
						{
							Application.LoadLevel(2);
						}
				}
	}
}
