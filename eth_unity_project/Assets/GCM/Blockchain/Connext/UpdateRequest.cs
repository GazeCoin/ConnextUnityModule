using System;

public class UpdateRequest
{
    public long id { get; set; }
    public string reason { get; set; }
    public Args args { get; set; }
    public long txCount { get; set; }
    public string sigUser { get; set; }
    public string sigHub { get; set; }
    public long createdOn { get; set; }
    public ThreadState[] initialThreadStates { get; set; }

	public UpdateRequest()
	{
	}

    public class Args
    {

    }

    public class ThreadState
    {

    }

}
