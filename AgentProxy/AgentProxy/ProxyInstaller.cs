using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace AgentProxy
{
    [RunInstaller(true)]
    public partial class ProxyInstaller : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller serviceProcessInstaller;

        private IContainer _components = null;

        public ProxyInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            serviceProcessInstaller = new ServiceProcessInstaller();

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "PMS Agent Proxy";
            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }

        private void InitializeComponent()
        {
            _components = new System.ComponentModel.Container();
        }
    }
}
