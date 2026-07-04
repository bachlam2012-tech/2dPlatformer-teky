using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    // Start is called before the first frame update
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountTochangeAttribute;
    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            PlayerHealth playerHealth = GameObject.Find("player").GetComponent<PlayerHealth>();
            if (playerHealth.currentHealth == playerHealth.maxHealth)
            {
                return false;
            }
            else
            {
                playerHealth.ChangeHealth(amountToChangeStat);
                return true;
            }
        }
        return false;
    }
}

public enum StatToChange
{
    none,
    health,
    mana,
    stamina
}
public enum AttributeToChange
{
    none,
    strength,
    defense,
    intelligence,
    agility
}

