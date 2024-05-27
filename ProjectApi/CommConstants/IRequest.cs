using System;
using System.ComponentModel.DataAnnotations;

namespace BikesApi.CommConstants
{
    public interface IRequest
    {
        string EndpointName { get; }
        bool HasData { get; }
    }

    public abstract class DataRequest : IRequest
    {
        protected DataRequest(string endpointName)
        {
            EndpointName = endpointName;
        }

        public string EndpointName { get; }

        public bool HasData 
        { 
            get { return true; } 
        }
    }

    public abstract class NoDataRequest : IRequest
    {
        protected NoDataRequest(string endpointName)
        {
            EndpointName = endpointName;
        }

        public string EndpointName { get; }

        public bool HasData
        {
            get { return false; }
        }
    }
}
