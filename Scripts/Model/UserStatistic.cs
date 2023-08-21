[System.Serializable]
public class UserStatistic
{
    public string userId;
    public float timer;
    public float deathCount;
    public int[] attempts;

    public UserStatistic(string userId, float timer, float deathCount, int[] attempts)
    {
        this.userId = userId;
        this.timer = timer;
        this.deathCount = deathCount;
        this.attempts = attempts;
    }

}