﻿using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

#region CommandMessageHandler
public class CommandMessageHandler :
    IHandleMessages<Command>
{
    static ILog log = LogManager.GetLogger<CommandMessageHandler>();

    public Task Handle(Command message, IMessageHandlerContext context)
    {
        log.Info("Hello from CommandMessageHandler");

        Task reply;
        DateTime t = DateTime.Now;
        DateTime tf = DateTime.Now.AddSeconds(10);

        while (t < tf)
        {
            t = DateTime.Now;
        }
        if (message.Id % 2 == 0)
        {
            log.Info("Returning Fail");
            reply = context.Reply(ErrorCodes.Fail);
        }
        else
        {
            log.Info("Returning None");
            reply = context.Reply(ErrorCodes.None);
        }
        return reply;
    }
}
#endregion
