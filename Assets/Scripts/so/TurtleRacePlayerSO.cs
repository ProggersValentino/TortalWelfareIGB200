using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBank/Turtle Race Player Data")]
public class TurtleRacePlayerSO : ScriptableObject
{
    [SerializeField] private Sprite playerSprite;

    [SerializeField] private float speedCooldown;
    public float _speedCooldown
    {
        get { return speedCooldown; }
    }
    
    public Sprite _playerSprite
    {
        get { return playerSprite; }
    }

    [SerializeField] private float normalSpeed;
    public float _normalSpeed
    {
        get { return normalSpeed; }
    }
    [SerializeField] private float slowedSpeed;
    public float _slowedSpeed
    {
        get { return slowedSpeed; }
    }
    
    [SerializeField] private float boostedSpeed;
    public float _boostedSpeed
    {
        get { return boostedSpeed; }
    }

    public enum SpeedState
    {
        Slowed,
        Normal,
        Boosted
    }

    public float DetermineSpeed(SpeedState speedType)
    {
        switch (speedType)
        {
            case SpeedState.Slowed:
                return _slowedSpeed;
            case SpeedState.Normal:
                return _normalSpeed;
            case SpeedState.Boosted:
                return _boostedSpeed;
        }

        return 0f;
    }
}
