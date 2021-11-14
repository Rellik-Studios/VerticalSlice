[System.Serializable]
public class PlayerData
{
    public int numOfPieces; //number of clock pieces
    public int Index; //the number which index for each time era (for change furniture)
    //public int Death;

    public bool hasPiece;

    public float[] position;

    public float[] rotation;
    //public string[] Loopnames;

    public PlayerData(PlayerSave player)
    {
        Index = player.Index;
        //Death = player.Death;
        numOfPieces = player.numOfPieces;

        hasPiece = player.hasPiece;

        position = new float[3];
        rotation = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;


        rotation[0] = player.transform.rotation.eulerAngles.x;
        rotation[1] = player.transform.rotation.eulerAngles.y;
        rotation[2] = player.transform.rotation.eulerAngles.z;
    }
}