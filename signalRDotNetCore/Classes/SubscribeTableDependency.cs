using Microsoft.AspNetCore.SignalR;
using signalRDotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;

namespace signalRDotNetCore.Classes
{
    public class SubscribeTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Product> tableDependency;
        SignalRServer dashboardHub;
        private readonly IHubContext<SignalRServer> _signalrHub;

        public SubscribeTableDependency(SignalRServer dashboardHub, IHubContext<SignalRServer> signalrHub)
        {
            this.dashboardHub = dashboardHub;
            _signalrHub = signalrHub;
        }

        public void SubscribeProductTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Product>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                _signalrHub.Clients.All.SendAsync("LoadProducts");
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Product)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
