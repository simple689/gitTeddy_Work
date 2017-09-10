import os
from Config import CommonConfig

CommonConfig.rebuild()
os.system("dotnet "+ CommonConfig._dllDebug + " -DLLType " + CommonConfig._dllType + " -ConfigType Debug -HostType Local")
os.system("pause")
