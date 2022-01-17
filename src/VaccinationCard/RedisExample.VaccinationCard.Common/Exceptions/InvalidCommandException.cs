namespace RedisExample.VaccinationCard.Common.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string command, string message)
            : base(message)
        {
            Source = command;
        }
    }
}
