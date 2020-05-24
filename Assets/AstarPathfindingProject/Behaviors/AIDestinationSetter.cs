using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		public float distanceToPoint;
		public Transform[] patrolPoints;
		public bool followPlayer;

		IAstarAI ai;

		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		private void ChangeDirection()
		{
			float distance = Vector2.Distance(transform.position, patrolPoints[0].transform.position);
			if (distance < distanceToPoint)
			{
				ChangeArray();
			}
		}

		private void ChangeArray()
		{
			Transform tmp = patrolPoints[0];
			for (int i = 0; i < patrolPoints.Length - 1; i++)
			{
				patrolPoints[i] = patrolPoints[i + 1];
			}

			patrolPoints[patrolPoints.Length - 1] = tmp; // обращение к последнему элементу массива
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update ()
		{
			// приследование Player. Player является таргетом
			// иначе патрулировате по точкам 
			if (target != null && ai != null && followPlayer)
			{
				ai.destination = target.position;
			}
			else
			{
				ChangeDirection();
				ai.destination = patrolPoints[0].position;
			}
		}
	}
}
