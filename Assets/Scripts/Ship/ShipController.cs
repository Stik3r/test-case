using UnityEngine;

static public class ShipControl
{

    static float brakingRotate = 0.01f;
    static public float boost = 0.1f;
    static public float brakingSpeed = 0.01f;   // значения для энерции
    static public float speed = 0;
    static public float rotate = 0;

    static public int delay = 0;    // задержка выстрела пули
    
    static public float laserDelay = 0;
    static public float laselCooldown = 20f;    //переменные для лазера
    static public int laserCount = 3;

    static public Vector3 lastDirectional;  

    static public int score = 0;
    static public bool alive = true;

    static public Transform ship;


    //Повороты право/лево
    static public Quaternion RightRotate(Quaternion rotation)
    {
        Quaternion newAngel = Quaternion.Euler(0, 0, rotation.eulerAngles.z - 3);
        rotate = rotate >= 1 ? 1 : rotate + boost;
        return Quaternion.Lerp(rotation, newAngel, rotate);
    }

    static public Quaternion LeftRotate(Quaternion rotation)
    {
        Quaternion newAngel = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 3);
        rotate = rotate <= -1 ? -1 : rotate - boost;
        return Quaternion.Lerp(rotation, newAngel, -rotate);
    }

    //Остаточные движения поворота право/лево
    static public Quaternion ResidualMovementLeft(Quaternion rotation)
    {
        if (rotate < 0)
        {
            rotate = rotate >= 0 ? 0 : rotate + brakingRotate;
            Quaternion newAngel_ = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 3);
            return Quaternion.Lerp(rotation, newAngel_, -rotate);
        }
        return rotation;
    }

    static public Quaternion ResidualMovementRight(Quaternion rotation)
    {
        if (rotate > 0)
        {
            rotate = rotate <= 0 ? 0 : rotate - brakingRotate;
            Quaternion newAngel_ = Quaternion.Euler(0, 0, rotation.eulerAngles.z - 3);
            return Quaternion.Lerp(rotation, newAngel_, rotate);
        }
        return rotation;
    }

    //Телепорт при вылет за границу
    static public Vector3 Redirect(Collider2D collision, Vector3 position)
    {
        if (collision.name == "TopBotton")
        {
            float newPosY = Mathf.Abs(position.y);
            float sign = newPosY / position.y;
            newPosY -= 0.1f;
            return new Vector3(position.x, -(newPosY * sign), 0);
        }
        else
        {
            float newPosX = Mathf.Abs(position.x);
            float sign = newPosX / position.x;
            newPosX -= 0.1f;
            return new Vector3(-(newPosX * sign), position.y, 0);
        }
    }

    static public void Reset()
    {
        laserCount = 3;
        laserDelay = 0;
        speed = 0;
        rotate = 0;
        score = 0;
        ship = null;
    }
}
