namespace AgoraphobiaLibrary.Exceptions.Player;

public class NotEnoughDreamCoinsException : Exception
{
    public NotEnoughDreamCoinsException()
        : base("You don't have enough DreamCoins to buy that!")
    {
        
    }
}