using UnityEngine;
using System.Collections;

namespace SpaceImpact.Utility {

	public class SICAreaBounds : MonoBehaviour {
		// Public Variables

		// Private Variables
		private const float OFFSET = 0.5f;

		// Static Variables

		public static Vector3 MinPosition {
			get {
				return new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
					-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			}
		}

		public static Vector3 MaxPosition {
			get {
				return new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			}
		}

		public static Vector3 MinExtendedPosition {
			get {
				return new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET, 0.0f);
			}
		}

		public static Vector3 MaxExtendedPosition {
			get {
				return new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET, 0.0f);
			}
		}

	# if UNITY_EDITOR
		private void OnDrawGizmos() {
			Vector3 ptUpperRight = new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			Vector3 ptLowerRight = new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			Vector3 ptUpperLeft = new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);
			Vector3 ptLowerLeft = new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT, 0.0f);

			Gizmos.DrawLine(transform.position + ptUpperRight, transform.position + ptLowerRight);
			Gizmos.DrawLine(transform.position + ptLowerRight, transform.position + ptLowerLeft);
			Gizmos.DrawLine(transform.position + ptLowerLeft, transform.position + ptUpperLeft);
			Gizmos.DrawLine(transform.position + ptUpperLeft, transform.position + ptUpperRight);

			Gizmos.color = Color.green;

			ptUpperRight = new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET, 0.0f);
			ptLowerRight = new Vector3((SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET, 0.0f);
			ptUpperLeft = new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET,
				(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT + OFFSET, 0.0f);
			ptLowerLeft = new Vector3(-(SICGameSettings.GAME_WIDTH / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET,
				-(SICGameSettings.GAME_HEIGHT / 2) / SICGameSettings.GAME_PIXELS_PER_UNIT - OFFSET, 0.0f);

			Gizmos.DrawLine(transform.position + ptUpperRight, transform.position + ptLowerRight);
			Gizmos.DrawLine(transform.position + ptLowerRight, transform.position + ptLowerLeft);
			Gizmos.DrawLine(transform.position + ptLowerLeft, transform.position + ptUpperLeft);
			Gizmos.DrawLine(transform.position + ptUpperLeft, transform.position + ptUpperRight);
		}
	# endif
	}
}