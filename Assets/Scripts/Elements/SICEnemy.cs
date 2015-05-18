using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public class SICEnemy : SICGameElement {
		// Public Variables	

		// Private Variables
		private bool targetted;

		// Static Variables

		public bool IsTargetted { get { return targetted; } } 

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_ENEMY; }
		}

		public void OnEnable() {
			targetted = false;
		}

		public override void DisableElement() {
			SpawnExplotion();
			base.DisableElement();
		}

		public void SetTargetted(bool tgt) {
			targetted = tgt;
		}
	}
}