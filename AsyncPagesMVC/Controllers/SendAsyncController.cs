using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using NServiceBus;
using System.IO;

public class SendAsyncController :
    AsyncController
{
    IEndpointInstance endpoint;

    public SendAsyncController(IEndpointInstance endpoint)
    {
        this.endpoint = endpoint;
    }

    [HttpGet]
    public ActionResult Index()
    {
        ViewBag.Title = "SendAsync";
        return View("Index");
    }

    [HttpPost]
    [AsyncTimeout(50000)]
    public async Task<ActionResult> IndexAsync(string textField)
    {
        if (!int.TryParse(textField, out var number))
        {
            return View("Index");
        }
        #region AsyncController
        var command = new Command
                        {
                            Id = number
                        };

        var sendOptions = new SendOptions();
        sendOptions.SetDestination("Samples.Mvc.Server");
        

        var status = await endpoint.Request<ErrorCodes>(command, sendOptions)
            .ConfigureAwait(false);
        using (StreamWriter sw = new StreamWriter(@"C:\upw\sdasdfasd.txt",true))
        {
            sw.WriteLine("first request sent: " + DateTime.Now);
        }
        #region second command
        var command2 = new Command
        {
            Id = number+3
        };

        var sendOptions2 = new SendOptions();
        sendOptions2.SetDestination("Samples.Mvc.Server");
        

        var status2 = await endpoint.Request<ErrorCodes>(command2, sendOptions2)
            .ConfigureAwait(false);
        #endregion
        using (StreamWriter sw = new StreamWriter(@"C:\upw\sdasdfasd.txt", true))
        {
            sw.WriteLine("first request sent: " + DateTime.Now);
        }

        return IndexCompleted(Enum.GetName(typeof(ErrorCodes), status));

        #endregion
    }

    public ActionResult IndexCompleted(string errorCode)
    {
        ViewBag.Title = "SendAsync";
        ViewBag.ResponseText = errorCode;
        return View("Index");
    }
}