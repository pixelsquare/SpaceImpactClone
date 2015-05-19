using UnityEngine;
using System.Collections;

namespace SpaceImpact.Utility {

	public class SICAreaBounds : MonoBehaviour {
		// Public Variables

		// Private Variables
		private const float OFFSET = 0.5f;

		// Static Variables

		private static Transform thisT;

		private static Vector3 ptUpperRight;
		private static Vector3 ptLowerRight;
		private static Vector3 ptUpperLeft;
		private static Vector3 ptLowerLeft;

		private static Vector3 ptExUpperRight;
		private static Vector3 ptExLowerRight;
		private static Vector3 ptExUpperLeft;
		private static Vector3 ptExLowerLeft;

		public static Transform ThisT { get { return thisT; } }

		public static Vector3 MinPosition { get { return ptLowerLeft; } }

		public static Vector3 MaxPosition { get { return ptUpperRight; } }

		public static Vector3 MinExtendedPosition { get { return ptExLowerLeft; } }

		public static Vector3 MaxExtendedPosition { get { return ptExUpperRight; } }

		private void Awake() {
			thisT = transform;

			//ptUpperRight = transform.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
			//    (SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			//ptLowerRight = transform.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
			//    -(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			//ptUpperLeft = transform.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
			//    (SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			//ptLowerLeft = transform.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
			//    -(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);

			//ptExUpperRight = transform.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET,
			//    (SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET, 0.0f);
			//ptExLowerRight = transform.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET,
			//    -(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET, 0.0f);
			//ptExUpperLeft = transform.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET,
			//    (SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET, 0.0f);
			//ptExLowerLeft = transform.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET,
			//    -(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET, 0.0f);

		}

	# if UNITY_EDITOR
		private void OnDrawGizmos() {
			ptUpperRight = transform.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			ptLowerRight = transform.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			ptUpperLeft = transform.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			ptLowerLeft = transform.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);

			Gizmos.DrawLine(ptUpperRight, ptLowerRight);
			Gizmos.DrawLine(ptLowerRight, ptLowerLeft);
			Gizmos.DrawLine(ptLowerLeft, ptUpperLeft);
			Gizmos.DrawLine(ptUpperLeft, ptUpperRight);

			Gizmos.color = Color.green;

			ptExUpperRight = transform.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET, 0.0f);
			ptExLowerRight = transform.position + new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET, 0.0f);
			ptExUpperLeft = transform.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET, 0.0f);
			ptExLowerLeft = transform.position + new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET, 0.0f);

			Gizmos.DrawLine(ptExUpperRight, ptExLowerRight);
			Gizmos.DrawLine(ptExLowerRight, ptExLowerLeft);
			Gizmos.DrawLine(ptExLowerLeft, ptExUpperLeft);
			Gizmos.DrawLine(ptExUpperLeft, ptExUpperRight);
		}
	# endif
	}
}