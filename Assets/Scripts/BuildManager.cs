using UnityEngine;

public class BuildManager : MonoBehaviour {
	
	public static BuildManager instance;

		
	void Awake (){
		instance = this;
        
	}

	public GameObject buildEffect;

	public static TurretBlueprint turretToBuild;
    private Node selectedNode;
	public NodeUI descriptionUI;
	public NodeUI UpgradeUI;
	

	public bool CanBuild{ get { return turretToBuild != null; }}
	public bool HasMoney{get { return PlayerStats.Money >= turretToBuild.cost; }}

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			descriptionUI.Hide();
			UpgradeUI.Hide();
			turretToBuild = null;
		}

	}
    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
			turretToBuild = null;
		
            return;
        }
        selectedNode = node;
		UpgradeUI.SetTarget(node);
        turretToBuild = null;
		
    }

    public void DeselectNode()
    {
        selectedNode = null;
		descriptionUI.Hide();
    }

	
	public void SelectTurretToBuild (TurretBlueprint turret){
		turretToBuild = turret;
        DeselectNode();
		
	}

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
