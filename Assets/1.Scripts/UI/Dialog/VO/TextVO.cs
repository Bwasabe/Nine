[System.Serializable]
public class TextVO
{
    public enum FacePosition
    {
        LEFT = 0,
        RIGHT = 1,
        Middle = 2,
    }
    public FacePosition facePosition;
    public int icon;
    public string name;
    public string msg;
}
