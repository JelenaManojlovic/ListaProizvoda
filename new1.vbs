Option Explicit

Dim objWebService, objWebSite
Dim objSite, objArgs, strServer
Dim WshNetwork

Set objArgs = WScript.Arguments

If objArgs.Length = 1 Then
strServer = objArgs(0)
ElseIf  objArgs.Length = 0 Then
Set WshNetwork = CreateObject("WScript.Network")
strServer = WshNetwork.ComputerName
Else
WScript.Echo "Usage: CScript ModifyVDir.vbs [servername]"
WScript.Quit
End If
WScript.Echo "Connecting to " & strServer

Set objWebService = GetObject("IIS://" & strServer & "/W3SVC")

For Each objWebSite in objWebService
Set objSite = GetObject(objWebSite.AdsPath)
If objSite.Class = "IIsWebServer" Then
WScript.Echo objSite.AdsPath
EnumVDir objSite, 1
End If
Next

WScript.Echo "Done processing."

Sub EnumVDir(objVDir, i)
Dim objSubVDir, objDir
Dim strNewPath

For Each objSubVDir in objVDir
If objSubVDir.Class = "IIsWebVirtualDir" Then
Set objDir = GetObject(objSubVDir.AdsPath)

If ((Right(objDir.Path, 1) = "\") and (Right(objDir.Path, 2) <> ":\")) Then
WScript.Echo Space(i*3) & objDir.AdsPath
WScript.Echo Space(i*3) & "Path = " & objDir.Path

strNewPath = Left(objDir.Path, Len(objDir.Path) - 1)
WScript.Echo Space(i*3) & "New Path = " & strNewPath

objDir.Put "Path", strNewPath
objDir.SetInfo
End If

EnumVDir objDir, i + 1
End If
Next
End Sub