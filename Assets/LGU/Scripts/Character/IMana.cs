using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMana
{
    Transform transform { get; }
    float MP { get; set; }
    float MaxMP { get; }

   System.Action onManaChange { get; set; }
}
