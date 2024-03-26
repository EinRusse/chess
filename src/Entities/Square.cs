class Square : IEquatable<Position>
{
  public Column Column { get; }
  public Row Row { get; }
  internal Piece? Piece { get; set; }

  public Square(int column, int row)
  {
    Column = (Column)column;
    Row = (Row)row;
  }
  public Square(int column, int row, Piece piece)
  {
    Column = (Column)column;
    Row = (Row)row;
    Piece = piece;
  }

  public override string ToString()
  {
    if (Piece is not null)
    {
      return Piece.ToString();
    }

    return "▢";


  }
  public bool Equals(Position? position)
  {

    if (position?.Column == Column && position.Row == Row)
    {
      return true;
    }
    else
    {
      return false;
    }
  }
}
