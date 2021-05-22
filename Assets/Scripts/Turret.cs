using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour {

	public Transform target;
    private Enemy targetEnemy;
	private CollectableItem targetCollectable;
	
	[Header("General")]

	public float range = 15f;
    private float targetRange;
	public string name_;
	public string description;
    public GameObject rangeIndicator;
    public bool canFire = true;
    


	[Header("Use Bullets (default)")]
	public GameObject bulletPreFab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Use Laser")]
	public bool useLaser = false;
	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public int damageOverTime = 30;
	public float slow = .5f;
    public ParticleSystem particles;

    [Header("Collector")]
    public GameObject collectorPrefab;
    public bool isCollector = false;
    public float collectionRate = 5f;

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";
    public string collectableTag = "Collectable";

    public int killCount = 0;

    public Transform partToRotate;
    public Transform partToTurn;
	public float turnSpeed = 10f;
	


	public Transform firePoint;


	// Use this for initialization
	void Start () {
        targetRange = range/2;
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}


	void UpdateTarget ()
	{
        if (isCollector)
        {
            target = null;
            GameObject[] collectables = GameObject.FindGameObjectsWithTag(collectableTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestItem = null;
            foreach (GameObject item in collectables)
            {
                float distanceToCollectable = Vector3.Distance(transform.position, item.transform.position);
                if (distanceToCollectable < shortestDistance)
                {
                    shortestDistance = distanceToCollectable;
                    nearestItem = item;
                }
            }
            if (nearestItem != null && shortestDistance <= range)
            {
                target = nearestItem.transform;
                targetCollectable = nearestItem.GetComponent<CollectableItem>();
            }
            return;
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }

            else
            {
                target = null;
            }
        }
	}
	// Update is called once per frame
	void Update () {
		if (target == null) {
			if (useLaser) {
				if (lineRenderer.enabled) {
					lineRenderer.enabled = false;
					impactEffect.Stop ();
				}
				return;
			}
            return;
        }
		LockOnTarget ();

		if (useLaser) {
			Laser ();
		} else {
			if (fireCountdown <= 0f) {
				Shoot ();
				fireCountdown = 1f / fireRate;
			}
			fireCountdown -= Time.deltaTime;
		}
	
	}


	void LockOnTarget (){
		if (target != null) {
            float diffy = target.transform.position.y - this.transform.position.y;
            float diffx = target.transform.position.x - this.transform.position.x;
            double rotationZ = Mathf.Atan(diffy / diffx);
            rotationZ *= 360 / (2 * 3.14);
            
            if(diffx < 0 && diffy > 0)
            {
                rotationZ += 180f;
            }
            else if(diffx < 0 && diffy < 0)
            {
                rotationZ -= 180;
            }
            else if(diffx > 0 && diffy > 0){
                rotationZ += 0;
            }
            else
            {
                rotationZ += 0;
            }
            
            this.transform.rotation = Quaternion.Euler(new Vector3( 0f ,0f , (float)rotationZ - 90));
        }
	}

	void Laser(){
		
		targetEnemy.Slow(slow);
		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
			impactEffect.Play();
		}

		lineRenderer.SetPosition (0, firePoint.position);
		lineRenderer.SetPosition (1, target.position);

		Vector3 dir = -firePoint.position + target.position;
        dir = dir.normalized;
		impactEffect.transform.position = target.position + dir.normalized * 2;

		impactEffect.transform.rotation = Quaternion.LookRotation (dir);

        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i);
            Vector3 spawnpoint = firePoint.position + dir * i/10 * Vector3.Distance(firePoint.position, target.position);
            Instantiate(particles, spawnpoint, Quaternion.identity);
        }
    }
	
	void Shoot(){
        if(bulletPreFab == null || target == null)
        {
            return;
        }
		GameObject bulletGO = (GameObject)Instantiate (bulletPreFab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet> ();

        if (bullet != null)
        {
            bullet.setParent(this);
            bullet.Seek(target);
        }
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, targetRange);
	}

    public void Collect(GameObject go)
    {
        if (!canFire)
        {
            return;
        }
        GameObject collectorGO = (GameObject)Instantiate(collectorPrefab, firePoint.position, firePoint.rotation);
        CollectorBullet CBullet = collectorGO.GetComponent<CollectorBullet>();
        CBullet.parent = this.gameObject;
        canFire = false;
        if(CBullet != null)
        {
            CBullet.Seek(go.transform);
        }

    }

    public void Reload()
    {
        canFire = true;
        return;
    }
}

