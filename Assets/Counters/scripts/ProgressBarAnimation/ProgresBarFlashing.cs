using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgresBarFlashing : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    const string IS_FLASHING = "IsFlashing";
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnPublicChangesEventsArgs e)
    {

        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.isFried() && e.progressNormalised >= burnShowProgressAmount;

        animator.SetBool(IS_FLASHING, show);
    }
}
