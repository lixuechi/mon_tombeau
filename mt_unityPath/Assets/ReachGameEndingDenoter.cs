using UnityEngine;
using System.Collections;

public class ReachGameEndingDenoter : MonoBehaviour {

	int frameCounter = 0;

	public Transform rollingCastList;

	// Use this for initialization
	void Start () {
		rollingCastList.position = new Vector3 (0, -12, 0);
	}
	
	// Update is called once per frame
	void Update () {



		if (this.transform.position.y >= 0) 
		{
			if (++frameCounter >= 90) 
			{
				Application.Quit ();
			}
		} 
		else 
		{
			rollingCastList.position += new Vector3(0, 2 * Time.deltaTime, 0);
		}
	}

}
