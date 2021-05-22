using UnityEngine;

public class RangeIndicator : MonoBehaviour {
	public static RangeIndicator instance;

	public GameObject range_1;
	public GameObject range_2;
	public GameObject range_3;
	public GameObject range_4;
	public GameObject range_5;
	public GameObject range_6;
	public GameObject range_7;
	public GameObject range_8;
	public GameObject range_9;
	public GameObject range_10;
	public GameObject range_11;
	public GameObject range_12;

	private GameObject[] ranges = new GameObject[13];

	void Awake()
	{
		ranges[0] = null;
		ranges[1] = range_1;
		ranges[2] = range_2;
		ranges[3] = range_3;
		ranges[4] = range_4;
		ranges[5] = range_5;
		ranges[6] = range_6;
		ranges[7] = range_7;
		ranges[8] = range_8;
		ranges[9] = range_9;
		ranges[10] = range_10;
		ranges[11] = range_11;
		ranges[12] = range_12;
	}

	public void indicateRange(Node node)
	{
		if(BuildManager.turretToBuild == null)
		{
			foreach(GameObject range_i in ranges)
			{
				range_i.SetActive(false);
			}
		}

		else
		{
			ranges[BuildManager.turretToBuild.range].SetActive(true);
			ranges[BuildManager.turretToBuild.range].transform.position = node.GetBuildPosition();
		}
	}

    public GameObject[] getRanges()
    {
        return ranges;
    }
}
