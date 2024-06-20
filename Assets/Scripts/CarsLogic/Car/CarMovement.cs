using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private BrakeWheels _breakWheels;

    [SerializeField]
    private GasWheels _gasWheels;

    [SerializeField]
    private TurnWheels _turnWheels;

    private void Update()
    {
        Debug.Log(_rb.velocity.magnitude);

        float verticalValue = Input.GetAxis("Vertical");
        if (verticalValue != 0f)
        {
            if (verticalValue > 0.1f)
                Gas();
            else if (verticalValue < -0.1f)
                Break();
        }

        Turn(Input.GetAxis("Horizontal"));
    }

    public void Gas()
    {
        _gasWheels.Gas();
    }

    public void Break()
    {
        _breakWheels.Break();
    }

    public void Turn(float direction)
    {
        _turnWheels.Turn(direction * 45f);
    }
}
