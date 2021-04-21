using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.multiplayerOverride
{
    public class multiplayerAnimationUi : NetworkBehaviour
    {
        public Animator anim;

        public void checkAnimationState()
        {
            anim.Play("uiShow");
        }
    }
}