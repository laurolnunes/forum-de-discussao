namespace Chatter.Domain.Core.Commands
{
    public class CommandResponse
    {
        public CommandResponse(bool success = false)
        {
            Success = success;
        }

        public static CommandResponse OK = new CommandResponse { Success = true };
        public static CommandResponse Fail = new CommandResponse { Success = false };

        public bool Success { get; private set; }
    }
}