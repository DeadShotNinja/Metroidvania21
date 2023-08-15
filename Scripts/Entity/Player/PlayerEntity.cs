using System;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerEntity : BaseEntity
    {
        protected override void Start()
        {
            base.Start();

            // TODO: This should probably be provided through instantiation
            InputProvider = GameManager.Instance.PlayerInputHandler;
        }
    }
}