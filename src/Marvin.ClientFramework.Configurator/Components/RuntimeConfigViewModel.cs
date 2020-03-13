// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Marvin.ClientFramework.Configurator.Properties;

namespace Marvin.ClientFramework.Configurator
{
    [ConfigViewModelPlugin]
    internal class RuntimeConfigViewModel : ConfigViewModelBase<RuntimeConfig>
    {
        public override string DisplayName => Strings.RuntimeConfigViewModel_Title;

        public override string ImageSource => "/Controls4Industry;component/Images/stairs.png";
    }
}
