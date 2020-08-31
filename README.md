# dotNET


>where /r c:\ csc.exe

>powershell.exe Get-ItemProperty ....exe -Name VersionInfo

>for /f %f in ('where /r c:\ csc.exe') do powershell.exe Get-ItemProperty '%f' -Name VersionInfo **( Nem működik... )**

```powershell "Get-Childitem -Path C:\ -Include csc.exe -File -Recurse  -ErrorAction SilentlyContinue | ForEach-Object { Get-ItemProperty $_.FullName -Name VersionInfo }" > "where are the csc.exe.txt"```


c:\Program Files\Unity\Editor\Data\MonoBleedingEdge\lib\mono\4.5\csc.exe

c:\Program Files\Unity\Editor\Data\MonoBleedingEdge\lib\mono\msbuild\15.0\bin\Roslyn\csc.exe

c:\Program Files\Unity\Editor\Data\Tools\Roslyn\csc.exe

c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\csc.exe

c:\Program Files (x86)\MSBuild\14.0\Bin\amd64\csc.exe
	-> Microsoft (R) Visual C# Compiler version 1.3.1.60616 Specify language version mode: ISO-1, ISO-2, 3, 4, 5, 6, or Default

**c:\ProgramData\CS-Script\CSScriptNpp\1.7.24.0\Roslyn\csc.exe**\
	-> Microsoft (R) Visual C# Compiler version 2.6.1.62414 (2c94423e) Supported language versions: default, 1, 2, 3, 4, 5, 6, * *7.0 (default), 7.1, 7.2 (latest)* *, latest

c:\Users\All Users\CS-Script\CSScriptNpp\1.7.24.0\Roslyn\csc.exe

c:\Users\User\.vscode\extensions\ms-dotnettools.csharp-1.22.1\.omnisharp\1.35.3\.msbuild\Current\Bin\Roslyn\csc.exe

c:\Windows\Microsoft.NET\Framework\v1.1.4322\csc.exe

c:\Windows\Microsoft.NET\Framework\v2.0.50727\csc.exe

c:\Windows\Microsoft.NET\Framework\v3.5\csc.exe

c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe
	-> Microsoft (R) Visual C# Compiler version 4.8.3761.0 for C# 5 Specify language version mode: ISO-1, ISO-2, 3, 4, 5, or Default

c:\Windows\Microsoft.NET\Framework64\v2.0.50727\csc.exe
	-> Microsoft (R) Visual C# 2005 Compiler version 8.00.50727.8670 for Microsoft (R) Windows (R) 2005 Framework version 2.0.50727 Specify language version mode: ISO-1 or Default

c:\Windows\Microsoft.NET\Framework64\v3.5\csc.exe
	-> Microsoft (R) Visual C# 2008 Compiler version 3.5.30729.8693 for Microsoft (R) .NET Framework version 3.5 Specify language version mode: ISO-1, ISO-2, or Default

c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe

c:\Windows\winsxs\amd64_netfx-csharp_compiler_csc_b03f5f7f11d50a3a_6.1.7600.16385_none_8b52bb03d4ea5d36\csc.exe

c:\Windows\winsxs\amd64_netfx-csharp_compiler_csc_b03f5f7f11d50a3a_6.1.7601.18523_none_8b28e17fd540a0c9\csc.exe

c:\Windows\winsxs\amd64_netfx-csharp_compiler_csc_b03f5f7f11d50a3a_6.1.7601.22733_none_745c3ae5eee71a77\csc.exe

c:\Windows\winsxs\amd64_netfx35linq-csharp_31bf3856ad364e35_6.1.7601.17514_none_7551b4792ac9630d\csc.exe

c:\Windows\winsxs\x86_netfx-csharp_compiler_csc_b03f5f7f11d50a3a_6.1.7600.16385_none_d2fff1dae966863c\csc.exe

c:\Windows\winsxs\x86_netfx-csharp_compiler_csc_b03f5f7f11d50a3a_6.1.7601.18523_none_d2d61856e9bcc9cf\csc.exe

c:\Windows\winsxs\x86_netfx-csharp_compiler_csc_b03f5f7f11d50a3a_6.1.7601.22733_none_bc0971bd0363437d\csc.exe

c:\Windows\winsxs\x86_netfx35linq-csharp_31bf3856ad364e35_6.1.7601.17514_none_193318f5726bf1d7\csc.exe
