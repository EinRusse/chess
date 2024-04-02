#pragma warning disable CS8602 // Dereference of a possibly null reference.
class Pawn(Side side, Position position) : Piece(side, position)
{
  public bool DoubleMoved = false;

  public override List<Square> GetMovableSquares(Board board)
  {
    int i = (int)Position.Column;
    int y = (int)Position.Row;
    List<Square> movableSquares = [];
    if (Side == Side.White)
    {
      Square? square = board.GetSquare(new Position(i, y + 1));
      if (square is not null && square.Piece is null)
      {
        movableSquares.Add(square);
      }
      if (Position.Row == Row.Two)
      {
        square = board.GetSquare(new Position(i, y + 2));
        if (square is not null && square.Piece is null)
        {
          movableSquares.Add(square);
        }
      }

      Square? leftDiagonal = board.GetSquare(new Position(i - 1, y + 1));
      if (leftDiagonal is not null && leftDiagonal.Piece is not null && leftDiagonal.Piece.Side != Side.White) movableSquares.Add(leftDiagonal);
      Square? rightDiagonal = board.GetSquare(new Position(i + 1, y + 1));
      if (rightDiagonal is not null && rightDiagonal.Piece is not null && rightDiagonal.Piece.Side != Side.White) movableSquares.Add(rightDiagonal);

      if (Position.Row == Row.Five)
      {
        Square? left = board.GetSquare(new Position(i - 1, y));
        if (left is not null && left.Piece is not null && left.Piece.Side is Side.Black && left.Piece is Pawn && (left.Piece as Pawn).DoubleMoved is true && leftDiagonal is not null) movableSquares.Add(leftDiagonal);
        Square? right = board.GetSquare(new Position(i + 1, y + 1));
        if (right is not null && right.Piece is not null && right.Piece.Side is Side.Black && right.Piece is Pawn && (right.Piece as Pawn).DoubleMoved is true && rightDiagonal is not null) movableSquares.Add(rightDiagonal);
      }
    }
    else
    {
      Square? square = board.GetSquare(new Position(i, y - 1));
      if (square is not null && square.Piece is null)
      {
        movableSquares.Add(square);
      }

      if (Position.Row == Row.Seven)
      {
        square = board.GetSquare(new Position(i, y - 2));
        if (square is not null && square.Piece is null)
        {
          movableSquares.Add(square);
        }
      }

      Square? leftDiagonal = board.GetSquare(new Position(i - 1, y - 1));
      if (leftDiagonal is not null && leftDiagonal.Piece is not null && leftDiagonal.Piece.Side != Side.Black) movableSquares.Add(leftDiagonal);
      Square? rightDiagonal = board.GetSquare(new Position(i - 1, y + 1));
      if (rightDiagonal is not null && rightDiagonal.Piece is not null && rightDiagonal.Piece.Side != Side.Black) movableSquares.Add(rightDiagonal);

      if (Position.Row == Row.Four)
      {
        Square? left = board.GetSquare(new Position(i - 1, y));
        if (left is not null && left.Piece is not null && left.Piece.Side is Side.White && left.Piece is Pawn && (left.Piece as Pawn).DoubleMoved is true && leftDiagonal is not null) movableSquares.Add(leftDiagonal);
        Square? right = board.GetSquare(new Position(i + 1, y));
        if (right is not null && right.Piece is not null && right.Piece.Side is Side.White && right.Piece is Pawn && (right.Piece as Pawn).DoubleMoved is true && rightDiagonal is not null) movableSquares.Add(rightDiagonal);
      }
    }
    return movableSquares;
  }
  public override List<Square> GetDefendableSquares(Board board)
  {
    int i = (int)Position.Column;
    int y = (int)Position.Row;
    List<Square> defendableSquares = [];
    if (Side == Side.White)
    {
      Square? leftDiagonal = board.GetSquare(new Position(i - 1, y + 1));
      if (leftDiagonal is not null && leftDiagonal.Piece is not null) defendableSquares.Add(leftDiagonal);
      Square? rightDiagonal = board.GetSquare(new Position(i + 1, y + 1));
      if (rightDiagonal is not null && rightDiagonal.Piece is not null) defendableSquares.Add(rightDiagonal);

      if (Position.Row == Row.Five)
      {
        Square? left = board.GetSquare(new Position(i - 1, y));
        if (left is not null && left.Piece is not null && left.Piece.Side is Side.Black && left.Piece is Pawn && (left.Piece as Pawn).DoubleMoved is true && leftDiagonal is not null) defendableSquares.Add(leftDiagonal);
        Square? right = board.GetSquare(new Position(i + 1, y + 1));
        if (right is not null && right.Piece is not null && right.Piece.Side is Side.Black && right.Piece is Pawn && (right.Piece as Pawn).DoubleMoved is true && rightDiagonal is not null) defendableSquares.Add(rightDiagonal);
      }
    }
    else
    {

      Square? leftDiagonal = board.GetSquare(new Position(i - 1, y - 1));
      if (leftDiagonal is not null && leftDiagonal.Piece is not null) defendableSquares.Add(leftDiagonal);
      Square? rightDiagonal = board.GetSquare(new Position(i - 1, y + 1));
      if (rightDiagonal is not null && rightDiagonal.Piece is not null) defendableSquares.Add(rightDiagonal);

      if (Position.Row == Row.Four)
      {
        Square? left = board.GetSquare(new Position(i - 1, y));
        if (left is not null && left.Piece is not null && left.Piece.Side is Side.White && left.Piece is Pawn && (left.Piece as Pawn).DoubleMoved is true && leftDiagonal is not null) defendableSquares.Add(leftDiagonal);
        Square? right = board.GetSquare(new Position(i + 1, y));
        if (right is not null && right.Piece is not null && right.Piece.Side is Side.White && right.Piece is Pawn && (right.Piece as Pawn).DoubleMoved is true && rightDiagonal is not null) defendableSquares.Add(rightDiagonal);
      }
    }
    return defendableSquares;
  }


  public override string ToString()
  {
    if (Side == Side.Black) return "♙";
    else return "♟︎";
  }
}
