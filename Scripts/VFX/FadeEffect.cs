using System;
using System.Collections;
using UnityEngine;

namespace Metro
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class FadeEffect : MonoBehaviour
	{
		[Header("Setup")]
		[SerializeField] private Material _fadeMaterial;
		[SerializeField] private float _fadeSpeed = 0.1f;
		
		private SpriteRenderer _spriteRenderer;
		private BaseEntity _entity;
		private Material _originalMaterial;
		private Coroutine _fadeCoroutine;
		private static readonly int _fadeAmount = Shader.PropertyToID("_FadeAmount");

		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_entity = GetComponentInParent<BaseEntity>();
		}

		private void OnEnable()
		{
			if (_entity == null) return;
			
			_entity.EntityDiedAction += OnEntityDied;
			_entity.EntityRespawnedAction += OnEntityRespawned;
		}
		
		private void OnEntityDied()
		{
			_originalMaterial = _spriteRenderer.material;
			_spriteRenderer.material = _fadeMaterial;

			_fadeCoroutine = StartCoroutine(FadeMaterial());
		}
		
		private IEnumerator FadeMaterial()
		{
			float currentFade = 0f;
			_spriteRenderer.material.SetFloat(_fadeAmount, 0f);
			while (currentFade < 1f)
			{
				currentFade += _fadeSpeed * Time.deltaTime;
				_spriteRenderer.material.SetFloat(_fadeAmount, currentFade);
				yield return null;
			}
		}
		
		private void OnEntityRespawned()
		{
			StopCoroutine(_fadeCoroutine);
			_spriteRenderer.material.SetFloat(_fadeAmount, 0f);
			_spriteRenderer.material = _originalMaterial;
		}

		private void OnDisable()
		{
			if (_entity == null) return;
			
			_entity.EntityDiedAction -= OnEntityDied;
			_entity.EntityRespawnedAction -= OnEntityRespawned;
		}
	}
}