using System.Security.Cryptography;

static class CharExtensions
{
  public static bool IsValidColumn(this char text)
  {
    return char.ToUpper(text) switch
    {
      'A' => true,
      'B' => true,
      'C' => true,
      'D' => true,
      'E' => true,
      'F' => true,
      'G' => true,
      'H' => true,
      _ => false,
    };
  }
  public static bool IsValidRow(this char text)
  {
    return text switch
    {
      '1' => true,
      '2' => true,
      '3' => true,
      '4' => true,
      '5' => true,
      '6' => true,
      '7' => true,
      '8' => true,
      _ => false,
    };
  }
}