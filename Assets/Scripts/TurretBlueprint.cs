using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurretBlueprint  {
	
	public GameObject prefab;
	public int cost;

	public string name;
	public string turretDescription;
	public int damage;
	public int range;
	public int attackSpeed;
	public int refundAmount;
    public GameObject rangeIndicator;

    public GameObject upgradedPrefab;
	public GameObject altUpgradedPrefab;
    public int upgradeCost;
	
	public GameObject UpgradedProjectilePrefab;
	public GameObject AltUpgradedProjectilePrefab;
	public int projectileUpgradeCost;


}
