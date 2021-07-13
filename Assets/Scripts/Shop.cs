using UnityEngine;

public class Shop : MonoBehaviour {
	public TurretBlueprint RubberDucky;
	public TurretBlueprint Catapult;
	public TurretBlueprint MudSlinger;
    public TurretBlueprint Collector;

	BuildManager buildManager;

	void Start() { 
		buildManager = BuildManager.instance;
	}


	public void SelectRubberDucky() {
		buildManager.SelectTurretToBuild (RubberDucky);
	}

	public void SelectCatapult() {
		buildManager.SelectTurretToBuild (Catapult);
	}
	public void SelectMudSlinger() {
		buildManager.SelectTurretToBuild (MudSlinger);
	}
    public void SelectCollector()
    {
        buildManager.SelectTurretToBuild(Collector);
    }
}
