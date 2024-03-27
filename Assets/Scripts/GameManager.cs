using Y9g;

public class GameManager : Singleton<GameManager>
{
    private string playerName;

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    // 判断是否属于某个玩家
    public bool IsBelongTo(string belongTo)
    {
        return belongTo == playerName;
    }
}