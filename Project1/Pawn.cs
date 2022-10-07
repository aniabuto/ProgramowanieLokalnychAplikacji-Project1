using System;
using System.Collections.Generic;

namespace Project1;

public class Pawn
{
    private Place field;

    public Place Field => field;

    public Pawn()
    {
        this.field = new Place(new Coords(-1, -1));
    }

    public Pawn(Place field)
    {
        this.field = field;
    }

    public void MovePawn(Place field)
    {
        if (this.field.Coordinates.Equals(field.Coordinates))
        {
            ClearSelected();
            return;
        }
        // List<Place> availableMoes = AvailableMoves();
        if (!IsAvailable(field))
        {
            ChangeSelected(field);
            return;
        }
        this.field.SetFree();
        ClearSelected();
        Board.RemovePawn(GetMiddlePawn(field));
        this.field = field;
        this.field.SetTaken();
    }

    private bool IsAvailable(Place field)
    {
        List<Place> availableMoves = AvailableMoves();
        foreach (var move in availableMoves)
        {
            if (field.Coordinates.Equals(move.Coordinates))
                return true;
        }

        return false;
    }
    
    private Pawn GetMiddlePawn(Place destination)
    {
        int verticalDist = (destination.Coordinates.Y - field.Coordinates.Y) / 2;
        int horizontalDist = (destination.Coordinates.X - field.Coordinates.X) / 2;

        Coords middleCoords = new Coords(field.Coordinates, horizontalDist, verticalDist);
        return Board.GetPawn(middleCoords);
    }
    
    public void SelectPawn()
    {
        field.SetSelected();
        Board.ChangeSelected(this);
        MarkAvailableMoves();
    }

    public List<Place> AvailableMoves()
    {
        Coords coords = field.Coordinates;
        int size = Coords.GetSize();
        List<Place> availableMoves = new List<Place>();
        
        // to the right
        if(!(coords.X + 1 >= size || coords.X + 2 >= size)
           && Board.GetPlace(new Coords(coords.X + 1, coords.Y)).IsTakenField()
           && Board.GetPlace(new Coords(coords.X + 2, coords.Y)).IsNotTakenField())
        {
            availableMoves.Add(Board.GetPlace(new Coords(coords.X + 2, coords.Y)));
        }
        
        // to the left
        if(!(coords.X - 1 < 0 || coords.X - 2 < 0)
           && Board.GetPlace(new Coords(coords.X - 1, coords.Y)).IsTakenField()
           && Board.GetPlace(new Coords(coords.X - 2, coords.Y)).IsNotTakenField())
        {
            availableMoves.Add(Board.GetPlace(new Coords(coords.X - 2, coords.Y)));
        }
        
        // up
        if(!(coords.Y - 1 < 0 || coords.Y - 2 < 0)
           && Board.GetPlace(new Coords(coords.X, coords.Y - 1)).IsTakenField()
           && Board.GetPlace(new Coords(coords.X, coords.Y - 2)).IsNotTakenField())
        {
            availableMoves.Add(Board.GetPlace(new Coords(coords.X, coords.Y - 2)));
        }
        
        // down
        if(!(coords.Y + 1 >= size || coords.Y + 2 >= size)
           && Board.GetPlace(new Coords(coords.X, coords.Y + 1)).IsTakenField()
           && Board.GetPlace(new Coords(coords.X, coords.Y + 2)).IsNotTakenField())
        {
            availableMoves.Add(Board.GetPlace(new Coords(coords.X, coords.Y + 2)));
        }

        return availableMoves;
    }

    public void MarkAvailableMoves()
    {
        foreach (var place in AvailableMoves())
        {
            place.SetAvailable();
        }
    }

    public void UnmarkAvailableMoves()
    {
        foreach (var place in AvailableMoves())
        {
            place.SetFree();
        }
    }

    public void ChangeSelected(Place field)
    {
        UnmarkAvailableMoves();
        // MarkAvailableMoves();
        this.field.SetTaken();
        if(Board.GetPawn(field).Equals(Board.NullPawn))
        {
            Board.ChangeSelected(Board.NullPawn);
            return;
        }
        Pawn newSelected = Board.GetPawn(field);
        newSelected.SelectPawn();
        Board.ChangeSelected(newSelected);
    }

    public void ClearSelected()
    {
        UnmarkAvailableMoves();
        Board.ChangeSelected(Board.NullPawn);
    }
}