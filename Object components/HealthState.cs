using Object_components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_components;

public class HealthState
{
	public int HealthAmount { get; private set; }
	public readonly int MaxHealth;

	public IDamageableObject Owner { get; }

	public HealthState(int maxHealth, IDamageableObject owner)
	{
		HealthAmount = maxHealth;
		MaxHealth = maxHealth;
		Owner = owner;
	}

	public void TakeDamage(int damage)
	{
		HealthAmount -= damage;
	}
}
