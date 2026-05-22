using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_components.Interfaces;

public interface IDamageableObject : ICollidingObject
{
	public HealthState Health { get; }
}
