namespace Zocat
{
    public enum AbilityType
    {
        None = -1,
        StandIdle = 10,
        StandWalk = 20,
        StandRun = 30,
        Jump = 40,
        StandDeath = 50,
        Aim = 60,
        StandBack = 70,
        CrouchIdle = 80,
        ThomsonIdle = 90,
        ThomsonAim = 91,
        Revive = 100,
        HitUpper = 110,
        Shoot = 1001
    }

    public enum BodyAnimationType
    {
        None = -1,
        Idle = 10,
        Walk = 20,
        Run = 30,
        Jump = 40,
        Die = 50,
        Aim = 60,
        StandBack = 70
    }

    public enum Sub0AnimationType
    {
        None = -1,
        Pistol0 = 10,
        MachineGun0 = 20,
        Sniper0 = 30
    }

    public enum ParameterType
    {
        None = -1,
        Forward = 10,
        Crouch = 20
    }

    public enum DirectionType
    {
        None = -1,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }

    public enum KeywordType
    {
        None = -1,
        Idle = 10,
        Stand = 20,
        Walk = 30,
        Run = 40,
        Crouch = 50,
        Cover = 60,
        Pistol = 70,
        Machinegun = 80,
        Sniper = 90
    }

    public enum CharacterStateType
    {
        None = -1,
        Stand = 0,
        Crouch = 1,
        Aim = 2
    }
}