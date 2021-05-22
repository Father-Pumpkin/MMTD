using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;

	public float explosionRadius = 0f;

    public int explosionStrength = 0;

	public float speed = 70f;

	public int damage;

    private Turret parent;
	
	public GameObject impactEffect;
	public void Seek(Transform _target){
		target = _target;
	}

	// Update is called once per frame
	void Update () {
		if (target == null) {
			Destroy (gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;
		//dir.magnitude is distance to target
		// so if the below is true, we hit target
		if (dir.magnitude <= distanceThisFrame) {
			HitTarget ();
			return;
		}
		Vector3 target_position = target.position;
		target_position.z = 0f;
		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
		//transform.LookAt(target_position);

	}

	void HitTarget()
	{
        if (impactEffect)
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);
        }
		if (explosionRadius > 0f) {
			Explode ();
		} else {
			Damage(target);
		}

		Destroy (gameObject);
	}

	void Damage (Transform enemy)
	{

		Enemy e = enemy.GetComponent<Enemy> ();
		if (e.gameObject != null) {
			e.TakeDamage (damage, this);
		}

	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);
        int i = 0;
        foreach (Collider collider in colliders)
        {
            if(i > explosionStrength)
            {
                return;
            }
            else if(collider.tag == "Enemy")
            {
                i++;
                Damage(collider.transform);
            }
        }
    
	}

    public Turret setParent(Turret go)
    {
        parent = go;
        return parent;
    }
    public Turret getParent()
    {
        return parent;
    }
}
