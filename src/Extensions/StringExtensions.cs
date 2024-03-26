
static class StringExtensions
{
  public static Position? ToPosition(this string text)
  {
    if (text.Length != 2)
    {
      return null;
    }
    char column = char.ToUpper(text[0]);

    char row = text[1];

    Column convertedColumn = Column.A;
    Row convertedRow = Row.One;
    switch (char.ToUpper(column))
    {
      case 'A':
        break;
      case 'B':
        convertedColumn = Column.B;
        break;
      case 'C':
        convertedColumn = Column.C;
        break;
      case 'D':
        convertedColumn = Column.D;
        break;
      case 'E':
        convertedColumn = Column.E;
        break;
      case 'F':
        convertedColumn = Column.F;
        break;
      case 'G':
        convertedColumn = Column.G;
        break;
      case 'H':
        convertedColumn = Column.H;
        break;
      default:
        return null;
    };
    switch (row)
    {
      case '1':
        break;
      case '2':
        convertedRow = Row.Two;
        break;
      case '3':
        convertedRow = Row.Three;
        break;
      case '4':
        convertedRow = Row.Four;
        break;
      case '5':
        convertedRow = Row.Five;
        break;
      case '6':
        convertedRow = Row.Six;
        break;
      case '7':
        convertedRow = Row.Seven;
        break;
      case '8':
        convertedRow = Row.Eight;
        break;
      default:
        return null;
    }
    return new Position(convertedColumn, convertedRow);
  }
}