using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	[HideInInspector]
	public float speed;

    public Slider healthbar;

	public float startSpeed = 10f;
    public float startHealth = 10f;

	private float health = 10f;

	public int value = 10;
	
	public GameObject deathEffect;
    public GameObject dropObject;
    public string collectorTag = "collector";
    private bool hasDied = false;
	
	void Start(){
		speed = startSpeed;
        health = startHealth;
	}

    private void Update()
    {
        ToggleHealthBar(GameManager.showHealthBar);
    }

    public void ToggleHealthBar(bool healthBarIsOn)
    {
        healthbar.gameObject.SetActive(healthBarIsOn);
    }

	
	public void TakeDamage( float amount, Bullet b){
        if (hasDied)
        {
            return;
        }
		health -= amount;
		if (health <= 0) {
            b.getParent().killCount++;
			Die ();
		}
        healthbar.value = health / startHealth;
	}

	public void Slow(float pct)
	{
		speed = startSpeed * (1f - pct);
	}


	void Die()
	{
        hasDied = true;
        GameObject[] collectionTurrets = GameObject.FindGameObjectsWithTag(collectorTag);
		PlayerStats.Money += value;
        PlayerStats.numEnemiesKilled++;
        bool hasCollected = false;
        PlayerStats.numEnemiesAlive--;
        if( Random.Range(0f,1f) < .8f) {
            Destroy(gameObject);
            return;
        }
        foreach (GameObject go in collectionTurrets)
        {
            if (go.GetComponent<Turret>().canFire)
            {
                if (!hasCollected)
                {
                    if (go.GetComponent<Turret>() != null)
                    {
                        if (Vector2.Distance(transform.position, go.transform.position) < go.GetComponent<Turret>().range )
                        {
                            GameObject dropGO = (GameObject)Instantiate(dropObject, this.transform.position, this.transform.rotation);
                            go.GetComponent<Turret>().Collect(dropGO);
                            hasCollected = true;
                        }
                    }
                }
            }
        }
        
        Destroy (gameObject);
	}
}
