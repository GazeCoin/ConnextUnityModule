using System;
using System.Collections.Generic;
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
        //opts.Verbose = true;
        opts.Pedantic = false;
        opts.Name = "gazecoin";
        // TODO: creds?

        try
        {
            connection = new ConnectionFactory().CreateConnection(opts);
            Debug.Log("NATS connection ID: " + connection.ConnectedId);
        } catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        //connection.
    }

    public void Disconnect()
    {

    }

    public void Start()
    {

    }
}
