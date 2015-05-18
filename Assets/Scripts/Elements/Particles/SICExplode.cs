using UnityEngine;
using System.Collections;

namespace SpaceImpact {

	public class SICExplode : SICGameElement {
		// Public Variables	
		[SerializeField] private ParticleSystem particle;

		// Private Variables	

		// Static Variables
		public override string OBJECT_ID {
			get { return SICObjectPoolName.PARTICLE_EXPLODE; }
		}

		public void Update() {
			if (!particle.IsAlive()) {
				gameObject.SetActive(false);
			}
		}
	}
}