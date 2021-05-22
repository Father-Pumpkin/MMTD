using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour {

	public GameObject TurretManagerUI;
	public GameObject DescriptionUI;

	private Node target;


	public void Awake()
	{
		TurretManagerUI.SetActive(false);
		target = null;
	}

	private void SetTarget(Node _target)
	{
		target = _target;
	}

	public void Show()
	{
		TurretManagerUI.SetActive(true);
		DescriptionUI.SetActive(false);
	}

	public void Hide()
	{
		if(target == null|| Input.GetKeyDown(KeyCode.Escape))
		{
			TurretManagerUI.SetActive(false);
		}
	}

}
