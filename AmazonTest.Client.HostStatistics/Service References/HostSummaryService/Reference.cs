﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AmazonTest.Client.HostStatistics.HostSummaryService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HostSummaryService.IHostSummaryService")]
    public interface IHostSummaryService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHostSummaryService/SummarizeHostStatistics", ReplyAction="http://tempuri.org/IHostSummaryService/SummarizeHostStatisticsResponse")]
        AmazonTest.Service.Models.Host.HostSummaryResult SummarizeHostStatistics(AmazonTest.Service.Models.Host.HostSummaryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHostSummaryService/SummarizeHostStatistics", ReplyAction="http://tempuri.org/IHostSummaryService/SummarizeHostStatisticsResponse")]
        System.Threading.Tasks.Task<AmazonTest.Service.Models.Host.HostSummaryResult> SummarizeHostStatisticsAsync(AmazonTest.Service.Models.Host.HostSummaryRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHostSummaryServiceChannel : AmazonTest.Client.HostStatistics.HostSummaryService.IHostSummaryService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HostSummaryServiceClient : System.ServiceModel.ClientBase<AmazonTest.Client.HostStatistics.HostSummaryService.IHostSummaryService>, AmazonTest.Client.HostStatistics.HostSummaryService.IHostSummaryService {
        
        public HostSummaryServiceClient() {
        }
        
        public HostSummaryServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HostSummaryServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HostSummaryServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HostSummaryServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AmazonTest.Service.Models.Host.HostSummaryResult SummarizeHostStatistics(AmazonTest.Service.Models.Host.HostSummaryRequest request) {
            return base.Channel.SummarizeHostStatistics(request);
        }
        
        public System.Threading.Tasks.Task<AmazonTest.Service.Models.Host.HostSummaryResult> SummarizeHostStatisticsAsync(AmazonTest.Service.Models.Host.HostSummaryRequest request) {
            return base.Channel.SummarizeHostStatisticsAsync(request);
        }
    }
}
