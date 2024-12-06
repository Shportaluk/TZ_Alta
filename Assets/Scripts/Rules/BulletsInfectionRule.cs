using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletsInfectionRule
{
    private readonly Func<IEnumerable<Obstacle>> _funcGetObstacles;


    public BulletsInfectionRule(Pool<Bullet> poolBullets, Func<IEnumerable<Obstacle>> funcGetObstacles)
    {
        poolBullets.onInstantiated += OnInstantiatedBullet;
        _funcGetObstacles = funcGetObstacles;
    }


    private void OnInstantiatedBullet(Bullet bullet)
    {
        bullet.onTriggerEnter += OnBulletTriggerEnter;
    }

    private void OnBulletTriggerEnter(Bullet bullet, Collider collider)
    {
        IEnumerable<Obstacle> obstacles = _funcGetObstacles();
        obstacles = obstacles.Where(o => o.IsInfected == false && o.IsDestroyed == false);
        obstacles = GameFuncs.FilterByDistance(bullet.transform.position, obstacles, bullet.DistanceToInfection);
        obstacles.ForEach(o => o.Infect);

        bullet.Destroy();
    }
}