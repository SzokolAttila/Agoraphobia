namespace AgoraphobiaLibrary.Exceptions.Player;

public class NotEnoughEnergyException : Exception
{
    public NotEnoughEnergyException()
        :base("You don't have enough energy to use that weapon!")
    {
        
    }
}