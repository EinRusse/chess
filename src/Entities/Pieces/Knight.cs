
class Knight(Side side, Position position) : Piece(side, position)
{

  public override List<Square> GetMovableSquares(Board board)
  {
    int i = (int)Position.Column;
    int y = (int)Position.Row;
    List<Square> movableSquares = [];
    Square? upRight = board.GetSquare(new Position(i + 1, y + 2));
    if (upRight is not null && upRight.Piece?.Side != Side)
    {
      movableSquares.Add(upRight);
    }
    Square? upLeft = board.GetSquare(new Position(i - 1, y + 2));
    if (upLeft is not null && upLeft.Piece?.Side != Side)
    {
      movableSquares.Add(upLeft);
    }
    Square? leftUp = board.GetSquare(new Position(i - 2, y + 1));
    if (leftUp is not null && leftUp.Piece?.Side != Side)
    {
      movableSquares.Add(leftUp);
    }
    Square? leftDown = board.GetSquare(new Position(i - 2, y - 1));
    if (leftDown is not null && leftDown.Piece?.Side != Side)
    {
      movableSquares.Add(leftDown);
    }
    Square? rightUp = board.GetSquare(new Position(i + 2, y + 1));
    if (rightUp is not null && rightUp.Piece?.Side != Side)
    {
      movableSquares.Add(rightUp);
    }
    Square? rightDown = board.GetSquare(new Position(i + 2, y - 1));
    if (rightDown is not null && rightDown.Piece?.Side != Side)
    {
      movableSquares.Add(rightDown);
    }
    Square? downLeft = board.GetSquare(new Position(i - 1, y - 2));
    if (downLeft is not null && downLeft.Piece?.Side != Side)
    {
      movableSquares.Add(downLeft);
    }
    Square? downRight = board.GetSquare(new Position(i + 1, y - 2));
    if (downRight is not null && downRight.Piece?.Side != Side)
    {
      movableSquares.Add(downRight);
    }
    return movableSquares;
  }
  public override List<Square> GetDefendableSquares(Board board)
  {
    int i = (int)Position.Column;
    int y = (int)Position.Row;
    List<Square> movableSquares = [];
    Square? upRight = board.GetSquare(new Position(i + 1, y + 2));
    if (upRight is not null)
    {
      movableSquares.Add(upRight);
    }
    Square? upLeft = board.GetSquare(new Position(i - 1, y + 2));
    if (upLeft is not null)
    {
      movableSquares.Add(upLeft);
    }
    Square? leftUp = board.GetSquare(new Position(i - 2, y + 1));
    if (leftUp is not null)
    {
      movableSquares.Add(leftUp);
    }
    Square? leftDown = board.GetSquare(new Position(i - 2, y - 1));
    if (leftDown is not null)
    {
      movableSquares.Add(leftDown);
    }
    Square? rightUp = board.GetSquare(new Position(i + 2, y + 1));
    if (rightUp is not null)
    {
      movableSquares.Add(rightUp);
    }
    Square? rightDown = board.GetSquare(new Position(i + 2, y - 1));
    if (rightDown is not null)
    {
      movableSquares.Add(rightDown);
    }
    Square? downLeft = board.GetSquare(new Position(i - 1, y - 2));
    if (downLeft is not null)
    {
      movableSquares.Add(downLeft);
    }
    Square? downRight = board.GetSquare(new Position(i + 1, y - 2));
    if (downRight is not null)
    {
      movableSquares.Add(downRight);
    }
    return movableSquares;
  }

  public override string ToString()
  {
    if (Side == Side.Black) return "♘";
    else return "♞";
  }
}
