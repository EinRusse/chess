
abstract class Piece(Side side, Position position)
{
  public Side Side = side;
  public Position Position = position;
  public abstract override string ToString();
}