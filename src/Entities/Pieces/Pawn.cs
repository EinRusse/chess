class Pawn(Side side, Position position) : Piece(side, position)
{
  public bool DoubleMoved = false;
  public override string ToString()
  {
    if (Side == Side.Black) return "♙";
    else return "♟︎";
  }
}