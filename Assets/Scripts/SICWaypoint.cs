using UnityEngine;
using System.Collections;
using SpaceImpact.Utility;

namespace SpaceImpact {

	public class SICWaypoint : MonoBehaviour {
		// Public Variables	
		[SerializeField] private Transform pointA;
		[SerializeField] private Transform pointB;

		// Private Variables	

		// Static Variables

		public Transform PointA { get { return pointA; } }

		public Transform PointB { get { return pointB; } }

# if UNITY_EDITOR
		private void OnDrawGizmos() {
			Vector3 upperLeft = pointA.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			Vector3 lowerLeft = new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			Vector3 upperRight = pointB.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			Vector3 lowerRight = pointB.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);

			Gizmos.DrawLine(lowerLeft, upperLeft);
			Gizmos.DrawLine(upperLeft, upperRight);
			Gizmos.DrawLine(upperRight, lowerRight);
			Gizmos.DrawLine(lowerRight, lowerLeft);
		}

# endif
	}
}