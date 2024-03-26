static class RowExtensions
{
  public static string ToText(this Row row)
  {
    return row switch
    {
      Row.One => "①",
      Row.Two => "②",
      Row.Three => "③",
      Row.Four => "④",
      Row.Five => "⑤",
      Row.Six => "⑥",
      Row.Seven => "⑦",
      Row.Eight => "⑧",
      _ => throw new NotImplementedException(),
    };
    ;
  }
}