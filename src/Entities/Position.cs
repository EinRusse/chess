

class Position
{

  public Column Column { get; set; }
  public Row Row { get; set; }
  public Position(Column column, Row row)
  {
    Column = column;
    Row = row;
  }
  public Position(int column, int row)
  {
    Column = (Column)column;
    Row = (Row)row;
  }
}