using System;
using System.Collections.Generic;
using System.Drawing;

namespace Project1;

public class Board
{
    private static List<Place> places;
    private static List<Pawn> pawns;
    private static Pawn selectedPawn;

    private static Pawn nullPawn = new Pawn();
    
    public Board(int size)
    {
        Coords.SetSize(size);
        InitPlaces();
    }

    public static Pawn SelectedPawn => selectedPawn;
    public static Pawn NullPawn => nullPawn;

    public static void ChangeSelected(Pawn pawn)
    {
        selectedPawn = pawn;
    }
    
    public static void RemovePawn(Pawn pawn)
    {
        if (pawn.Field.Coordinates.Equals(nullPawn.Field.Coordinates))
            return;
        pawn.Field.SetFree();
        pawns.Remove(pawn);
    }

    private void InitPlaces()
    {
        pawns = new List<Pawn>();
        places = new List<Place>();
        int size = Coords.GetSize();
        int notFields = (size + 1) / 4;
        ;
        // if (boardType == BoardType.ENGLISH)
        // {
        //     for (int i = 0; i < size; i++)
        //     {
        //         for (int j = 0; j < size; j++)
        //         {
        //             Place place = new Place(new Coords(i, j));
        //             if ((i < notFields && j < notFields)
        //                 || (i >= size - notFields && j >= size - notFields)
        //                 || (i < notFields && j >= size - notFields)
        //                 || (i >= size - notFields && j < notFields))
        //             {
        //                 place.SetNotAField();
        //                 places.Add(place);
        //                 continue;
        //             }
        //
        //             if (i == j && i == size / 2)
        //             {
        //                 place.SetFree();
        //                 places.Add(place);
        //                 continue;
        //             }
        //
        //             Pawn pawn = new Pawn(place);
        //             
        //             places.Add(place);
        //             pawns.Add(pawn);
        //         }
        //     }
        // }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Place place = new Place(new Coords(i, j));
                if (MainWindow.boardtemplate[i, j] == -1)
                {
                    place.SetNotAField();
                } else if (MainWindow.boardtemplate[i, j] == 0)
                {
                    place.SetFree();
                }
                else
                {
                    Pawn pawn = new Pawn(place);
                    pawns.Add(pawn);
                }
                places.Add(place);
            }
        }

        selectedPawn = NullPawn;
    }

    public static Place GetPlace(Coords coordinates)
    {
        foreach (var place in places)
        {
            if (place.Coordinates.Equals(coordinates))
                return place;
        }

        return null;
    }

    public static Pawn GetPawn(Place field)
    {
        foreach (var pawn in pawns)
        {
            if (pawn.Field.Equals(field))
                return pawn;
        }

        return NullPawn;
    }

    public static Pawn GetPawn(Coords coords)
    {
        foreach (var pawn in pawns)
        {
            if (pawn.Field.Coordinates.Equals(coords))
                return pawn;
        }

        return NullPawn;
    }

    public static bool IsEnd()
    {
        foreach (var pawn in pawns)
        {
            if (pawn.AvailableMoves().Count != 0)
                return false;
            
        }
        return true;
    }

    public static bool IsWin()
    {
        if (pawns.Count == 1)
            return true;
        return false;
    }

}