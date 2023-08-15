using UnityEngine;

namespace Metro
{
	public class EntityWallSlide : EntityComponent
	{
		[SerializeField, Range(0f, 1f)] private float _resistanceMultiplier = 0.5f;
		
		private Rigidbody2D _rb;
		
		public override void Initialize(BaseEntity entity)
		{
			base.Initialize(entity);

			_rb = _entity.EntityRigidbody;
			Type = ComponentType.WallSlide;
		}
		
		public void ApplySlide()
		{
			if (_rb.velocity.y < 0f)
			{
				float yVelocity = _rb.velocity.y * _resistanceMultiplier;
				_rb.velocity = new Vector2(_rb.velocity.x, yVelocity);
			}
		}
	}
}