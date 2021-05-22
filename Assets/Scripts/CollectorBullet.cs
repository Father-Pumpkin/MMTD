using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorBullet : MonoBehaviour
{
    private Transform target;

    public float explosionRadius = 0f;

    public float speed = 70f;

    public int damage;

    public GameObject impactEffect;
    public GameObject parent;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        //dir.magnitude is distance to target
        // so if the below is true, we hit target
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        Vector3 target_position = target.position;
        target_position.z = 0f;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        //transform.LookAt(target_position);

    }

    void HitTarget()
    {
        if (impactEffect)
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 5f);
        }
        CollectableItem collectedItem = target.GetComponent<CollectableItem>();
        if(collectedItem != null) {
            collectedItem.Collect();
        }

        parent.GetComponent<Turret>().Reload();
        Destroy(gameObject);
    }

}
