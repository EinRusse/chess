
class Bishop(Side side, Position position) : Piece(side, position)
{
  public override List<Square> GetMovableSquares(Board board)
  {
    int i = (int)Position.Column;
    int y = (int)Position.Row;
    List<Square> movableSquares = [];
    while (i < 8)
    {
      i++;
      y++;
      Square? square = board.GetSquare(new Position(i, y));
      if (square is null) break;
      if (square.Piece is not null && square.Piece.Side == Side) break;
      movableSquares.Add(square);

      if (square.Piece is not null) break;
    }
    i = (int)Position.Column;
    y = (int)Position.Row;
    while (i < 8)
    {
      i++;
      y--;
      Square? square = board.GetSquare(new Position(i, y));
      if (square is null) break;
      if (square.Piece is not null && square.Piece.Side == Side) break;
      movableSquares.Add(square);

      if (square.Piece is not null) break;
    }
    i = (int)Position.Column;
    y = (int)Position.Row;
    while (i >= 0)
    {
      i--;
      y++;
      Square? square = board.GetSquare(new Position(i, y));
      if (square is null) break;
      if (square.Piece is not null && square.Piece.Side == Side) break;
      movableSquares.Add(square);

      if (square.Piece is not null) break;
    }
    i = (int)Position.Column;
    y = (int)Position.Row;
    while (i >= 0)
    {
      i--;
      y--;
      Square? square = board.GetSquare(new Position(i, y));
      if (square is null) break;
      if (square.Piece is not null && square.Piece.Side == Side) break;
      movableSquares.Add(square);

      if (square.Piece is not null) break;
    }
    return movableSquares;
  }
  public override List<Square> GetDefendableSquares(Board board)
  {
    int i = (int)Position.Column;
    int y = (int)Position.Row;
    List<Square> defendableSquares = [];
    while (i < 8)
    {
      i++;
      y++;
      Square? square = board.GetSquare(new Position(i, y));
      if (square is null) break;
      defendableSquares.Add(square);

      if (square.Piece is not null) break;
    }
    i = (int)Position.Column;
    y = (int)Position.Row;
    while (i < 8)
    {
      i++;
      y--;
      Square? square = board.GetSquare(new Position(i, y));
      if (square is null) break;
      defendableSquares.Add(square);

      if (square.Piece is not null) break;
    }
    i = (int)Position.Column;
    y = (int)Position.Row;
    while (i >= 0)
    {
      i--;
      y++;
      Square? square = board.GetSquare(new Position(i, y));
      if (square is null) break;
      defendableSquares.Add(square);

      if (square.Piece is not null) break;
    }
    i = (int)Position.Column;
    y = (int)Position.Row;
    while (i >= 0)
    {
      i--;
      y--;
      Square? square = board.GetSquare(new Position(i, y));
      if (square is null) break;
      defendableSquares.Add(square);

      if (square.Piece is not null) break;
    }
    return defendableSquares;
  }
  public override string ToString()
  {
    if (Side == Side.Black) return "♗";
    else return "♝";
  }
}
