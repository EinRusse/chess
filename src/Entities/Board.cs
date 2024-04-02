#pragma warning disable CS8602 // Dereference of a possibly null reference.

class Board
{
  private readonly List<Square> Squares;
  public Side Turn { get; set; }
  public List<Piece> TakenPieces;
  public bool Checked = false;
  public bool Ended = false;
  public bool WhiteWinner = false;
  private int Move = 1;

  public void PrintBoard()
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"Move {Move}");
    Console.ForegroundColor = ConsoleColor.DarkGray;

    foreach (Square square in Squares)
    {
      if (square.Column == Column.A)
      {
        Console.Write(square.Row.ToText() + " ");
      }
      Console.Write(square.ToString() + " ");
      if (square.Column == Column.H)
      {
        Console.WriteLine();
      }

    }
    Console.WriteLine("  Ⓐ Ⓑ Ⓒ Ⓓ Ⓔ Ⓕ Ⓖ Ⓗ");
    Console.ResetColor();

  }
  public void PrintBoard(List<Square> movableSquares)
  {
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"Move {Move}");
    Console.ForegroundColor = ConsoleColor.DarkGray;
    foreach (Square square in Squares)
    {
      if (square.Column == Column.A)
      {
        Console.Write(square.Row.ToText() + " ");
      }
      if (movableSquares.Contains(square))
      {
        Console.Write("* ");
      }
      else
      {
        Console.Write(square.ToString() + " ");
      };
      if (square.Column == Column.H)
      {
        Console.WriteLine();
      }

    }
    Console.WriteLine("  Ⓐ Ⓑ Ⓒ Ⓓ Ⓔ Ⓕ Ⓖ Ⓗ");
    Console.ResetColor();

  }
  public Square? GetSquare(Position position)
  {
    return Squares.Find(x => x.Column == position.Column && x.Row == position.Row);
  }
  public void MovePiece(Square square, Piece piece, List<Square> movableSquares)
  {
    PrintBoard(movableSquares);

    Console.Write("Type where to move:\n__");
    string nextMove = "";
    while (true)
    {
      ConsoleKeyInfo key = Console.ReadKey(true);

      if (key.Key == ConsoleKey.Backspace)
      {
        if (nextMove.Length > 0)
        {
          nextMove = nextMove.Remove(nextMove.Length - 1);
        }
      }
      else if ((key.KeyChar.IsValidColumn() || key.KeyChar.IsValidRow()) && nextMove.Length < 2)
      {
        nextMove += char.ToUpper(key.KeyChar);
      }
      else if (key.Key is ConsoleKey.Q)
      {
        Console.Clear();
        Console.WriteLine("Exiting program!");

        Task.Run(async () =>
        {
          await Task.Delay(3000);
          Environment.Exit(0);
        });
      }
      else if (key.Key == ConsoleKey.Enter)
      {

        Position? position = nextMove.ToPosition();
        Square? moveToSquare = movableSquares.Find(x => x.Equals(position));
        if (position is not null && moveToSquare is not null)
        {
          if (piece is Pawn && ((moveToSquare.Row == Row.Five && piece.Position.Row == Row.Seven) || (moveToSquare.Row == Row.Four && piece.Position.Row == Row.Two)))
          {
            (piece as Pawn).DoubleMoved = true;
          }
          square.Piece = null;
          Piece? lastDoubleMovedPiece = Squares.Find(x => x.Piece is not null && x.Piece is Pawn && (x.Piece as Pawn).DoubleMoved == true)?.Piece;
          if (lastDoubleMovedPiece is not null)
          {
            (lastDoubleMovedPiece as Pawn).DoubleMoved = false;
          }
          if (moveToSquare.Piece is not null)
          {
            TakenPieces.Add(moveToSquare.Piece);
          }

          if (Turn == Side.White)
          {
            Turn = Side.Black;
          }
          else
          {
            Move += 1;
            Turn = Side.White;
          }
          moveToSquare.Piece = piece;
          piece.Position = new Position(moveToSquare.Column, moveToSquare.Row);
          Square? enemyKingSquare = piece.GetMovableSquares(this).Find(x => x.Piece is King && x.Piece.Side != piece.Side);
          if (enemyKingSquare is not null && enemyKingSquare.Piece is not null)
          {
            Checked = true;
            List<Square> kingMovableSquares = enemyKingSquare.Piece.GetMovableSquares(this);
            foreach (Square square1 in Squares)
            {
              if (square1.Piece is not null && square1.Piece.Side == piece.Side)
              {
                List<Square> enemyDefendableSquares = square1.Piece.GetDefendableSquares(this);
                kingMovableSquares.RemoveAll(enemyDefendableSquares.Contains);
              }
            }
            if (kingMovableSquares.Count is 0)
            {
              Console.Clear();
              if (piece.Side is Side.White)
              {
                WhiteWinner = true;
              }

              Ended = true;
            }
          }

          break;
        }
      }
      if (nextMove.Length < 2)
      {
        string toPrintMove = "";
        foreach (char pos in nextMove)
        {
          toPrintMove += pos;
        }
        for (int i = toPrintMove.Length; i < 2; i++)
        {
          toPrintMove += "_";
        }
        Console.Write($"\r{toPrintMove}");
      }
      else
      {
        Console.Write($"\r{nextMove}");
      }
    }
  }
  public List<Square> SelectPiece()
  {
    PrintBoard();
    Console.Write($"{Turn}'s turn\n__");
    string nextMove = "";
    while (true)
    {
      ConsoleKeyInfo key = Console.ReadKey(true);

      if (key.Key == ConsoleKey.Backspace)
      {
        if (nextMove.Length > 0)
        {
          nextMove = nextMove.Remove(nextMove.Length - 1);
        }
      }
      else if ((key.KeyChar.IsValidColumn() || key.KeyChar.IsValidRow()) && nextMove.Length < 2)
      {
        nextMove += char.ToUpper(key.KeyChar);
      }
      else if (key.Key is ConsoleKey.Q)
      {
        Console.Clear();
        Console.WriteLine("Exiting progam!");

        Task.Run(async () =>
        {
          await Task.Delay(3000);
          Environment.Exit(0);
        });
      }
      else if (key.Key == ConsoleKey.Enter)
      {
        Position? position = nextMove.ToPosition();
        if (position is not null)
        {
          Square? square = GetSquare(position);
          if (square is not null && square.Piece is not null)
          {
            if ((Turn == Side.White && square.Piece.Side == Side.White) ||
             (Turn == Side.Black && square.Piece.Side == Side.Black))
            {
              if (Checked == true && square.Piece is not King) continue;


              List<Square> movableSquares = square.Piece.GetMovableSquares(this);
              if (Checked == true && square.Piece is King)
              {
                foreach (Square square1 in Squares)
                {
                  if (square1.Piece is not null && square1.Piece.Side != square.Piece.Side)
                  {
                    List<Square> enemyDefendableSquares = square1.Piece.GetMovableSquares(this);
                    movableSquares.RemoveAll(enemyDefendableSquares.Contains);
                  }
                }
              }
              if (movableSquares.Count is 0) continue;
              Console.WriteLine();
              MovePiece(square, square.Piece, movableSquares);
              return movableSquares;

            }
          }
        }
      }
      if (nextMove.Length < 2)
      {
        string toPrintMove = "";
        foreach (char pos in nextMove)
        {
          toPrintMove += pos;
        }
        for (int i = toPrintMove.Length; i < 2; i++)
        {
          toPrintMove += "_";
        }
        Console.Write($"\r{toPrintMove}");
      }
      else
      {
        Console.Write($"\r{nextMove}");
      }
    }
  }
  public Board()
  {
    List<Square> squares = [];
    for (int row = 7; row >= 0; row--)
    {
      for (int column = 0; column < 8; column++)
      {
        if (row == 6 || row == 7 || row == 0 || row == 1)
        {
          Position position = new(column, row);
          if (row == 6)
          {

            squares.Add(new Square(column, row, new Pawn(Side.Black, position)));

          }
          else if (row == 1)
          {
            squares.Add(new Square(column, row, new Pawn(Side.White, position)));
          }
          else
          {
            if (column == 0 || column == 7)
            {
              if (row == 7)
              {
                squares.Add(new Square(column, row, new Rook(Side.Black, position)));

              }
              else
              {
                squares.Add(new Square(column, row, new Rook(Side.White, position)));

              }
            }

            else if (column == 1 || column == 6)
            {
              if (row == 7)
              {
                squares.Add(new Square(column, row, new Knight(Side.Black, position)));

              }
              else
              {
                squares.Add(new Square(column, row, new Knight(Side.White, position)));

              }
            }
            else if (column == 2 || column == 5)
            {
              if (row == 7)
              {
                squares.Add(new Square(column, row, new Bishop(Side.Black, position)));

              }
              else
              {
                squares.Add(new Square(column, row, new Bishop(Side.White, position)));

              }
            }
            else if (column == 3)
            {
              if (row == 7)
              {
                squares.Add(new Square(column, row, new Queen(Side.Black, position)));

              }
              else
              {
                squares.Add(new Square(column, row, new Queen(Side.White, position)));

              }
            }
            else
            {
              if (row == 7)
              {
                squares.Add(new Square(column, row, new King(Side.Black, position)));

              }
              else
              {
                squares.Add(new Square(column, row, new King(Side.White, position)));

              }
            }
          }
        }
        else
        {
          squares.Add(new Square(column, row));
        }
      }
    }
    Squares = squares;
    TakenPieces = [];
    Turn = Side.White;
  }
}