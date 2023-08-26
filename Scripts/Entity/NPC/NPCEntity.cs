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
        [SerializeField] private TMP_Text _aIStateText;
        
        protected override void Start()
        {
            base.Start();

            // TODO: Better way to set this.
            InputProvider = GetComponent<AIBrain>();
        }
    }
}