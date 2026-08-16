using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class CarController : MonoBehaviour
{
    Rigidbody rb;

    [Header("On Screen Stats")]
    public int HP;
    public float speedDisplay = 0;
    public TextMeshProUGUI UIStats;

    [Header("Hiding")] // not working
    bool canHide = false;
    public bool isHiding = false;

    [Header("Shielding")] // not working
    bool canShield = true;
    public bool isShielding = false;
    [SerializeField] int ShieldLength;
    [SerializeField] int ShieldCooldown;

    [Header("Wheels")] // not working

    [SerializeField] Transform BackLeftWheel, BackRightWheel, FrontLeftWheel, FrontRightWheel;
    [SerializeField] WheelCollider BackLeftCol, BackRightCol, FrontLeftCol, FrontRightCol;

    [Header("Car Movement Varis")] // not working

    [SerializeField] float MotorForce, accerlationRate, decccerlationRate, steerAngle, brakeForce;
    float Throttle, Steering, MotorForceAccerlated;
    bool isBraking = false;
    float brake;
    bool GasPress = false;
    bool SpeedChange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void OnHide(InputAction.CallbackContext context)
    {
        if (canHide) // if player has ability
        {
            //isHiding = context.ReadValueAsButton();
            if (context.performed) // toogles hiding rather than having to hold it
            {
                isHiding = !isHiding; // Toggle state of bool
                // animation logic
                Debug.Log("hiding " + isHiding);
            }
        }
    }

    public void OnGas(InputAction.CallbackContext context)
    {
        GasPress = context.ReadValueAsButton(); // bool true when button is held
        if (MotorForceAccerlated < MotorForce) // accerlation and deccerlation logic, if the car is currently moving
            {
                MotorForceAccerlated = MotorForce + accerlationRate;
                //Debug.Log("MotorForce + accerlationRate = MotorForceAccerlated");
            }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 Input = context.ReadValue<Vector2>(); // input to vector 2

        Steering = Input.x; // turning car with horizontal input
        Throttle = Input.y; // extent with vertical input
    }

    public void OnBrake(InputAction.CallbackContext context)
    {
        isBraking = context.ReadValueAsButton(); // isbraking true so brake funtion starts having an effect
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        if (canShield) // checks if the cooldown is still active
        {
            if (context.performed) // toogles shield rather than having to hold it
            {
                isShielding = !isShielding; // Toggle state
                canShield = false; // starts cooldown, preventing stacking of effect

                // animation logic
                // damage logic

                Debug.Log("shielding " + isShielding);
                StartCoroutine(ShieldDuration());
            }
        }
    }

    IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(ShieldLength); // shield active for set seconds
        isShielding = false; // turn off shield
        Debug.Log("shielding " + isShielding);

        // animation logic

        StartCoroutine(ShieldCoolDown());
    }

    IEnumerator ShieldCoolDown()
    {
        Debug.Log("canShield " + canShield);
        yield return new WaitForSeconds(ShieldCooldown); // unable to use shield until set seconds is up
        canShield = true; // restores ability to use shield

        Debug.Log("canShield " + canShield);
    }

    void FixedUpdate() // every frame i think
    {
        Debug.Log(GasPress);
        if (GasPress) // if pushing gas
        {
            //SpeedChange = true; // either accerlating or declerlating
            ApplyTorque(); // moves the car using the back wheels - forward and backwards
            ApplySteering(); // rotates the car using front wheels - horiontal

            Accerlating(); // car gets faster the longer the player is holding the gas button
        }
        else
        {
            Deccerlating();

            UpdateWheelMeshes(); // not currently working
            if (!isBraking)
            {
                FrontLeftCol.brakeTorque = 0;
                FrontRightCol.brakeTorque = 0;
                BackLeftCol.brakeTorque = 0;
                BackRightCol.brakeTorque = 0;
            }

            ApplyBrakes();
        }
    }

    
    void Accerlating()
    {
        MotorForceAccerlated += accerlationRate; // adds accerlation rate to the force of wheel movements, see ApplyTorque

        speedDisplay = MotorForceAccerlated/100; // meant to look like km/h
    }

    void Deccerlating() // still wip
    {
        if (MotorForceAccerlated > decccerlationRate)
        {
            //Debug.Log("Decelerating");
            MotorForceAccerlated -= decccerlationRate;
            speedDisplay = MotorForceAccerlated / 100;

            BackLeftCol.motorTorque = MotorForceAccerlated; // slow down wheels
            BackRightCol.motorTorque = MotorForceAccerlated;
        }
        else // stop the car moving completely
        {
            //Debug.Log("Stopping at the end of decelerating");

            SuddenStop();
        }
    }

    void ApplyTorque() // Throttle is the vertical direction based on inputs, MotorForceAccerlated is movement speed
    {
        BackLeftCol.motorTorque = Throttle * MotorForceAccerlated; // rotates back wheels and moves car
        BackRightCol.motorTorque = Throttle * MotorForceAccerlated;
    }

    void ApplySteering() // tilts car, steerangle may need adjusting if the corners are tighter
    {
        FrontLeftCol.steerAngle = Steering * steerAngle;
        FrontRightCol.steerAngle = Steering * steerAngle;
    }

    void SuddenStop()
    {
        Debug.Log("SuddenStop");
        float stop = SpeedChange ? brakeForce : 0;

        rb.linearVelocity = Vector3.zero;
        
        FrontLeftCol.brakeTorque = stop; 
        FrontRightCol.brakeTorque = stop;
        BackLeftCol.brakeTorque = stop;
        BackRightCol.brakeTorque = stop;

        FrontLeftCol.brakeTorque = 0;
        FrontRightCol.brakeTorque = 0;
        BackLeftCol.motorTorque = 0;
        BackRightCol.motorTorque = 0;

        //speedDisplay = 0;
        MotorForceAccerlated = 0;
    }

    void ApplyBrakes() // stops car
    {
        brake = isBraking ? brakeForce : 0; // if isbraking is true than the brakeforce is 0
        Debug.Log("Braking");
        
        FrontLeftCol.brakeTorque = brake; 
        FrontRightCol.brakeTorque = brake;
        BackLeftCol.brakeTorque = brake;
        BackRightCol.brakeTorque = brake;

        FrontLeftCol.brakeTorque = 0;
        FrontRightCol.brakeTorque = 0;
        BackLeftCol.motorTorque = 0;
        BackRightCol.motorTorque = 0;

        MotorForceAccerlated = 0;
    }

    void UpdateWheelMeshes()
    {
        UpdateWheel(FrontLeftCol, FrontLeftWheel);
        UpdateWheel(FrontRightCol, FrontRightWheel);
        UpdateWheel(BackLeftCol, BackLeftWheel);
        UpdateWheel(BackRightCol, BackRightWheel);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;

        col.GetWorldPose(out pos, out rot);

        //trans.position = pos;
        //trans.rotation = rot;
    }
}