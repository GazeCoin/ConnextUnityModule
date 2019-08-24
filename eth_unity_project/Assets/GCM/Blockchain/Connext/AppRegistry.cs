using System;

public class AppRegistry : NatsResponse<AppRegistry.AppResponse[]>
{
    public class AppResponse {
        public int id;
        public string name;
        public string network;
        public string outcomeType;
        public string appDefinitionAddress;
        public string stateEncoding;
        public string actionEncoding;
        public bool allowNodeInstall;
    }
}
