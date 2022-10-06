using System.Windows.Controls;

namespace Project1;

public class Field : Button
{
    private bool isTaken;
    private bool isField;
    private bool canMoveTo;
    private int i;
    private int j;

    public Field(int i, int j)
    {
        this.i = i;
        this.j = j;
        isTaken = true;
        isField = true;
    }

    public bool IsTaken => isTaken;
    public bool CanMoveTo => canMoveTo;
    public bool IsField => isField;

    public int I => i;

    public int J => j;

    public void Free()
    {
        isTaken = false;
    }

    public void Take()
    {
        isTaken = true;
    }

    public void NotAField()
    {
        isField = false;
    }

    public void Mark()
    {
        canMoveTo = true;
    }

    public void UnMark()
    {
        canMoveTo = false;
    }
}