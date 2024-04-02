bool ended = false;

async Task<ConsoleKey> ReadControlSettings()
{
  try
  {
    ConsoleKey key = default;
    await Task.Run(() => key = Console.ReadKey(true).Key);
    if (key is ConsoleKey.Q)
    {
      ended = true;
      Console.Clear();
      Console.WriteLine("Exiting program!");
      await Task.Delay(1000);
      Environment.Exit(0);
    }
    return key;
  }
  catch (Exception)
  {
    throw;
  }
}
void Run()
{
  _ = ReadControlSettings();
  while (true && ended == false)
  {
    Board board = new();
    while (board.Ended == false && ended == false)
    {
      board.SelectPiece();
    }
    Console.Clear();
    if (board.WhiteWinner is true)
    {
      Console.WriteLine("White wins!");
    }
    else
    {
      Console.WriteLine("Black Wins!");
    }
    Thread.Sleep(5000);
  }
}
Run();