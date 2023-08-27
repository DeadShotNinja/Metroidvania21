using UnityEngine;
using TMPro;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(AIBrain))]
    public class NPCEntity : BaseEntity
    {
        public TMP_Text AIStateText;
        
        protected override void Start()
        {
            base.Start();

            // TODO: Better way to set this.
            InputProvider = GetComponent<AIBrain>();
        }
    }
}