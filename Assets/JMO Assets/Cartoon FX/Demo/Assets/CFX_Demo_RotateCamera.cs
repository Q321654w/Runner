using UnityEngine;

// Cartoon FX  - (c) 2013, Jean Moreno

namespace JMO_Assets.Cartoon_FX.Demo.Assets
{
	public class CFX_Demo_RotateCamera : MonoBehaviour
	{
		static public bool rotating = true;
	
		public float speed = 30.0f;
		public Transform rotationCenter;
	
		void Update ()
		{
			if(rotating)
				transform.RotateAround(rotationCenter.position, Vector3.up, speed*Time.deltaTime);
		}
	}
}
