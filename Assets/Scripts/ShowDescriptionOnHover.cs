using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDescriptionOnHover : MonoBehaviour
{
    public GameObject UIManager;
    private NodeUI uiManagerScript;
    public Spell thisSpell;
    private void Start()
    {
        uiManagerScript = UIManager.GetComponent<NodeUI>();
    }
    public void Show()
    {
        uiManagerScript.ShowSpellUI(thisSpell.manaCost, thisSpell.description);
    }
}

