namespace Project1;

public struct Coords
{
    private int x;
    private int y;
    private static int Size;

    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Coords(Coords coords, int x, int y)
    {
        this.x = coords.X + x;
        this.y = coords.Y + y;
    }
    
    public int X => x;

    public int Y => y;

    public static void SetSize(int size)
    {
        Size = size;
    }
    
    public static int GetSize()
    {
        return Size;
    }
}