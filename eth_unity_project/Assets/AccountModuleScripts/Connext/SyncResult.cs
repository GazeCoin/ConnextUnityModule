
public class SyncResult
{
    public string status;
    public Update[] updates;

    public class Update
    {
        public string type;
        public UpdateDetails update;
    }

    public class UpdateDetails
    {
        public DepositArgs args;
        public string reason;
        public string sigUser;
        public string sigHub;
        public int txCount;
        public string createdOn;
    }

    public class DepositArgs
    {
        public string sigUser;
        public long timeout;
        public string depositWeiHub;
        public string depositWeiUser;
        public string depositTokenHub;
        public string depositTokenUser;
    }
}
