using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy", order = 1)]
public class NewEnemyData : ScriptableObject
{
    public int health;
    public int score;
    public int xp;
    public GameObject obj;
    public Sprite state1;
    public Sprite state2;
    public Sprite state3;
    public Sprite state4;
    public Sprite state5;
    public Sprite state6;
    public ParticleSystem explosion;
}
