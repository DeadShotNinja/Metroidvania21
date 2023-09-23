using System;
using UnityEngine;

namespace Metro
{
	public class AnimatorLocator : MonoBehaviour
	{
		private Animator _animator;
		
		private void Awake()
		{
			_animator = GetComponentInChildren<Animator>();
			
			if (!HasAnimator())
			{
				Debug.LogWarning("Animator missing from " + gameObject.name + " or its children. Animations will not work.", this);
			}
		}
		
		public bool HasAnimator()
		{
			return _animator != null;
		}
		
		public Animator GetAnimator()
		{
			return _animator;
		}
	}
}