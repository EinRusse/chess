
abstract class Piece(Side side, Position position)
{
  public Side Side = side;
  public Position Position = position;
  public abstract List<Square> GetMovableSquares(Board board);
  public abstract List<Square> GetDefendableSquares(Board board);
  public abstract override string ToString();
}