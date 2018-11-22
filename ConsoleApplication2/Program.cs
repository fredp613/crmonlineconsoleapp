using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.Net;
using System.ServiceModel.Description;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService organizationService = null;

            try
            {
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = "peaf@healthstandards.org";
                clientCredentials.UserName.Password = "Fredp613$";

                // For Dynamics 365 Customer Engagement V9.X, set Security Protocol as TLS12
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // Get the URL from CRM, Navigate to Settings -> Customizations -> Developer Resources
                // Copy and Paste Organization Service Endpoint Address URL
                organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri("https://hsodevelopment2.api.crm3.dynamics.com/XRMServices/2011/Organization.svc"),
                 null, clientCredentials, null);

                if (organizationService != null)
                {
                    Guid userid = ((WhoAmIResponse)organizationService.Execute(new WhoAmIRequest())).UserId;

                    if (userid != Guid.Empty)
                    {
                        Console.WriteLine("Connection Established Successfully...");
                    }

                    Entity TargetEntity = organizationService.Retrieve("opportunity", Guid.Parse("B76E3BEC-429B-E811-80C2-00155D011409"), new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

                    var activeInstancesRequest = new RetrieveProcessInstancesRequest
                    {
                        EntityId = TargetEntity.Id,
                        EntityLogicalName = TargetEntity.LogicalName
                    };
                   


                    Console.WriteLine(TargetEntity.GetAttributeValue<EntityReference>("stageid"));
                }
                else
                {
                    Console.WriteLine("Failed to Established Connection!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught - " + ex.Message);
            }
            Console.ReadKey();

        }
    }
}
