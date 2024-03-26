class Knight(Side side, Position position) : Piece(side, position)
{
  public override string ToString()
  {
    if (Side == Side.Black) return "♘";
    else return "♞";
  }
}
