import os
from Config import CommonConfig

os.system("dotnet "+ CommonConfig._dll + " -DLLType " + CommonConfig._dllType + " -HostType Lan")
os.system("pause")
