namespace UrlShortener.WebApi.Common.Exceptions {
    public class ServiceNotRegisteredException : Exception {
        public string ServiceName { get; set; } = null!;

        public ServiceNotRegisteredException(string serviceName) : base($"Service {serviceName} not registered") {
            this.ServiceName = serviceName;
        }
    }
}
