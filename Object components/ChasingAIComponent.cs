using Object_components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_components;

public class ChasingAIComponent
{
	public ICollidingObject Target { get; private set; }
	private readonly BehaviourType standartBehaviour;
	private BehaviourType behaviour;

	private readonly Transform2D transform;
	private readonly Physics physics;

	public ChasingAIComponent(ICollidingObject target, BehaviourType behaviour, Transform2D transform, Physics physics)
	{
		Target = target;
		standartBehaviour = behaviour;
		this.behaviour = behaviour;
		this.transform = transform;
		this.physics = physics;
	}

	public void Sleep() => behaviour = BehaviourType.Static;

	public void WakeUp() => behaviour = standartBehaviour;
}

public enum BehaviourType
{
	Static,
	BlindChasing,
	HitAndRun
}