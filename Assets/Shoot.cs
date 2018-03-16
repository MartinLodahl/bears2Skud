using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public Transform bulletSpawn;

    public Pool pool;

    public float speed;
    //public float deathTimer    
    public float travelX;

    private float ySpeed;
    private float xSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //rotation        
        if (Input.GetKeyDown(KeyCode.Space))
        {
           Fire();    
        }
    }

    private void Fire()
    {
        GameObject bullet = pool.GetObject();
        Vector3 mousePos = Input.mousePosition;
        
        Vector3 objectPos = Camera.main.WorldToScreenPoint(bulletSpawn.transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float relativeX = mousePos.x - bulletSpawn.position.x;
        float relativeY = mousePos.y - bulletSpawn.position.y;

        bullet.transform.position = bulletSpawn.position;
        bullet.transform.rotation = bulletSpawn.rotation;
        bullet.SetActive(true);
        bool xPositive;
        bool yPositive;
        if (relativeX < 0){
            relativeX = relativeX * -1;
            xPositive = false;
        }
        else{
            xPositive = true;
        }
        if (relativeY < 0){
            relativeY = relativeY * -1;
            yPositive = false;
        }
        else{
            yPositive = true;
        }

        float yRelativeX = relativeY / relativeX;
        if (yRelativeX == 1 || yRelativeX>2)
        {
            ySpeed = CalculateFastestSpeed(yRelativeX);
            xSpeed = speed - ySpeed;
//            Debug.Log("Equal or Y>2x xSpeed: " + xSpeed + " ySpeed: " + ySpeed);
        }
        else if (yRelativeX > 1)
        {
            xSpeed = CalculateFastestSpeed(1 / yRelativeX);
            ySpeed = speed - xSpeed;
//            Debug.Log("Y>1x && Y<2x xSpeed: " + xSpeed + " ySpeed: " + ySpeed);
        }
        else if (yRelativeX < 1)
        {
            if (yRelativeX <0.5) {
                xSpeed = CalculateFastestSpeed(1 / yRelativeX);
                ySpeed = speed - xSpeed;
//                Debug.Log("X>2y xSpeed: " + xSpeed + " ySpeed: " + ySpeed);
            }
            else
            {
                xSpeed = CalculateFastestSpeed(1 / yRelativeX);
                ySpeed = speed - xSpeed;
//                Debug.Log("X<y2 && X>y1 xSpeed: " + xSpeed + " ySpeed: " + ySpeed);
            }
        }
        /*
        if (relativeX < 0)
        {
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(-speed, ySpeed*-speed);
        }
        else
        {
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(speed, ySpeed* speed);
        }*/
        if (!xPositive)
        {
            xSpeed = xSpeed * -1;
        }
        if (!yPositive)
        {
            ySpeed = ySpeed * -1;
        }
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(xSpeed, ySpeed);
        //bullet.transform.forward * 100;
        float traveldistance = travelX / speed;
        StartCoroutine(LateCall(traveldistance, bullet));
    }
    private float CalculateFastestSpeed(float highestNumber)
    {
        return  speed - (speed / (highestNumber + 1));
    }

    IEnumerator LateCall(float sec, GameObject bullet)
    {
        yield return new WaitForSeconds(sec);
        bullet.SetActive(false);
    }

}
