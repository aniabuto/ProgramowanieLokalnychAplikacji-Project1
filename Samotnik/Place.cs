using System.Windows.Controls;
using System.Windows.Media;

namespace Project1;

public class Place : Button
{
    private Coords coordinates;
    private Color color;
    private SolidColorBrush brush;
    private FieldState state;

    public Place(Coords coordinates)
    {
        this.coordinates = coordinates;
        brush = new SolidColorBrush();
        //Background = brush;
        SetTaken();
    }

    public Coords Coordinates => coordinates;

    private void SetBackground()
    {
        brush.Color = color;
    }
    
    public void SetFree()
    {
        color = Colors.Beige;
        SetBackground();
        Tag = "Empty";
        state = FieldState.FREE;
    }

    public void SetTaken()
    {
        color = Colors.Black;
        SetBackground();
        Tag = "Taken";
        state = FieldState.TAKEN;
    }

    public void SetSelected()
    {
        color = Colors.Aqua;
        SetBackground();
        Tag = "Selected";
        state = FieldState.SELECTED;
    }

    public void SetAvailable()
    {
        color = Colors.LightGreen;
        SetBackground();
        Tag = "Available";
        state = FieldState.AVAILABLE;
    }

    public void SetNotAField()
    {
        color = Colors.White;
        SetBackground();
        state = FieldState.NOT_A_FIELD;
        IsEnabled = false;
    }
    
    public bool IsTaken()
    {
        return state == FieldState.TAKEN;
    }

    public bool IsAField()
    {
        return state != FieldState.NOT_A_FIELD;
    }

    public bool IsNotTakenField()
    {
        return !IsTaken() && IsAField();
    }

    public bool IsTakenField()
    {
        return IsTaken() && IsAField();
    }
}