using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Menu : MonoBehaviour {

	/// <summary>
	/// Public variables
	/// </summary>

	public int defaultPanel = 0;
	public GameObject[] panels;
	public GameObject   consoleText;

	/// <summary>
	/// Basic functions
	/// </summary>

	void Start () 
	{
		DisplayPanel (defaultPanel);
	}

	/// <summary>
	/// Class specific function
	/// </summary>

	public void DisplayPanel(int p)
	{
		foreach (GameObject panel in panels) 
		{
			panel.SetActive(false);
		}

		panels[p].SetActive(true);
	}
}
