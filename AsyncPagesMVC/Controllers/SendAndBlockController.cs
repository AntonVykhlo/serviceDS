using System;
using System.IO;
using System.Web.Mvc;
using NServiceBus;

public class SendAndBlockController :
    Controller
{
    IEndpointInstance endpoint;

    public SendAndBlockController(IEndpointInstance endpoint)
    {
        this.endpoint = endpoint;
    }

    [HttpGet]
    public ActionResult Index()
    {
        ViewBag.Title = "SendAndBlock";
        return View();
    }

    [HttpPost]
    public ActionResult Index(string textField)
    {
        ViewBag.Title = "SendAndBlock";

        if (!int.TryParse(textField, out var number))
        {
            return View();
        }

        #region SendAndBlockController

        var command = new Command
        {
            Id = number
        };


        var sendOptions = new SendOptions();
        sendOptions.SetDestination("Samples.Mvc.Server");
        var status = endpoint.Request<ErrorCodes>(command, sendOptions).GetAwaiter().GetResult();
        using (StreamWriter sw = new StreamWriter(@"C:\upw\sdasdfasd2.txt", true))
        {
            sw.WriteLine("first request sent: " + DateTime.Now);
        }
        #region second command
        var command2 = new Command
        {
            Id = number + 3
        };

        var sendOptions2 = new SendOptions();
        sendOptions2.SetDestination("Samples.Mvc.Server");


        var status2 = endpoint.Request<ErrorCodes>(command2, sendOptions2).GetAwaiter().GetResult();
        #endregion
        using (StreamWriter sw = new StreamWriter(@"C:\upw\sdasdfasd2.txt", true))
        {
            sw.WriteLine("second request sent: " + DateTime.Now);
        }


        #endregion
        return IndexCompleted(Enum.GetName(typeof(ErrorCodes), status));

    }

    public ActionResult IndexCompleted(string errorCode)
    {
        ViewBag.Title = "SendAsync";
        ViewBag.ResponseText = errorCode;
        return View("Index");
    }
}
