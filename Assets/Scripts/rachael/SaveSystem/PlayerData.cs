[System.Serializable]
public class PlayerData
{
    public int numOfPieces; //number of clock pieces
    public int Index; //the number which index for each time era (for change furniture)
    public int Death;

    public float[] position;
    
    public PlayerData(Player player)
    {
        Index = player.Index;
        Death = player.Death;
        numOfPieces = player.numOfPieces;

        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}