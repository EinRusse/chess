
class King(Side side, Position position) : Piece(side, position)
{
  public override List<Square> GetMovableSquares(Board board)
  {
    int i = (int)Position.Column;
    int y = (int)Position.Row;
    List<Square> movableSquares = [];

    Square? kingUpLeft = board.GetSquare(new Position(i - 1, y + 1));
    if (kingUpLeft is not null)
    {
      movableSquares.Add(kingUpLeft);
    }
    Square? kingUp = board.GetSquare(new Position(i, y + 1));
    if (kingUp is not null)
    {
      movableSquares.Add(kingUp);
    }
    Square? kingUpRight = board.GetSquare(new Position(i + 1, y + 1));
    if (kingUpRight is not null)
    {
      movableSquares.Add(kingUpRight);
    }
    Square? kingRight = board.GetSquare(new Position(i + 1, y));
    if (kingRight is not null)
    {
      movableSquares.Add(kingRight);
    }
    Square? kingDownRight = board.GetSquare(new Position(i + 1, y - 1));
    if (kingDownRight is not null)
    {
      movableSquares.Add(kingDownRight);
    }
    Square? kingDown = board.GetSquare(new Position(i, y - 1));
    if (kingDown is not null)
    {
      movableSquares.Add(kingDown);
    }
    Square? kingDownLeft = board.GetSquare(new Position(i - 1, y - 1));
    if (kingDownLeft is not null)
    {
      movableSquares.Add(kingDownLeft);
    }
    Square? kingLeft = board.GetSquare(new Position(i + 1, y));
    if (kingLeft is not null)
    {
      movableSquares.Add(kingLeft);
    }
    return movableSquares;
  }
  public override List<Square> GetDefendableSquares(Board board)
  {
    int i = (int)Position.Column;
    int y = (int)Position.Row;
    List<Square> defendableSquares = [];

    Square? kingUpLeft = board.GetSquare(new Position(i - 1, y + 1));
    if (kingUpLeft is not null && kingUpLeft.Piece?.Side != Side)
    {
      defendableSquares.Add(kingUpLeft);
    }
    Square? kingUp = board.GetSquare(new Position(i, y + 1));
    if (kingUp is not null && kingUp.Piece?.Side != Side)
    {
      defendableSquares.Add(kingUp);
    }
    Square? kingUpRight = board.GetSquare(new Position(i + 1, y + 1));
    if (kingUpRight is not null && kingUpRight.Piece?.Side != Side)
    {
      defendableSquares.Add(kingUpRight);
    }
    Square? kingRight = board.GetSquare(new Position(i + 1, y));
    if (kingRight is not null && kingRight.Piece?.Side != Side)
    {
      defendableSquares.Add(kingRight);
    }
    Square? kingDownRight = board.GetSquare(new Position(i + 1, y - 1));
    if (kingDownRight is not null && kingDownRight.Piece?.Side != Side)
    {
      defendableSquares.Add(kingDownRight);
    }
    Square? kingDown = board.GetSquare(new Position(i, y - 1));
    if (kingDown is not null && kingDown.Piece?.Side != Side)
    {
      defendableSquares.Add(kingDown);
    }
    Square? kingDownLeft = board.GetSquare(new Position(i - 1, y - 1));
    if (kingDownLeft is not null && kingDownLeft.Piece?.Side != Side)
    {
      defendableSquares.Add(kingDownLeft);
    }
    Square? kingLeft = board.GetSquare(new Position(i + 1, y));
    if (kingLeft is not null && kingLeft.Piece?.Side != Side)
    {
      defendableSquares.Add(kingLeft);
    }
    return defendableSquares;
  }
  public override string ToString()
  {
    if (Side == Side.Black) return "♔";
    else return "♚";
  }
}
