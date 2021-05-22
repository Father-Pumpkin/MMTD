using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour {
	[Header("Shop UI")]
	public GameObject ShopUI;
	public GameObject ShopToggle;
		

	[Header("Description UI")]
	public GameObject DescriptionUI;
    public GameObject description;
    private TMP_Text descriptionText;
    public GameObject cost;
    private TMP_Text costPro;

	[Header("Upgrade UI")]
	public GameObject UpgradeUI;
	public GameObject CurrentSelected;
	public GameObject ProjectileUpgradeCost;
	public GameObject TurretUpgradeCost;
	public GameObject RefundAmount;
    public GameObject enemiesKilledTracker;
    private TMP_Text CurrentSelectedPro;
    private TMP_Text ProjectileUpgradeCostPro;
    private TMP_Text TurretUpgradeCostPro;
    private TMP_Text RefundAmountPro;
    private TMP_Text enemiesKilledTrackerPro;

    [Header("Mana UI")]
    public GameObject ManaUI;
    public Text spellDescription;
    public Text manaCost;

    [Header("Money Text UI")]
    public GameObject moneyText;
    private TextMeshProUGUI moneyTextPro;

    private Node target;

    private void Awake()
    {
        descriptionText = description.GetComponent<TMP_Text>();
        CurrentSelectedPro = CurrentSelected.GetComponent<TMP_Text>();
        costPro = cost.GetComponent<TMP_Text>();
        ProjectileUpgradeCostPro = ProjectileUpgradeCost.GetComponent<TMP_Text>();
        TurretUpgradeCostPro = TurretUpgradeCost.GetComponent<TMP_Text>();
        RefundAmountPro = RefundAmount.GetComponent<TMP_Text>();
        moneyTextPro = moneyText.GetComponent<TextMeshProUGUI>();
        enemiesKilledTrackerPro = enemiesKilledTracker.GetComponent<TMP_Text>();
    }

    public void Update()
	{
		if (UpgradeUI.activeSelf)
		{
			//ShopUI.SetActive(false);
		}
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShopUI.SetActive(!ShopUI.activeSelf);
        }
        if (target != null && target.turret != null)
        { enemiesKilledTrackerPro.text = "Num Enemies Killed: " + target.turret.GetComponent<Turret>().killCount;
        }

	}

    public void SetTarget(Node _target)
    {

        target = _target;

		CurrentSelectedPro.text = target.turretBlueprint.name;
		ProjectileUpgradeCostPro.text = "$" + target.turretBlueprint.projectileUpgradeCost.ToString();
		TurretUpgradeCostPro.text = "$" + target.turretBlueprint.upgradeCost.ToString();
		RefundAmountPro.text = "$" + target.turretBlueprint.refundAmount.ToString();


		DescriptionUI.SetActive(false);
		//ShopUI.SetActive(false);
		UpgradeUI.SetActive(true);
	}

	public void Show()
	{
		if (BuildManager.turretToBuild != null)
		{

            if (descriptionText == null)
            {
                Debug.LogWarning("This Bitch Empty. For " + BuildManager.turretToBuild.name);
            }
            else
            {
                descriptionText.text = BuildManager.turretToBuild.turretDescription;
                costPro.text = "Cost: " + BuildManager.turretToBuild.cost.ToString();
            }
			
			UpgradeUI.SetActive(false);
			DescriptionUI.SetActive(true);
		}

	}
    public void ShowSpellUI(int cost, string description_)
    {
        Hide();
        BuildManager.turretToBuild = null;
        spellDescription.text = description_;
        manaCost.text = "Price: " + cost;
        if (ManaUI != null) ManaUI.SetActive(true);
    }


    public void Hide()
    {
        if(DescriptionUI != null) DescriptionUI.SetActive(false);
        if (UpgradeUI != null)  UpgradeUI.SetActive(false);
        if (ManaUI != null) ManaUI.SetActive(false);
    }

    public void FlashText()
    {
        Color textColorBright = moneyTextPro.color;
        textColorBright.a = 1f;
        moneyTextPro.color = textColorBright;
    }

    public void upgradeProjectile()
    {

        target.UpgradeProjectile();
        BuildManager.instance.DeselectNode();
        ShopUI.SetActive(true);
    }

	public void upgradeTurret()
	{

		target.UpgradeTurret();
		BuildManager.instance.DeselectNode();
        ShopUI.SetActive(true);
    }

	public void Sell()
	{
		target.SellTurret();
		BuildManager.instance.DeselectNode();
        ShopUI.SetActive(true);
    }
}
