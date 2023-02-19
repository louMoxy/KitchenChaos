using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<OnPublicChangesEventsArgs> OnProgressChanged;
    public class OnPublicChangesEventsArgs : EventArgs
    {
        public float progressNormalised;
    }
}
