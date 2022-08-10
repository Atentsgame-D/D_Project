using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    Transform transform { get; }
    float HP { get; set; }
    float MaxHP { get; }

   System.Action onHealthChange { get; set; }
}
