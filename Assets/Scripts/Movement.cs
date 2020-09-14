using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody rb;
    public GameController gameController;
    private Vector2 initialPos, touchPos, dir;
    private float forceMultiplier = 1.0F, forceStrength = 400F;
    public bool canMove = true;
    public Joystick joystick;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove){
            //rb.velocity = new Vector3(joystick.Horizontal * 480 * Time.deltaTime, rb.velocity.y 
            //, joystick.Vertical * 480 * Time.deltaTime);
            //rb.AddForce(new Vector3(joystick.Horizontal, 0, joystick.Vertical));
        }
       
        if(canMove){
            rb.AddForce(0, -800 * calculateForce(gameController.level) * Time.deltaTime, 0);
            if(Input.GetKey("a")){
                rb.AddForce(new Vector3(-400 * Time.deltaTime, 0, 0), ForceMode.Force);
            }else if(Input.GetKey("d")){
                rb.AddForce(new Vector3(400 * Time.deltaTime, 0, 0));
            }else if(Input.GetKey("w")){
                rb.AddForce(new Vector3(0, 0, 400 * Time.deltaTime));
            }else if(Input.GetKey("s")){
                rb.AddForce(new Vector3(0, 0, -400 * Time.deltaTime));
            }
        }

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began){
                initialPos.x = touch.position.x;
                initialPos.y = touch.position.y;
            }

            touchPos.x = touch.position.x;
            touchPos.y = touch.position.y;

            dir.x = touchPos.x - initialPos.x;
            dir.y = touchPos.y - initialPos.y;

            dir = dir.normalized;

            rb.AddForce(new Vector3(dir.x, 0, dir.y)*forceStrength*Time.deltaTime*calculateDistance(initialPos, touchPos), ForceMode.VelocityChange);
        }

     /** 
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                initialPos.x = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, touch.position.y)).x;
                initialPos.y = 0;
                initialPos.z = -Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, touch.position.y)).z;
            }

            
            touchPos.x = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, touch.position.y)).x;
            touchPos.y = 0;
            touchPos.z = -Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, touch.position.y)).z;

            dir = touchPos - initialPos;
            dir.Normalize();
            

            if(touch.phase == TouchPhase.Stationary){
                initialPos.x = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, touch.position.y)).x;
                initialPos.y = 0;
                initialPos.z = -Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, touch.position.y)).z;
                dir = Vector3.zero;
            }

            if(touch.phase == TouchPhase.Ended){
                dir = Vector3.zero;
            }

            rb.AddForce(dir*5*Time.deltaTime, ForceMode.VelocityChange);

        }
     */
    }

    void Update() {
        if(canMove){
            //rb.MovePosition(new Vector3(joystick.Horizontal * 240 * Time.deltaTime, 0 
            //, joystick.Vertical * 240 * Time.deltaTime));
            rb.velocity = new Vector3(joystick.Horizontal * 240 * Time.deltaTime 
            , rb.velocity.y, joystick.Vertical * 240 * Time.deltaTime);
            rb.AddForce(new Vector3(joystick.Horizontal * 100 * Time.deltaTime, 0, 
            joystick.Vertical * 100 * Time.deltaTime), ForceMode.VelocityChange);
        }
    }


    float calculateForce(int level){
        float ammount = Mathf.Log(level);
        if(ammount < 1) ammount = 0;
        if(ammount > 1) ammount = 1;
        return ammount + forceMultiplier;
    }

    float calculateDistance(Vector2 firstLoc, Vector2 secondLoc){
        return Mathf.Sqrt(Mathf.Pow(secondLoc.x-firstLoc.x, 2) + Mathf.Pow(secondLoc.y-firstLoc.y, 2));
    }

}
