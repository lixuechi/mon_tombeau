using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleControl : MonoBehaviour {
	void Start () {
		List<string> btnNames = new List<string> ();
		btnNames.Add ("StartGame");
		
		foreach (string name in btnNames) 
		{
			GameObject btnObj = GameObject.Find (name);
			Button btn = btnObj.GetComponent<Button>();
			btn.onClick.AddListener(delegate()
			                        {
				this.OnClick(btnObj);
			});
		}
	}
	
	void Update () {
		
	}
	
	void OnClick(GameObject sender)
	{
		switch (sender.name) 
		{
		case "StartGame":
			Debug.Log ("button StartGame pressed");
			Application.LoadLevel(1);
			break;
		}
	}
}
