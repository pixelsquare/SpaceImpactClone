using UnityEngine;
using System.Collections;

namespace SpaceImpact.GameCore {

	public class SICGameManager : MonoBehaviour {
		// Public Variables	

		// Private Variables	
		private GameObject spaceShip;

		// Static Variables

		public GameObject SpaceShip {
			get { return spaceShip; }
		}

		private void Start() {
			spaceShip = SICObjectPoolManager.SharedInstance.GetObject(SICObjectPoolName.OBJECT_SPACE_SHIP);
			SICSpaceShip ship = spaceShip.GetComponent<SICSpaceShip>();
			ship.EnableElement();
		}
	}
}