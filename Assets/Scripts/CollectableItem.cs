using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public GameObject myOwner;
    public void Collect()
    {
        PlayerStats.Mana += 10;
        Destroy(myOwner);
    }
}
