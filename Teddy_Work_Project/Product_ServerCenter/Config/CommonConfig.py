import os

_dllType = "ServerCenter"
_projName = "Teddy_Work_" + _dllType
_dllName = "TeddyNetCore_" + _dllType

_root = "./../.."
_projDir = _root + "/" + _projName + "/TeddyNetCoreProject/" + _dllName
_projFrameDir = _root + "/" + _projName + "/TeddyNetCoreProject/TeddyNetCore_EngineFrame"

_dllDebug = _projFrameDir + "/bin/Debug/netcoreapp2.0/TeddyNetCore_EngineFrame.dll"
_dll = "./DLL/TeddyNetCore_EngineFrame.dll"

print (_projName)
print (_dllName)
print (_projDir)
print (_projFrameDir)
print (_dllDebug)
print (_dll)

def rebuild():
    print("rebuild")
    os.system("dotnet clean "+ _projDir)
    os.system("dotnet clean "+ _projFrameDir)
    os.system("dotnet build " + _projDir)
    os.system("dotnet build "+ _projFrameDir)
