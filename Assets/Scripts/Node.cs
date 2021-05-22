using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
// responsible for dealing with animations on the node. 
// for example, can build here, cant build here, etc
public class Node : MonoBehaviour
{
	public Color hoverColor;
	public Color notEnoughMoneyColor;
	public Vector3 positionOffset;

	[Header("Ranges")]
	public GameObject range0;
	public GameObject range1;
	public GameObject range2;
	public GameObject range3;
	public GameObject range4;
	public GameObject range5;
	public GameObject range6;
	public GameObject range7;
	public GameObject range8;
	public GameObject range9;

	private GameObject[] ranges = new GameObject[10];
	private GameObject currentRange;
    private GameObject tempRange;

    private NodeUI UIManager;

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;


	private Renderer rend;
	private Color startColor;
	BuildManager buildManager;

	private bool projectileUpgraded = false;
	private bool turretUpgraded = false;

	private bool stayOn = false;

	void Awake()
	{
		ranges[0] = range0;
		ranges[1] = range1;
		ranges[2] = range2;
		ranges[3] = range3;
		ranges[4] = range4;
		ranges[5] = range5;
		ranges[6] = range6;
		ranges[7] = range7;
		ranges[8] = range8;
		ranges[9] = range9;
		currentRange = null;
        UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<NodeUI>();
	}

	void Start()
	{

		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		buildManager = BuildManager.instance;
		
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			stayOn = false;
			Hide();
		}
		else if(stayOn && BuildManager.turretToBuild != null)
		{
			stayOn = false;
			Hide();
		}
	}



	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	void OnMouseDown() {
        Hide();
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (turret != null)
		{
			buildManager.SelectNode(this);

            currentRange = turret.GetComponent<Turret>().rangeIndicator;
            currentRange.SetActive(true);
            stayOn = true;
			return;
		}
        else
        {
            stayOn = false;
        }

		if (!buildManager.CanBuild)
			return;

		BuildTurret(buildManager.GetTurretToBuild());

	}

	void BuildTurret(TurretBlueprint blueprint)
	{
		if (PlayerStats.Money < blueprint.cost)
		{
            UIManager.FlashText();
			return;
		}
		PlayerStats.Money -= blueprint.cost;



		GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        _turret.AddComponent(typeof(Collision2D));
		turret = _turret;

		turretBlueprint = blueprint;
        
	}

	public void UpgradeTurret()
	{
		if (projectileUpgraded)
		{
			if (PlayerStats.Money < turretBlueprint.upgradeCost)
			{
                UIManager.FlashText();
				return;
			}

			PlayerStats.Money -= turretBlueprint.upgradeCost;

			//Get rid of the old turret
			Destroy(turret);

			//build new one
			GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
			turret = _turret;


			GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);

			Destroy(effect, 5f);

			turretUpgraded = true;
		}
		else
		{
			if (PlayerStats.Money < turretBlueprint.upgradeCost)
			{
				Debug.Log("Not enough money to build that!");
				return;
			}

			PlayerStats.Money -= turretBlueprint.upgradeCost;

			//Get rid of the old turret
			Destroy(turret);

			//build new one
			GameObject _turret = (GameObject)Instantiate(turretBlueprint.altUpgradedPrefab, GetBuildPosition(), Quaternion.identity);
			turret = _turret;

            turretUpgraded = true;
		}
	}

	public void UpgradeProjectile()
	{
		if (turretUpgraded)
		{
			if (PlayerStats.Money < turretBlueprint.projectileUpgradeCost)
			{
				Debug.Log("Not enough money to build that!");
				return;
			}

			PlayerStats.Money -= turretBlueprint.projectileUpgradeCost;

			//Get rid of the old turret
			Destroy(turret);

			//build new one
			GameObject _turret = (GameObject)Instantiate(turretBlueprint.UpgradedProjectilePrefab, GetBuildPosition(), Quaternion.identity);
			turret = _turret;

            
		}

		else
		{
			if (PlayerStats.Money < turretBlueprint.projectileUpgradeCost)
			{
				Debug.Log("Not enough money to build that!");
				return;
			}

			PlayerStats.Money -= turretBlueprint.projectileUpgradeCost;

			//Get rid of the old turret
			Destroy(turret);

			//build new one
			GameObject _turret = (GameObject)Instantiate(turretBlueprint.AltUpgradedProjectilePrefab, GetBuildPosition(), Quaternion.identity);
			turret = _turret;

			projectileUpgraded = true;
		}
	}

	public void SellTurret()
	{
		stayOn = false;
		Hide();
		PlayerStats.Money += turretBlueprint.refundAmount;
		Destroy(turret);
	}

	void OnMouseEnter()
	{
		
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (!buildManager.CanBuild)
		{
			return;
		}

		if (buildManager.HasMoney)
		{
            Show();
			rend.material.color = hoverColor;
		}

		if (!buildManager.HasMoney)
		{
			rend.material.color = notEnoughMoneyColor;
		}


	}

	void OnMouseExit()
	{
        HideTemp();
		rend.material.color = startColor;
	}

	void Show()
	{
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(90f, 0f, 0f);
		Vector3 rangePosition = new Vector3(GetBuildPosition().x, GetBuildPosition().y, GetBuildPosition().z + .55f);
		tempRange = (GameObject)Instantiate(ranges[BuildManager.turretToBuild.range], rangePosition, rotation);
		tempRange.SetActive(true);
	}

    void HideTemp()
    {
        if (tempRange != null)
        {
            Destroy(tempRange);
        }
    }
	void Hide()
	{
        GameObject[] ranges = GameObject.FindGameObjectsWithTag("Range");
        foreach (GameObject range in ranges)
        {
            range.SetActive(false);
        }
		
	}
}

