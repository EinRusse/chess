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
  public List<Square> GetMovableSquares(Piece piece)
  {
    int i = (int)piece.Position.Column;
    int y = (int)piece.Position.Row;
    List<Square> movableSquares = [];
    switch (piece)
    {
      case Pawn:
        if (piece.Side == Side.White)
        {
          Square? square = GetSquare(new Position(i, y + 1));
          if (square is not null && square.Piece is null)
          {
            movableSquares.Add(square);
          }
          if (piece.Position.Row == Row.Two)
          {
            square = GetSquare(new Position(i, y + 2));
            if (square is not null && square.Piece is null)
            {
              movableSquares.Add(square);
            }
          }

          Square? leftDiagonal = GetSquare(new Position(i - 1, y + 1));
          if (leftDiagonal is not null && leftDiagonal.Piece is not null && leftDiagonal.Piece.Side != Side.White) movableSquares.Add(leftDiagonal);
          Square? rightDiagonal = GetSquare(new Position(i + 1, y + 1));
          if (rightDiagonal is not null && rightDiagonal.Piece is not null && rightDiagonal.Piece.Side != Side.White) movableSquares.Add(rightDiagonal);

          if (piece.Position.Row == Row.Five)
          {
            Square? left = GetSquare(new Position(i - 1, y));
            if (left is not null && left.Piece is not null && left.Piece.Side is Side.Black && left.Piece is Pawn && (left.Piece as Pawn).DoubleMoved is true && leftDiagonal is not null) movableSquares.Add(leftDiagonal);
            Square? right = GetSquare(new Position(i + 1, y + 1));
            if (right is not null && right.Piece is not null && right.Piece.Side is Side.Black && right.Piece is Pawn && (right.Piece as Pawn).DoubleMoved is true && rightDiagonal is not null) movableSquares.Add(rightDiagonal);
          }
        }
        else
        {
          Square? square = GetSquare(new Position(i, y - 1));
          if (square is not null && square.Piece is null)
          {
            movableSquares.Add(square);
          }

          if (piece.Position.Row == Row.Seven)
          {
            square = GetSquare(new Position(i, y - 2));
            if (square is not null && square.Piece is null)
            {
              movableSquares.Add(square);
            }
          }

          Square? leftDiagonal = GetSquare(new Position(i - 1, y - 1));
          if (leftDiagonal is not null && leftDiagonal.Piece is not null && leftDiagonal.Piece.Side != Side.Black) movableSquares.Add(leftDiagonal);
          Square? rightDiagonal = GetSquare(new Position(i - 1, y + 1));
          if (rightDiagonal is not null && rightDiagonal.Piece is not null && rightDiagonal.Piece.Side != Side.Black) movableSquares.Add(rightDiagonal);

          if (piece.Position.Row == Row.Four)
          {
            Square? left = GetSquare(new Position(i - 1, y));
            if (left is not null && left.Piece is not null && left.Piece.Side is Side.White && left.Piece is Pawn && (left.Piece as Pawn).DoubleMoved is true && leftDiagonal is not null) movableSquares.Add(leftDiagonal);
            Square? right = GetSquare(new Position(i + 1, y));
            if (right is not null && right.Piece is not null && right.Piece.Side is Side.White && right.Piece is Pawn && (right.Piece as Pawn).DoubleMoved is true && rightDiagonal is not null) movableSquares.Add(rightDiagonal);
          }
        }
        break;
      case Rook:
        while (i < 8)
        {
          i++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;

        }
        i = (int)piece.Position.Column;
        while (i >= 0)
        {
          i--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        while (y < 8)
        {
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;

        }
        y = (int)piece.Position.Row;
        while (y >= 0)
        {
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        break;
      case Bishop:
        while (i < 8)
        {
          i++;
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i < 8)
        {
          i++;
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i >= 0)
        {
          i--;
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i >= 0)
        {
          i--;
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        break;
      case Queen:
        while (i < 8)
        {
          i++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;

        }
        i = (int)piece.Position.Column;
        while (i >= 0)
        {
          i--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        while (y < 8)
        {
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;

        }
        y = (int)piece.Position.Row;
        while (y >= 0)
        {
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i < 8)
        {
          i++;
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i < 8)
        {
          i++;
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i >= 0)
        {
          i--;
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i >= 0)
        {
          i--;
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          if (square.Piece is not null && square.Piece.Side == piece.Side) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        break;
      case Knight:
        Square? upRight = GetSquare(new Position(i + 1, y + 2));
        if (upRight is not null && upRight.Piece?.Side != piece.Side)
        {
          movableSquares.Add(upRight);
        }
        Square? upLeft = GetSquare(new Position(i - 1, y + 2));
        if (upLeft is not null && upLeft.Piece?.Side != piece.Side)
        {
          movableSquares.Add(upLeft);
        }
        Square? leftUp = GetSquare(new Position(i - 2, y + 1));
        if (leftUp is not null && leftUp.Piece?.Side != piece.Side)
        {
          movableSquares.Add(leftUp);
        }
        Square? leftDown = GetSquare(new Position(i - 2, y - 1));
        if (leftDown is not null && leftDown.Piece?.Side != piece.Side)
        {
          movableSquares.Add(leftDown);
        }
        Square? rightUp = GetSquare(new Position(i + 2, y + 1));
        if (rightUp is not null && rightUp.Piece?.Side != piece.Side)
        {
          movableSquares.Add(rightUp);
        }
        Square? rightDown = GetSquare(new Position(i + 2, y - 1));
        if (rightDown is not null && rightDown.Piece?.Side != piece.Side)
        {
          movableSquares.Add(rightDown);
        }
        Square? downLeft = GetSquare(new Position(i - 1, y - 2));
        if (downLeft is not null && downLeft.Piece?.Side != piece.Side)
        {
          movableSquares.Add(downLeft);
        }
        Square? downRight = GetSquare(new Position(i + 1, y - 2));
        if (downRight is not null && downRight.Piece?.Side != piece.Side)
        {
          movableSquares.Add(downRight);
        }
        break;
      case King:
        Square? kingUpLeft = GetSquare(new Position(i - 1, y + 1));
        if (kingUpLeft is not null && kingUpLeft.Piece?.Side != piece.Side)
        {
          movableSquares.Add(kingUpLeft);
        }
        Square? kingUp = GetSquare(new Position(i, y + 1));
        if (kingUp is not null && kingUp.Piece?.Side != piece.Side)
        {
          movableSquares.Add(kingUp);
        }
        Square? kingUpRight = GetSquare(new Position(i + 1, y + 1));
        if (kingUpRight is not null && kingUpRight.Piece?.Side != piece.Side)
        {
          movableSquares.Add(kingUpRight);
        }
        Square? kingRight = GetSquare(new Position(i + 1, y));
        if (kingRight is not null && kingRight.Piece?.Side != piece.Side)
        {
          movableSquares.Add(kingRight);
        }
        Square? kingDownRight = GetSquare(new Position(i + 1, y - 1));
        if (kingDownRight is not null && kingDownRight.Piece?.Side != piece.Side)
        {
          movableSquares.Add(kingDownRight);
        }
        Square? kingDown = GetSquare(new Position(i, y - 1));
        if (kingDown is not null && kingDown.Piece?.Side != piece.Side)
        {
          movableSquares.Add(kingDown);
        }
        Square? kingDownLeft = GetSquare(new Position(i - 1, y - 1));
        if (kingDownLeft is not null && kingDownLeft.Piece?.Side != piece.Side)
        {
          movableSquares.Add(kingDownLeft);
        }
        Square? kingLeft = GetSquare(new Position(i + 1, y));
        if (kingLeft is not null && kingLeft.Piece?.Side != piece.Side)
        {
          movableSquares.Add(kingLeft);
        }
        break;
    }
    return movableSquares;
  }
  public List<Square> GetDefendableSquares(Piece piece)
  {
    int i = (int)piece.Position.Column;
    int y = (int)piece.Position.Row;
    List<Square> movableSquares = [];
    switch (piece)
    {
      case Pawn:
        if (piece.Side == Side.White)
        {
          Square? leftDiagonal = GetSquare(new Position(i - 1, y + 1));
          if (leftDiagonal is not null && leftDiagonal.Piece is not null && leftDiagonal.Piece.Side != Side.White) movableSquares.Add(leftDiagonal);
          Square? rightDiagonal = GetSquare(new Position(i + 1, y + 1));
          if (rightDiagonal is not null && rightDiagonal.Piece is not null && rightDiagonal.Piece.Side != Side.White) movableSquares.Add(rightDiagonal);

          if (piece.Position.Row == Row.Five)
          {
            Square? left = GetSquare(new Position(i - 1, y));
            if (left is not null && left.Piece is not null && left.Piece.Side != Side.Black && left.Piece is Pawn && (left.Piece as Pawn).DoubleMoved is false && leftDiagonal is not null) movableSquares.Add(leftDiagonal);
            Square? right = GetSquare(new Position(i + 1, y));
            if (right is not null && right.Piece is not null && right.Piece.Side != Side.Black && right.Piece is Pawn && (right.Piece as Pawn).DoubleMoved is true && rightDiagonal is not null) movableSquares.Add(rightDiagonal);
          }
        }
        else
        {
          Square? leftDiagonal = GetSquare(new Position(i - 1, y - 1));
          if (leftDiagonal is not null && leftDiagonal.Piece is not null && leftDiagonal.Piece.Side != Side.Black) movableSquares.Add(leftDiagonal);
          Square? rightDiagonal = GetSquare(new Position(i - 1, y + 1));
          if (rightDiagonal is not null && rightDiagonal.Piece is not null && rightDiagonal.Piece.Side != Side.Black) movableSquares.Add(rightDiagonal);

          if (piece.Position.Row == Row.Four)
          {
            Square? left = GetSquare(new Position(i - 1, y));
            if (left is not null && left.Piece is not null && left.Piece.Side != Side.Black && left.Piece is Pawn && (left.Piece as Pawn).DoubleMoved is false && leftDiagonal is not null) movableSquares.Add(leftDiagonal);
            Square? right = GetSquare(new Position(i + 1, y));
            if (right is not null && right.Piece is not null && right.Piece.Side != Side.Black && right.Piece is Pawn && (right.Piece as Pawn).DoubleMoved is true && rightDiagonal is not null) movableSquares.Add(rightDiagonal);
          }
        }
        break;
      case Rook:
        while (i < 8)
        {
          i++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;

        }
        i = (int)piece.Position.Column;
        while (i >= 0)
        {
          i--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        while (y < 8)
        {
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;

        }
        y = (int)piece.Position.Row;
        while (y >= 0)
        {
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        break;
      case Bishop:
        while (i < 8)
        {
          i++;
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i < 8)
        {
          i++;
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i >= 0)
        {
          i--;
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i >= 0)
        {
          i--;
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        break;
      case Queen:
        while (i < 8)
        {
          i++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;

        }
        i = (int)piece.Position.Column;
        while (i >= 0)
        {
          i--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        while (y < 8)
        {
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;

        }
        y = (int)piece.Position.Row;
        while (y >= 0)
        {
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i < 8)
        {
          i++;
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i < 8)
        {
          i++;
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i >= 0)
        {
          i--;
          y++;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        i = (int)piece.Position.Column;
        y = (int)piece.Position.Row;
        while (i >= 0)
        {
          i--;
          y--;
          Square? square = GetSquare(new Position(i, y));
          if (square is null) break;
          movableSquares.Add(square);

          if (square.Piece is not null) break;
        }
        break;
      case Knight:
        Square? upRight = GetSquare(new Position(i + 1, y + 2));
        if (upRight is not null)
        {
          movableSquares.Add(upRight);
        }
        Square? upLeft = GetSquare(new Position(i - 1, y + 2));
        if (upLeft is not null)
        {
          movableSquares.Add(upLeft);
        }
        Square? leftUp = GetSquare(new Position(i - 2, y + 1));
        if (leftUp is not null)
        {
          movableSquares.Add(leftUp);
        }
        Square? leftDown = GetSquare(new Position(i - 2, y - 1));
        if (leftDown is not null)
        {
          movableSquares.Add(leftDown);
        }
        Square? rightUp = GetSquare(new Position(i + 2, y + 1));
        if (rightUp is not null)
        {
          movableSquares.Add(rightUp);
        }
        Square? rightDown = GetSquare(new Position(i + 2, y - 1));
        if (rightDown is not null)
        {
          movableSquares.Add(rightDown);
        }
        Square? downLeft = GetSquare(new Position(i - 1, y - 2));
        if (downLeft is not null)
        {
          movableSquares.Add(downLeft);
        }
        Square? downRight = GetSquare(new Position(i + 1, y - 2));
        if (downRight is not null)
        {
          movableSquares.Add(downRight);
        }
        break;
      case King:
        Square? kingUpLeft = GetSquare(new Position(i - 1, y + 1));
        if (kingUpLeft is not null)
        {
          movableSquares.Add(kingUpLeft);
        }
        Square? kingUp = GetSquare(new Position(i, y + 1));
        if (kingUp is not null)
        {
          movableSquares.Add(kingUp);
        }
        Square? kingUpRight = GetSquare(new Position(i + 1, y + 1));
        if (kingUpRight is not null)
        {
          movableSquares.Add(kingUpRight);
        }
        Square? kingRight = GetSquare(new Position(i + 1, y));
        if (kingRight is not null)
        {
          movableSquares.Add(kingRight);
        }
        Square? kingDownRight = GetSquare(new Position(i + 1, y - 1));
        if (kingDownRight is not null)
        {
          movableSquares.Add(kingDownRight);
        }
        Square? kingDown = GetSquare(new Position(i, y - 1));
        if (kingDown is not null)
        {
          movableSquares.Add(kingDown);
        }
        Square? kingDownLeft = GetSquare(new Position(i - 1, y - 1));
        if (kingDownLeft is not null)
        {
          movableSquares.Add(kingDownLeft);
        }
        Square? kingLeft = GetSquare(new Position(i + 1, y));
        if (kingLeft is not null)
        {
          movableSquares.Add(kingLeft);
        }
        break;
    }
    return movableSquares;
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
          Square? enemyKingSquare = GetMovableSquares(piece).Find(x => x.Piece is King && x.Piece.Side != piece.Side);
          if (enemyKingSquare is not null && enemyKingSquare.Piece is not null)
          {
            Checked = true;
            List<Square> kingMovableSquares = GetMovableSquares(enemyKingSquare.Piece);
            foreach (Square square1 in Squares)
            {
              if (square1.Piece is not null && square1.Piece.Side == piece.Side)
              {
                List<Square> enemyDefendableSquares = GetDefendableSquares(square1.Piece);
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


              List<Square> movableSquares = GetMovableSquares(square.Piece);
              if (Checked == true && square.Piece is King)
              {
                foreach (Square square1 in Squares)
                {
                  if (square1.Piece is not null && square1.Piece.Side != square.Piece.Side)
                  {
                    List<Square> enemyDefendableSquares = GetDefendableSquares(square1.Piece);
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