using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeUpgrade : Upgrade {
    [SerializeField] private float radius;

    public override void Run() {
        this.ConsumeStamina();
    }

}
