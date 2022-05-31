using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianManager : MonoBehaviour
{
    [SerializeField] PlayerBullet _bullet;
    [SerializeField] PlayerBehaviour _player;

    public void LaunchBullet(Vector3 targetPos, int attack, float speed)
    {
        var diff = targetPos - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);

        float arg;
        if(diff.x == 0.0f)
        {
            arg = 90.0f;
        }
        else
        {
            arg = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        }

        var bullet = Instantiate(_bullet, transform.position + new Vector3(0, 8), Quaternion.identity, _player.transform.parent);
        bullet.MoveBullet(_player, attack, speed, Utils.Effect.None, Utils.PlayerBulletType.Guardian, arg);
    }
}
