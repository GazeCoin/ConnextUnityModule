using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NATS.Client;
using UnityEngine;

public class MessagingClient
{
    public string Url { get; set; }
    private Options opts;
    private IConnection connection;

	public MessagingClient()
	{
	}

    public MessagingClient(string url)
    {
        Url = url;
    }

    public void Connect()
    {
        opts = ConnectionFactory.GetDefaultOptions();
        opts.Url = Url;
        opts.Servers = new[] { Url };
        opts.Verbose = true;
        opts.Pedantic = false;
        //opts.Name = "GazeCoin";
        // TODO: creds?

        try
        {
            connection = new ConnectionFactory().CreateConnection(opts);
            Debug.Log("NATS connection ID: " + connection.ConnectedId);
            string inbox = connection.NewInbox();
            //Debug.Log("New inbox" + i);
            //string inbox = "_INBOX.S0C7SYBROBELA5GZSTF70S";// + NATS.Client.NUID.NextGlobal;
            Debug.Log(inbox);

            //EventHandler<MsgHandlerEventArgs> msgHandler = (sender, args) =>
            //{
            //    Debug.Log("sub handler: " + sender + args);
            //};
            //IAsyncSubscription sub = connection.SubscribeAsync(inbox, msgHandler);
            //sub.Start();

        } catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }


    public void Disconnect()
    {
        connection.Flush();
        connection.Drain();
        connection.Close();
    }

    public void Start()
    {

    }

    public String Send(string subject, string data)
    {
        // TODO - Put data into JSON
        //Guid uuid = Guid.NewGuid();
        //string sData = "{ id: \"{uuid}\" }".Replace("{uuid}", uuid.ToString());
        string sData = "{ }";
        Debug.Log(sData);
        Msg response = connection.Request(subject, Encoding.ASCII.GetBytes(sData), 5000);
        Debug.Log("request response " + response.ToString());
        return Encoding.ASCII.GetString(response.Data);
    }

    public String Send(string subject)
    {
        return this.Send(subject, "");
    }
}
