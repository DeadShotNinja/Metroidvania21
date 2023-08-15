using UnityEngine;

namespace Metro
{
    public enum ComponentType
    {
        None,
        HorizontalMove,
        Jump,
        Dash,
        WallSlide
    }
    
    /// <summary>
    /// Base class for all Entity "Abilities"
    /// </summary>
    public abstract class EntityComponent : MonoBehaviour
    {
        protected BaseEntity _entity;

        [HideInInspector] public ComponentType Type = ComponentType.None;
        
        public virtual void Initialize(BaseEntity entity)
        {
            _entity = entity;
        }
        
        public virtual void LogicUpdate()
        {
            
        }
        
        public virtual void CleanUp()
        {
            
        }
    }
}