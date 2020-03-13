// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Marvin.ClientFramework;
using Marvin.Container;
using Marvin.Tools.Wcf;

namespace Marvin.Tools.WcfClient.UI.Viewer
{
    [Plugin(LifeCycle.Singleton, typeof(IModuleWorkspace), Name = nameof(WcfClientViewerWorkspaceViewModel))]
    internal class WcfClientViewerWorkspaceViewModel : ModuleWorkspace
    {
        #region Dependency Injection

        public IWcfClientFactory ClientFactory { get; set; }

        #endregion

        private ObservableCollection<WcfClientInfoViewModel> _clients;
        public ObservableCollection<WcfClientInfoViewModel> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                NotifyOfPropertyChange(() => Clients);
            }
        }

        /// <summary>
        /// Called when initializing this screen.
        /// </summary>
        protected override void OnInitialize()
        {
            ClientFactory.ClientInfoChanged += OnClientInfoChanged;
            Clients = new ObservableCollection<WcfClientInfoViewModel>();

            foreach (var client in ClientFactory.ClientInfos)
                Clients.Add(new WcfClientInfoViewModel(client));

            base.OnInitialize();
        }

        /// <summary>
        /// Called when [client information changed].
        /// </summary>
        private void OnClientInfoChanged(object sender, WcfClientInfo client)
        {
            Execute.OnUIThread(() => UpdateClientInfo(client));
        }

        /// <summary>
        /// Updates the client information.
        /// </summary>
        private void UpdateClientInfo(WcfClientInfo clientInfo)
        {
            var clientVm = Clients.FirstOrDefault(c => c.Source == clientInfo);
            var index = 0;

            if (clientVm != null)
            {
                index = Clients.IndexOf(clientVm);
            }

            if (index >= 0)
            {
                Clients.RemoveAt(index);

                if (clientInfo.State != ConnectionState.Closed)
                {
                    Clients.Insert(index, new WcfClientInfoViewModel(clientInfo));
                }
            }
            else
            {
                Clients.Add(new WcfClientInfoViewModel(clientInfo));
            }
        }
    }
}
