using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class NPCEntity : BaseEntity
    {
        protected override void Start()
        {
            base.Start();

            // TODO: Better way to set this.
            InputProvider = GetComponent<AIBrain>();
        }
    }
}