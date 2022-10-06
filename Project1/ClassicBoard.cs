using System.Collections.Generic;

namespace Project1;

public class ClassicBoard
{
    private int size;
    private int notFields;
    private Field[] board;
    private List<Field> correct;

    public ClassicBoard(int size)
    {
        board = new Field[size * size];
        this.size = size;
        this.notFields = (size + 1) / 4;
        correct = new List<Field>();
    }

    public void PopulateBoard()
    {
        // i - poziom, j - pion
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if ((i < notFields && j < notFields)
                    || (i >= size - notFields && j >= size - notFields)
                    || (i < notFields && j >= size - notFields)
                    || (i >= size - notFields && j < notFields))
                {
                    board[i + size * j] = new Field(i, j);
                    board[i + size * j].NotAField();
                    continue;
                }

                board[i + size * j] = new Field(i, j);
                if (i == j && i == size / 2)
                {
                    board[i + size * j].Free();
                }
                else
                {
                    board[i + size * j].Take();
                }
            }
        }
    }

    // 0
    private void MoveRight(int i, int j)
    {
        board[i + size * j].Free();
        board[i + 1 + size * j].Free();
        board[i + 2 + size * j].Take();
    }

    // 1
    private void MoveLeft(int i, int j)
    {
        board[i + size * j].Free();
        board[i - 1 + size * j].Free();
        board[i - 2 + size * j].Take();
    }

    // 2
    private void MoveDown(int i, int j)
    {
        board[i + size * j].Free();
        board[i + size * (j + 1)].Free();
        board[i + size * (j + 2)].Take();
    }

    // 3
    private void MoveUp(int i, int j)
    {
        board[i + size * j].Free();
        board[i + size * (j - 1)].Free();
        board[i + size * (j - 2)].Take();
    }

    public void Move(Field start, Field destination)
    {
        if(start.I < destination.I)
            MoveRight(start.I, start.J);
        if(start.I > destination.I)
            MoveLeft(start.I, start.J);
        if(start.J > destination.J)
            MoveUp(start.I, start.J);
        if(start.J < destination.J)
            MoveDown(start.I, start.J);
    }
    
    private bool IsMoveCorrect(int i, int j, int direction)
    {
        int hopOver = i + size * j;
        int hopTo = i + size * j;
        switch (direction)
        {
            case 0:
                hopOver = i + 1 + size * j;
                hopTo = i + 2 + size * j;
                if (i + 1 >= size || i + 2 >= size)
                    return false;
                break;
            case 1:
                hopOver = i - 1 + size * j;
                hopTo = i - 2 + size * j;
                if (i - 1 < 0 || i - 2 < 0)
                    return false;
                break;
            case 2:
                hopOver = i + size * (j + 1);
                hopTo = i + size * (j + 2);
                if (j + 1 >= size || j + 2 >= size)
                    return false;
                break;
            case 3:
                hopOver = i + size * (j - 1);
                hopTo = i + size * (j - 2);
                if (j - 1 < 0 || j - 2 < 0)
                    return false;
                break;
        }

        if (!board[hopOver].IsField
            || !board[hopOver].IsTaken
            || !board[hopTo].IsField
            || board[hopTo].IsTaken)
            return false;
        return true;

    }

    public void ShowCorrectMoves(int i, int j)
    {
        if (IsMoveCorrect(i, j, 0))
            correct.Add(board[i + 2 + size * j]);
        if (IsMoveCorrect(i, j, 1))
            correct.Add(board[i - 2 + size * j]);
        if (IsMoveCorrect(i, j, 2))
            correct.Add(board[i + size * (j + 2)]);
        if (IsMoveCorrect(i, j, 3))
            correct.Add(board[i + size * (j - 2)]);
    }

    public void UnmarkCorrect()
    {
        correct.Clear();
    }
    
    public Field GetField(int i, int j)
    {
        return board[i + size * j];
    }

    public List<Field> GetCorrect()
    {
        return correct;
    }
}