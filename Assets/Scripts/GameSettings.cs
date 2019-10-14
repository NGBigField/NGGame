public enum GameControlMode
{
    Joystick,
    Gyroscope
}

public class GameSettings
{
    private static GameSettings instance;

    public static GameSettings Instance
    {
        get
        {
            if (instance == null) instance = new GameSettings();
            return instance;
        }
    }

    public GameControlMode ControlMode = GameControlMode.Joystick;
}
