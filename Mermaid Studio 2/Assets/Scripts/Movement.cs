using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public abstract Vector3 Move(Vector3 direction);

    public abstract GameObject RayCastManager(GameObject gameObj);
}
