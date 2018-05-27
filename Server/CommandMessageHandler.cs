using System;
using System.Data.OleDb;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

#region CommandMessageHandler
public class CommandMessageHandler :
    IHandleMessages<Command>
{
    static ILog log = LogManager.GetLogger<CommandMessageHandler>();

    //public Task Handle(Command message, IMessageHandlerContext context)
    //{
    //    log.Info("Request received");

    //    Task reply;
    //    DateTime t = DateTime.Now;
    //    DateTime tf = DateTime.Now.AddSeconds(10);

    //    while (t < tf)
    //    {
    //        t = DateTime.Now;
    //    }
    //    if (message.Id % 2 == 0)
    //    {
    //        log.Info("Returning Fail");
    //        reply = context.Reply(ErrorCodes.Fail);
    //    }
    //    else
    //    {
    //        log.Info("Returning None");
    //        reply = context.Reply(ErrorCodes.None);
    //    }
    //    return reply;
    //}
    public Task Handle(Command message, IMessageHandlerContext context)
    {
        log.Info("Request received");

        Task reply;
        
        string connectionString = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ServiceRateDB;Data Source=DESKTOP-78RVP3N\SQLEXPRESS";
        using (OleDbConnection connection = new OleDbConnection(connectionString))
        {

            OleDbCommand command = new OleDbCommand($"INSERT INTO [dbo].[Rates] ([RateMark]) VALUES ({message.Id})");


            command.Connection = connection;


            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                reply = context.Reply(ErrorCodes.None);
            }
            catch (Exception ex)
            {
                reply = context.Reply(ErrorCodes.Fail);
            }

        }
        return reply;
    }
}
#endregion
