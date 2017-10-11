Configuration ServiceFabricNode {

    Import-DscResource -ModuleName PsDesiredStateConfiguration

    Node 'localhost' {

        Script NetUse #ResourceName
        {
            GetScript = {
                @{ Result = (Get-ChildItem -Path "Z:\") }
            }
            SetScript = {
                net use Z: \\minecraftfilestore.file.core.windows.net\minecraftdata /u:AZURE\minecraftfilestore V8GVoXkSVsdfSZhAfDmEC4wNQFicScJKK+AOrQS223w1xOoTb60iA4jqfKFpnVQHIMRi/NaWLAnFqKfJ9o575g==
            }
            
            TestScript = { Test-Path "Z:\" }

        }
        #add the config to mount

    }
}