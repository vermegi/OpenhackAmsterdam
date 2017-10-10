Configuration ServiceFabricNode {

    Import-DscResource -ModuleName PsDesiredStateConfiguration

    Node 'localhost' {

        Script NetUse #ResourceName
        {
            GetScript = {
                @{ Result = (Get-ChildItem -Path "G:\") }
            }
            SetScript = {
                # net use....
            }
            
            TestScript = { Test-Path "G:\" }

        }
        #add the config to mount

    }
}
    