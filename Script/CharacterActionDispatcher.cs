using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionDispatcher : MonoBehaviour
{
    public interface IMoveReceiver
    {
        void Move();
    }

    public interface IAttackReceiver
    {
        void Attack();
    }

}
