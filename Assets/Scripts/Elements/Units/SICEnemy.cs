using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public class SICEnemy : SICGameUnit {
		// Public Variables	

		// Private Variables
		private bool targetted;

		// Static Variables

		public bool IsTargetted { get { return targetted; } } 

		public override string OBJECT_ID {
			get { return SICObjectPoolName.OBJECT_ENEMY; }
		}

		public override void OnEnable() {
			base.OnEnable();
			targetted = false;
		}

		public void SetTargetted(bool tgt) {
			targetted = tgt;
		}
	}
}